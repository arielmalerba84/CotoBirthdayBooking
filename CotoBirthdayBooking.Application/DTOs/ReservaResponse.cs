using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotoBirthDayBooking.Application.DTOs
{
    // DTO que representa la respuesta después de crear o consultar una reserva
    public class ReservaResponse
    {
        public int Id { get; set; }
        public int SalonId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}
