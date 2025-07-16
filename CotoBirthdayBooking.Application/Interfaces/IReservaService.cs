using CotoBirthDayBooking.Application.DTOs;
using CotoBirthDayBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotoBirthDayBooking.Application.Interfaces
{
    public interface IReservaService
    {
        Task<ReservaResponse> CrearReservaAsync(Reserva reserva);
        Task<IEnumerable<ReservaResponse>> ObtenerReservasPorFechaAsync(DateTime fecha);
    }
}
