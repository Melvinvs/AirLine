using BookingService.Entity;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace BookingService.Controllers
{
    public class QueueListener: IHostedService
    {
        private IConnection connection;
        private readonly IModel channel;

        private readonly IBookingRepository _booking;

        public QueueListener()
        {
            //this._booking = booking;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            QueueConsumer();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            connection.Close();
            return Task.CompletedTask;
        }

        public bool QueueConsumer()
        {
            string message = "";
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
                    //message = JsonConvert.DeserializeObject<Ticket>(data);
                    //model.FlightNo = message.FlightNo;
                    //model.FromPlace = message.FromPlace;
                    //model.StartTime = message.StartTime;
                    message = Convert.ToString(data);
                }

                var obj = new BookingController(_booking);
                //var status = obj.CancelTicketByAirline(message);
            };

            channel.BasicConsume("blockflights", true, consumer);

            return true;
        }

        public async Task<bool> CancelTicketByAirline(string id)
        {
            return (_booking.CancelTicketByAirline(id));
        }
    }
}
