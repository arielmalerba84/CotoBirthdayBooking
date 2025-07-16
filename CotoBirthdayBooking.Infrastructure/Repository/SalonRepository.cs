using CotoBirthDayBooking.Domain.Interfaces;
using CotoBirthDayBooking.Domain.Models.CotoBirthdayBooking.Domain.Models;
using CotoBirthDayBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotoBirthDayBooking.Infrastructure.Repository
{
    public class SalonRepository : ISalonRepository
    {
        private readonly AppDbContext _context;

        public SalonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Salon>> ObtenerTodosAsync()
        {
            return await _context.Salones.ToListAsync();
        }
    }
}
