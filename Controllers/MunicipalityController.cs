using Microsoft.AspNetCore.Mvc;
using MunicipalityTax.Dtos;
using MunicipalityTax.Services;

namespace MunicipalityTax.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MunicipalityController : ControllerBase
    {
        private readonly MunicipalityService _municipalityService;

        public MunicipalityController(MunicipalityService municipality)
        {
            _municipalityService = municipality;
        }

        // Get all Municipality
        [HttpGet]
        public async Task<IActionResult> GetAllMunicipality()
        {
            var municipaities = await _municipalityService.GetAllMunicipalitiesAsync();
            return Ok(municipaities);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMunicipality([FromBody] AddMunicipalityRequestDto municipality)
        {
            await _municipalityService.AddMunicipalityAsync(municipality);
            return Created();
        }
    }
}
