using CotoBirthDayBooking.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotoBirthDayBooking.Application.Validator
{
    public class ReservaRequestValidator : AbstractValidator<ReservaRequest>
    {
        public ReservaRequestValidator()
        {
            // Hora fin debe ser mayor que hora inicio
            RuleFor(r => r.HoraFin)
                .GreaterThan(r => r.HoraInicio)
                .WithMessage("La hora fin debe ser mayor que la hora inicio.");

            // Hora inicio >= 09:00
            RuleFor(r => r.HoraInicio)
                .GreaterThanOrEqualTo(new TimeSpan(9, 0, 0))
                .WithMessage("La reserva debe comenzar después de las 09:00.");

            // Hora fin <= 18:00
            RuleFor(r => r.HoraFin)
                .LessThanOrEqualTo(new TimeSpan(18, 0, 0))
                .WithMessage("La reserva debe terminar antes de las 18:00.");
        }
    }

}
