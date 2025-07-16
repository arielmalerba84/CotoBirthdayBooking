using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotoBirthDayBooking.Application.DTOs
{
    // DTO que representa la petición para crear una reserva
    public class ReservaRequest
    {
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int SalonId { get; set; }
    }
}
