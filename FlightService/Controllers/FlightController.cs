using FlightService.Entity;
using FlightService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IdataRepository _flight;

        public FlightController(IdataRepository flight)
        {
            this._flight = flight;
        }

        [HttpPost("addflight"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<String>> AddFlight(FlightModel model)
        {
            var domain = new Flight
            {
                FlightNo = model.FlightNo,
                AirLineName = model.AirLineName,
                FromPlace = model.FromPlace,
                ToPlace = model.ToPlace,
                PlaneNo = model.PlaneNo,
                TicketPrice = model.TicketPrice,
                ToralRows = model.ToralRows
            };

            Flight flight = _flight.AddFlight(domain);

            return Ok("Added flight Successfully");
        }

        [HttpPost("flights")]
        public async Task<ActionResult<List<Flight>>> flights(string from, string to)
        {
            List<Flight> flights = _flight.GetFlights(from, to);

            return Ok(flights);
        }

        [HttpPost("blockflights"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Flight>> blockflights(int id)
        {
            Flight flights = _flight.BlockFlight(id);

            return Ok(flights);
        }

        [HttpPost("adddiscount"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Flight>> adddiscount(int id, int value)
        {
            Flight flights = _flight.adddiscount(id, value);

            return Ok(flights);
        }
    }
}
