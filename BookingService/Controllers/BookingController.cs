using BookingService.Entity;
using BookingService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(_booking.SearchByPNR(PNR));
        }

        [HttpPost("SearchByemail"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<List<Ticket>>> SearchByemail(string email)
        {
            return Ok(_booking.GetBookings(email));
        }

        [HttpPost("CancelTicket"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Ticket>> CancelTicket(string PNR)
        {
            return Ok(_booking.CancelTicket(PNR));
        }
    }
}
