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
            try
            {
                var municipaities = await _municipalityService.GetAllMunicipalitiesAsync();
                return Ok(municipaities);
            }
            catch
            {
                return StatusCode(500 ,"An unexpected error occurred.");
            }
            
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMunicipality([FromBody] AddMunicipalityRequestDto municipality) 
        {
            try
            {
                await _municipalityService.AddMunicipalityAsync(municipality);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
