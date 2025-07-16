using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CotoBirthDayBooking.Application.DTOs;

namespace CotoBirthDayBooking.Infrastructure.Messaging
{
    // Servicio en segundo plano (BackgroundService) que consume mensajes de RabbitMQ
    public class RabbitMqConsumer : BackgroundService
    {
        private readonly ILogger<RabbitMqConsumer> _logger;
        private readonly IConfiguration _configuration;

        private IConnection? _connection;   // Conexión con RabbitMQ
        private IModel? _channel;            // Canal para comunicarse con RabbitMQ
        private const string QueueName = "reserva-creada";  // Nombre de la cola que escucha

        // Constructor que recibe configuración para obtener datos del host RabbitMQ
        public RabbitMqConsumer(ILogger<RabbitMqConsumer> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        // Crea y configura la fábrica para la conexión RabbitMQ
        private ConnectionFactory CreateFactory() =>
            new ConnectionFactory
            {
                HostName = _configuration["RabbitMq:Host"] ?? "localhost",  // Host de RabbitMQ
                Port = int.Parse(_configuration["RabbitMq:Port"] ?? "5672"), // Puerto
                UserName = _configuration["RabbitMq:Username"] ?? "guest",  // Usuario
                Password = _configuration["RabbitMq:Password"] ?? "guest",  // Contraseña
                DispatchConsumersAsync = true,                              // Permite consumidores async
                AutomaticRecoveryEnabled = true,                            // Reconexión automática
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)         // Intervalo entre reintentos
            };

        // Inicializa la conexión y canal, declara la cola
        private void InitRabbitMq()
        {
            var factory = CreateFactory();

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Declara la cola para escuchar mensajes (si no existe, la crea)
            _channel.QueueDeclare(
                queue: QueueName,
                durable: false,    // La cola no persiste después de reiniciar RabbitMQ
                exclusive: false,  // La cola puede ser usada por otros consumidores
                autoDelete: false, // No se elimina automáticamente cuando el último consumidor se desconecta
                arguments: null);  // Sin argumentos especiales

            _logger.LogInformation("✅ RabbitMQ conectado y cola declarada.");
        }

        // Método principal que ejecuta el consumidor en segundo plano
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                // Ciclo que intenta mantener conexión activa hasta cancelar el token
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        // Si no hay conexión abierta, intenta conectar
                        if (_connection == null || !_connection.IsOpen)
                        {
                            _logger.LogInformation("Intentando conectar a RabbitMQ...");
                            InitRabbitMq();
                        }

                        // Crea un consumidor async para el canal
                        var consumer = new AsyncEventingBasicConsumer(_channel);

                        // Evento que se dispara cada vez que llega un mensaje a la cola
                        consumer.Received += async (model, ea) =>
                        {
                            try
                            {
                                // Obtiene el cuerpo del mensaje (byte[])
                                var body = ea.Body.ToArray();

                                // Convierte el array de bytes a string UTF8
                                var json = Encoding.UTF8.GetString(body);

                                // Deserializa el JSON al DTO ReservaResponse
                                var reserva = JsonSerializer.Deserialize<ReservaResponse>(json);

                                // Loguea que se recibió correctamente la reserva
                                _logger.LogInformation("Reserva recibida: Id {Id}, Salon {SalonId}, Fecha {Fecha}",
                                    reserva?.Id, reserva?.SalonId, reserva?.Fecha);

                                // Confirma que el mensaje fue procesado correctamente
                                _channel.BasicAck(ea.DeliveryTag, false);
                            }
                            catch (Exception ex)
                            {
                                // Si ocurre un error, loguea el error
                                _logger.LogError(ex, "Error procesando mensaje RabbitMQ");

                                // Indica que el mensaje NO fue procesado y pide reintentar (reenviar a la cola)
                                _channel.BasicNack(ea.DeliveryTag, false, true);
                            }
                        };

                        // Comienza a consumir mensajes de la cola sin auto-acknowledge
                        _channel.BasicConsume(QueueName, autoAck: false, consumer);

                        // Espera hasta que se cancele el token para detener el servicio
                        stoppingToken.WaitHandle.WaitOne();
                    }
                    catch (Exception ex)
                    {
                        // Si hay error de conexión, loguea y espera antes de reintentar
                        _logger.LogError(ex, "Error en conexión RabbitMQ, intentando reconectar en 5 segundos...");
                        Thread.Sleep(5000);
                    }
                }
            }, stoppingToken);
        }

        // Dispose para cerrar la conexión y canal cuando se termina el servicio
        public override void Dispose()
        {
            try
            {
                _channel?.Close();
                _connection?.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cerrando conexión RabbitMQ");
            }
            base.Dispose();
        }
    }
}
