using CotoBirthDayBooking.Domain.Models.CotoBirthdayBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotoBirthDayBooking.Domain.Interfaces
{
    public interface ISalonRepository
    {
        Task<IEnumerable<Salon>> ObtenerTodosAsync();
    }
}
