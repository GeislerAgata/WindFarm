using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace FarmBack.Services
{
    public class RabbitMQConsumerService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string filePath = "messages.txt";

        public RabbitMQConsumerService()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost", // Podaj adres RabbitMQ
                Port = 5672,            // Port RabbitMQ
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queues = new List<string>
            {
                "wind_speed",
                "temperature",
                "vibrations",
                "noise"
            };

            //while (!stoppingToken.IsCancellationRequested)
            //{
                foreach (var queue in queues)
                {
                    var consumer = new EventingBasicConsumer(_channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        System.Diagnostics.Debug.WriteLine($"Received message from queue '{queue}': {message}"); 
                        //Console.WriteLine($"Received message from queue '{queue}': {message}");

                        File.AppendAllText(filePath, $"Queue: {queue}, Message: {message}\n");
                    };

                    _channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
                }
            //}

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
