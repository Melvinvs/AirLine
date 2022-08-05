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
                StartTime = Convert.ToDateTime(model.StartTime),
                EndTime = Convert.ToDateTime(model.EndTime),
                PlaneNo = model.PlaneNo,
                TicketPrice = model.TicketPrice,
                ToralRows = model.ToralRows,
                BussinesSeatNo= model.BussinesSeatNo,
                EconomySeatNo= model.EconomySeatNo,
                ScheduledType= model.ScheduledType,
                MealType= model.MealType
            };

            Flight flight = _flight.AddFlight(domain);

            return Ok(true);
        }

        [HttpPost("flights")]
        public async Task<ActionResult<List<Flight>>> flights(string from, string to, string date)
        {
            List<Flight> flights = _flight.GetFlights(from, to, Convert.ToDateTime(date));

            return Ok(flights);
        }

        [HttpPost("blockairline"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> blockAirline(int id)
        {
            AirLineModel obj = _flight.BlockAirline(id);

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

            var message = new { name = "blockflights", AirLineName = obj.AirlineName };
            //var message = obj.AirlineName;
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("", "blockflights", null, body);

            return Ok(obj != null ? true : false);
        }

        [HttpPost("adddiscount"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> adddiscount(string name, int value)
        {
            var status = _flight.adddiscount(name, value);

            return Ok(status);
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
