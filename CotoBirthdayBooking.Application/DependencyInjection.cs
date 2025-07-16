using CotoBirthDayBooking.Application.Interfaces;
using CotoBirthDayBooking.Application.MappingProfile;
using CotoBirthDayBooking.Application.Service;
using CotoBirthDayBooking.Application.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;


namespace CotoBirthDayBooking.Application
{


    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IReservaService,ReservaService>();
            services.AddScoped<ISalonService, SalonService>();
            services.AddValidatorsFromAssemblyContaining<ReservaRequestValidator>();
            services.AddAutoMapper(typeof(Mappings).Assembly); // Registra AutoMapper
            return services;
        }
    }
}
