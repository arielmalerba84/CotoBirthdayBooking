using CotoBirthDayBooking.Domain.Models;
using CotoBirthDayBooking.Domain.Models.CotoBirthdayBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CotoBirthDayBooking.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Salon> Salones { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}

   /* public class AppDbContext : DbContext
    {
        public DbSet<Salon> Salones { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Salón - Reserva (1 a muchos)
            modelBuilder.Entity<Salon>()
                .HasMany(s => s.Reservas)
                .WithOne(r => r.Salon)
                .HasForeignKey(r => r.SalonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}*/
