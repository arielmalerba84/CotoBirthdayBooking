using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotoBirthDayBooking.Domain.Messaging
{
    // Interfaz para publicar mensajes en RabbitMQ
    public interface IRabbitMqPublisher
    {
        void PublishMessage<T>(T message);
    }
}
