using Microsoft.AspNetCore.Mvc;
using MunicipalityTax.Dtos;
using MunicipalityTax.Services;

namespace MunicipalityTax.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxService _taxService;
        public TaxController(ITaxService taxService)
        {
            _taxService = taxService;
        }
        /// <summary>
        /// get tax rate for a specific municipality and date
        /// the shorter date range has higher priority
        /// </summary>
        /// <param name="municipality"></param>
        /// <param name="date"></param>
        /// <returns></returns>
   
        [HttpGet]
        public async Task<IActionResult> GetTaxRate([FromQuery] string municipality, [FromQuery] DateOnly date)
        {
            try
            {
                var taxRate = await _taxService.GetTaxRate(municipality, date);
                return Ok(taxRate);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Add a new tax record to the database.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddTaxRecord([FromBody] AddTaxRequestDto request)
        {
            try
            {
                await _taxService.AddTaxRecord(request);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    };
}
