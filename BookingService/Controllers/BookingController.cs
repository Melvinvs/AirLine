using BookingService.Entity;
using BookingService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
        public async Task<ActionResult<String>> AddTicket(TicketModel model)
        {

            var status = QueueConsumer();

            Ticket obj = new Ticket
            {
                Id = model.Id,
                FlightNo = model.FlightNo,
                Name = model.Name,
                AirLineName = model.AirLineName,
                FromPlace = model.FromPlace,
                ToPlace = model.ToPlace,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                TicketPrice = model.TicketPrice,
                MealType = model.MealType,
                Discount = model.Discount,
                seatNo = model.seatNo,
                Email = model.Email,
                PNR = model.PNR,
                Seattype = model.Seattype,
                IsCancelled = model.IsCancelled,
                Age = model.Age,
                Gender = model.Gender
            };

            var bookedTickets = _booking.GetBookedTickets(obj);

            if (model.Seattype == 0 && bookedTickets >= model.totalESeats)
            {
                return Ok("All seats booked");
            }
            else if (model.Seattype == 1 && bookedTickets >= model.totalBSeats)
            {
                return Ok("All seats booked");
            }
            else
            {
                obj.PNR = model.FlightNo + model.FromPlace + model.StartTime.Ticks + (++bookedTickets).ToString();

                obj.seatNo = (model.Seattype == 0 ? "E":"B") + (++bookedTickets).ToString();
                Ticket ticket = _booking.AddTicket(obj);

                return Ok("Successful PNR: " + ticket.PNR.ToUpper() + " \n SeatNo :" + ticket.seatNo);
            }                
        }

        [HttpPost("SearchByPNR"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Ticket>> SearchByPNR(string PNR)
        {
            //QueueConsumer();
            return Ok(_booking.SearchByPNR(PNR));
        }

        [HttpPost("SearchByemail"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<List<Ticket>>> SearchByemail(string email)
        {
            //QueueConsumer();
            return Ok(_booking.GetBookings(email));
        }

        [HttpPost("CancelTicket"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Ticket>> CancelTicket(string PNR)
        {
            return Ok(_booking.CancelTicket(PNR));
        }

        public async Task<ActionResult<bool>> CancelTicketByFlightID(Ticket model)
        {
            return Ok(_booking.CancelTicketByFlightID(model));
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
                    model.FlightNo = message.FlightNo;
                    model.FromPlace = message.FromPlace;
                    model.StartTime =message.StartTime;
                }

                var status = CancelTicketByFlightID(model);
            };

            channel.BasicConsume("blockflights", true, consumer);

            return true;
        }
    }
}
