using AutoMapper;
using CotoBirthDayBooking.Application.DTOs;
using CotoBirthDayBooking.Application.Interfaces;
using CotoBirthDayBooking.Domain.Interfaces;
using CotoBirthDayBooking.Domain.Models;

public class ReservaService : IReservaService
{
    private readonly IReservaRepository _repository;
    private readonly IMapper _mapper;

    // Lock estático para controlar concurrencia en memoria
    // Bloqueo para evitar que dos personas reserven el mismo horario al mismo tiempo.
    // Mientras una reserva está siendo procesada, las demás esperan su turno.
    private static readonly object _lock = new object();

    public ReservaService(IReservaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ReservaResponse> CrearReservaAsync(Reserva reserva)
    {
        var inicio = reserva.Fecha.Date + reserva.HoraInicio;
        var fin = reserva.Fecha.Date + reserva.HoraFin;

        var inicioConMargen = inicio.AddMinutes(-30);
        var finConMargen = fin.AddMinutes(30);

        lock (_lock) // Bloquea para evitar concurrencia
        {
            // Validar solapamiento con margen
            var existeSolapamiento = _repository.ExisteReservaSolapadaAsync(inicioConMargen, finConMargen, reserva.SalonId).GetAwaiter().GetResult();
            if (existeSolapamiento)
                throw new Exception("La reserva se solapa con otra existente o no respeta los 30 minutos de margen.");

            // Generar reserva
            var creada = _repository.GenerarReservaAsync(reserva).GetAwaiter().GetResult();

            // Mapear y devolver
            return _mapper.Map<ReservaResponse>(creada);
        }
    }

    public async Task<IEnumerable<ReservaResponse>> ObtenerReservasPorFechaAsync(DateTime fecha)
    {
        var reservas = await _repository.ObtenerPorFechaAsync(fecha);
        return _mapper.Map<IEnumerable<ReservaResponse>>(reservas);
    }
}
