using CotoBirthDayBooking.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotoBirthDayBooking.Application.Interfaces
{
    public interface ISalonService
    {
        Task<IEnumerable<SalonResponse>> ObtenerTodosAsync();
    }
}
