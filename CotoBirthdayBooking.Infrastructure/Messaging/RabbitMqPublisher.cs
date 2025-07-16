using System.Text;
using System.Text.Json;
using CotoBirthDayBooking.Domain.Messaging;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace CotoBirthDayBooking.Infrastructure.Messaging
{
    /// <summary>
    /// Publisher de mensajes para la cola "reserva-creada" en RabbitMQ.
    /// Esta implementación se registra como Singleton ya que solo publica mensajes
    /// y no depende de servicios Scoped como DbContext.
    /// </summary>
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;

        public RabbitMqPublisher(IConfiguration configuration)
        {
            var factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMq:Host"] ?? "localhost",
                Port = int.Parse(configuration["RabbitMq:Port"] ?? "5672"),
                UserName = configuration["RabbitMq:Username"] ?? "guest",
                Password = configuration["RabbitMq:Password"] ?? "guest",
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: "reserva-creada",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }

        public void PublishMessage<T>(T message)
        {
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(
                exchange: "",
                routingKey: "reserva-creada",
                basicProperties: null,
                body: body
            );
        }

        // Opcional: liberar recursos
        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
