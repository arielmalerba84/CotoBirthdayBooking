using CotoBirthDayBooking.Domain.Interfaces;
using CotoBirthDayBooking.Domain.Models;
using CotoBirthDayBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotoBirthDayBooking.Infrastructure.Repository
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly AppDbContext _context;
        public ReservaRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ExisteReservaSolapadaAsync(DateTime fechaInicio, DateTime fechaFin, int salonId)
        {
            // Traemos todas las reservas del mismo salón y fecha, pero sin traer toda la tabla(filtramos solo por salonId y fecha)
            var reservas = await _context.Reservas
                .Where(r => r.SalonId == salonId && r.Fecha == fechaInicio.Date)
                .ToListAsync(); // Traemos a memoria para poder usar las propiedades calculadas

                    // Ahora chequeamos si alguna reserva se solapa con la nueva reserva
                    return reservas.Any(r =>
                        r.FechaHoraInicio < fechaFin &&
                        r.FechaHoraFin > fechaInicio
                    );
        }
        

        public async Task<Reserva> GenerarReservaAsync(Reserva reserva)
        {
           // var connString = _context.Database.GetConnectionString();
            //Console.WriteLine($"Cadena de conexión usada: {connString}");

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            return reserva;
        }

        public async Task<IEnumerable<Reserva>> ObtenerPorFechaAsync(DateTime fecha)
        {
            return await _context.Reservas
              .Where(r => r.Fecha == fecha.Date)
              .ToListAsync();
        }

        // Inicia una transacción para controlar la concurrencia,
        // evitando que se creen reservas solapadas al mismo tiempo.
        // Solo permite que una reserva se procese y las demás esperan.
        public async Task<IDbContextTransaction> BeginTransactionAsync(System.Data.IsolationLevel isolationLevel)
        {
            return await _context.Database.BeginTransactionAsync(isolationLevel);
        }
    }
}
