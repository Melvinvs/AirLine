using BookingService.Entity;
using BookingService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Drawing;
using System.Text;

namespace BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _booking;

        public BookingController(IBookingRepository booking)
        {
            this._booking = booking;
        }

        [HttpPost("AddTicket"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<TicketModel>> AddTicket(TicketModel model)
        {

            //var status = QueueConsumer();

            Ticket obj = new Ticket
            {
                Id = model.id,
                FlightNo = model.flightNo,
                Name = model.name,
                AirLineName = model.airLineName,
                FromPlace = model.fromPlace,
                ToPlace = model.toPlace,
                StartTime = Convert.ToDateTime(model.startTime),
                EndTime = Convert.ToDateTime(model.endTime),
                TicketPrice = model.ticketPrice,
                MealType = model.mealType,
                Discount = model.discount,
                seatNo = model.seatNo,
                Email = model.email,
                PNR = model.pnr,
                Seattype = model.seatType,
                IsCancelled = model.isCancelled,
                Age = model.age,
                Gender = model.gender,
                CreatedBy = model.createdBy
            };

            var bookedTickets = _booking.GetBookedTickets(obj);

            if (model.seatType == 0 && bookedTickets >= model.totalESeats)
            {
                model.isBooked = 0;
            }
            else if (model.seatType == 1 && bookedTickets >= model.totalBSeats)
            {
                model.isBooked = 0;
            }
            else
            {
                obj.PNR = model.flightNo + model.fromPlace + obj.StartTime.Ticks + (++bookedTickets).ToString() + model.seatType;

                obj.seatNo = (model.seatType == 0 ? "E":"B") + (++bookedTickets).ToString();
                Ticket ticket = _booking.AddTicket(obj);
                model.isBooked = 1;

                model.pnr = ticket.PNR;
                model.seatNo = ticket.seatNo;

                creatPDF(model);

            }

            return Ok(model);
        }

        [HttpGet("SearchByPNR"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<List<Ticket>>> SearchByPNR(string PNR)
        {
            QueueConsumer();
            return Ok(_booking.SearchByPNR(PNR));
        }

        [HttpGet("SearchByemail"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<List<Ticket>>> SearchByemail(string email)
        {
            QueueConsumer();
            return Ok(_booking.GetBookings(email));
        }

        [HttpGet("ticketsbyuserid"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<List<Ticket>>> GetAllTicketByUserID(int UserID)
        {
            QueueConsumer();
            
            return Ok(_booking.GetAllTicketByUserID(UserID));
        }

        [HttpGet("CancelTicket"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Ticket>> CancelTicket(string PNR)
        {
            return Ok(_booking.CancelTicket(PNR));
        }


        public async Task<ActionResult<bool>> CancelTicketByAirline(string name)
        {
            return Ok(_booking.CancelTicketByAirline(name));
        }

        public bool QueueConsumer()
        {
            dynamic message;
            string data = "";
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("blockflights",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                data = Encoding.UTF8.GetString(body);

                Ticket model = new Ticket();

                if (!String.IsNullOrEmpty(data))
                {
                    message = JsonConvert.DeserializeObject<Ticket>(data);
                    model.AirLineName = message.AirLineName;
                    //message = Convert.ToString(data);
                }

                //var obj = new BookingController(_booking);
                var status = CancelTicketByAirline(model.AirLineName);
            };

            channel.BasicConsume("blockflights", true, consumer);

            return true;
        }


        public void creatPDF(TicketModel model)
        {
            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            XFont headfont = new XFont("Verdana", 20, XFontStyle.Bold);
            XFont font = new XFont("Verdana", 10, XFontStyle.Regular);

            XPoint point = new XPoint();

            //gfx.DrawString("Hello", font,XBrushes.Black, point);
            gfx.DrawString("Ticket", headfont, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormat.TopCenter);

            gfx.DrawString("Name", font, XBrushes.Black,100,50);
            gfx.DrawString(":" + model.name, font, XBrushes.Black,250,50);

            gfx.DrawString("Gender", font, XBrushes.Black, 100, 70);
            gfx.DrawString(":" + model.gender, font, XBrushes.Black, 250, 70);

            gfx.DrawString("Age", font, XBrushes.Black, 100, 90);
            gfx.DrawString(":" + model.age, font, XBrushes.Black, 250, 90);

            gfx.DrawString("PNR", font, XBrushes.Black, 100, 110);
            gfx.DrawString(":" + model.pnr, font, XBrushes.Black, 250, 110);

            gfx.DrawString("Seat No", font, XBrushes.Black, 100, 130);
            gfx.DrawString(":" + model.seatNo, font, XBrushes.Black, 250, 130);

            gfx.DrawString("Airline Name", font, XBrushes.Black, 100, 150);
            gfx.DrawString(":" + model.airLineName, font, XBrushes.Black, 250, 150);

            gfx.DrawString("Flight No.", font, XBrushes.Black, 100, 170);
            gfx.DrawString(":" + model.flightNo, font, XBrushes.Black, 250, 170);

            gfx.DrawString("Departure", font, XBrushes.Black, 100, 190);
            gfx.DrawString(":" + model.fromPlace, font, XBrushes.Black, 250, 190);

            gfx.DrawString("Destination", font, XBrushes.Black, 100, 210);
            gfx.DrawString(":" + model.toPlace, font, XBrushes.Black, 250, 210);

            gfx.DrawString("Start Time", font, XBrushes.Black, 100, 230);
            gfx.DrawString(":" + model.startTime, font, XBrushes.Black, 250, 230);

            gfx.DrawString("End Tine", font, XBrushes.Black, 100, 250);
            gfx.DrawString(":" + model.endTime, font, XBrushes.Black, 250, 250);



            string filename = model.pnr + ".pdf";

            document.Save("../FlightBooking/src/assets/Tickets/" + filename);

        }

    }
}
