using FlightService.Entity;
using FlightService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

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
        public async Task<ActionResult<bool>> AddFlight(FlightModel model)
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

            return Ok(true);
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

            var message = new { name = "blockflights", FlightNo = flights.FlightNo, FromPlace = flights.FromPlace, StartTime = flights.StartTime };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("", "blockflights", null, body);

            return Ok(flights);
        }

        [HttpPost("adddiscount"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Flight>> adddiscount(int id, int value)
        {
            Flight flights = _flight.adddiscount(id, value);

            return Ok(flights);
        }

        [HttpPost("addairline"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> AddAirLine(AirLineModel model)
        {
            var status = _flight.AddAirLine(model);

            return Ok(status);
        }

        [HttpGet("getallairline"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<AirLineModel>>> GetAllAirline()
        {
            return Ok(_flight.GetAllAirline());
        }
    }
}
