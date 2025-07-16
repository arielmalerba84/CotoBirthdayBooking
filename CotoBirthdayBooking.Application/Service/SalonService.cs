using AutoMapper;
using CotoBirthDayBooking.Application.DTOs;
using CotoBirthDayBooking.Application.Interfaces;
using CotoBirthDayBooking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotoBirthDayBooking.Application.Service
{
    public class SalonService : ISalonService
    {
        private readonly ISalonRepository _repository;
        private readonly IMapper _mapper;

        public SalonService(ISalonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SalonResponse>> ObtenerTodosAsync()
        {
            var salones = await _repository.ObtenerTodosAsync();
            return _mapper.Map<IEnumerable<SalonResponse>>(salones);
        }
    }
}
