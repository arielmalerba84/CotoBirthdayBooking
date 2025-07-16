using CotoBirthDayBooking.Infrastructure.Messaging;
using CotoBirthDayBooking.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CotoBirthDayBooking.Domain.Interfaces;
using CotoBirthDayBooking.Infrastructure.Repository;
using CotoBirthDayBooking.Domain.Messaging;

namespace CotoBirthDayBooking.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Cambiar a InMemory para no depender de SQL Server ni Docker
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseInMemoryDatabase("BirthdayBookingDB"));

            // Repositorios
            services.AddScoped<ISalonRepository, SalonRepository>();
            services.AddScoped<IReservaRepository, ReservaRepository>();

            // Sacamos RabbitMQ, no lo necesitamos
            // services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
            // services.AddHostedService<RabbitMqConsumer>();

            return services;
        }
    }
}
