using AutoMapper;
using CotoBirthDayBooking.Application.DTOs;
using CotoBirthDayBooking.Domain.Models.CotoBirthdayBooking.Domain.Models;
using CotoBirthDayBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotoBirthDayBooking.Application.MappingProfile
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            // Mapea entidad Reserva <-> DTO
            CreateMap<Reserva, ReservaResponse>();

            // Mapea entidad Salon <-> DTO
            CreateMap<Salon, SalonResponse>();

            // Mapea ReservaRequest a Reserva (para POST)
            CreateMap<ReservaRequest, Reserva>();
        }
    }
}
