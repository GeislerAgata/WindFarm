using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using FarmBack.DTO;
using Newtonsoft.Json;

namespace FarmBack.Services
{
    public class RabbitMQConsumerService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string filePath = "messages.txt";
        private readonly SensorDataRepository _sensorDataRepository;

        public RabbitMQConsumerService()
        {
            var factory = new ConnectionFactory
            {
                HostName = "mqtt",
                Port = 1883,
                UserName = "guest",
                Password = "guest"
            };
            
            _sensorDataRepository = new SensorDataRepository("mongodb://mongodb:27017", "windfarm", "windfarm");

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

            foreach (var queue in queues)
            {
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    
                    var sensorData = JsonConvert.DeserializeObject<SensorData>(message);
                    _sensorDataRepository.InsertSensorData(sensorData);

                    //File.AppendAllText(filePath, $"Queue: {queue}, Message: {message}\n");
                };

                _channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}