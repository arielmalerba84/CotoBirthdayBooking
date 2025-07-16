using CotoBirthDayBooking.Domain.Models.CotoBirthdayBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotoBirthDayBooking.Domain.Models
{
    public class Reserva
    {
        public int Id { get; set; }                   // Identificador único (PK)
        public DateTime Fecha { get; set; }           // Fecha del cumpleaños
        public TimeSpan HoraInicio { get; set; }      // Hora inicio reserva
        public TimeSpan HoraFin { get; set; }         // Hora fin reserva
        public int SalonId { get; set; }        // FK a Salon
        public Salon Salon { get; set; }        // Navegación

        // Propiedad calculada que combina Fecha + HoraInicio
        [NotMapped]
        public DateTime FechaHoraInicio => Fecha.Date + HoraInicio;

        // Propiedad calculada que combina Fecha + HoraFin
        [NotMapped]
        public DateTime FechaHoraFin => Fecha.Date + HoraFin;
    }
}

