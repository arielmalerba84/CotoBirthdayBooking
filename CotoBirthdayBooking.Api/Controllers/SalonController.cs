using CotoBirthDayBooking.Application.DTOs;
using CotoBirthDayBooking.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CotoBirthDayBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonController : ControllerBase
    {
        private readonly ISalonService _salonService;

        public SalonController(ISalonService salonService)
        {
            _salonService = salonService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalonResponse>>> Get()
        {
            var salones = await _salonService.ObtenerTodosAsync();
            return Ok(salones);
        }
    }
}