using CotoBirthDayBooking.Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotoBirthDayBooking.Domain.Interfaces
{
    public interface IReservaRepository
    {
        Task<bool> ExisteReservaSolapadaAsync(DateTime fechaInicio, DateTime fechaFin, int salonId);
        Task<Reserva> GenerarReservaAsync(Reserva reserva);
        Task<IEnumerable<Reserva>> ObtenerPorFechaAsync(DateTime fecha);
        Task<IDbContextTransaction> BeginTransactionAsync(System.Data.IsolationLevel isolationLevel);
    }
}
