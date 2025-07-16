using AutoMapper;
using CotoBirthDayBooking.Application.DTOs;
using CotoBirthDayBooking.Application.Interfaces;
using CotoBirthDayBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;


namespace CotoBirthDayBooking.Api.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ReservaController : ControllerBase
        {
            private readonly IReservaService _reservaService;
            private readonly IMapper _mapper;

            public ReservaController(IReservaService reservaService, IMapper mapper)
            {
                _reservaService = reservaService;
                _mapper = mapper;
            }

            [HttpPost]
            public async Task<IActionResult> CrearReserva([FromBody] ReservaRequest request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var reserva = _mapper.Map<Reserva>(request);
                var resultado = await _reservaService.CrearReservaAsync(reserva);
                return Ok(resultado);
            }

            [HttpGet("{fecha}")]
            public async Task<IActionResult> ObtenerReservasPorFecha(DateTime fecha)
            {
                var reservas = await _reservaService.ObtenerReservasPorFechaAsync(fecha);
                return Ok(reservas);
            }
        }
    }

