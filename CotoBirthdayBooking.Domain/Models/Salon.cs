using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotoBirthDayBooking.Domain.Models
{
    namespace CotoBirthdayBooking.Domain.Models
    {
        public class Salon
        {
            public int Id { get; set; }         
            public string Nombre { get; set; }   

            // Navegación a reservas
            public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        }
    }

}
