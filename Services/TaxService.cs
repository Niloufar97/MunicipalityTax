using MunicipalityTax.Dtos;
using MunicipalityTax.Models;
using MunicipalityTax.Repositories;

namespace MunicipalityTax.Services
{

    public interface ITaxService
    {
        Task<decimal> GetTaxRate(string municipality, DateOnly date);
        Task AddTaxRecord(AddTaxRequestDto request);
    }
    public class TaxService : ITaxService
    {
        // Inject taxRepository for database comunication via dependency injection
        private readonly ITaxRepository _taxRepository;
       

        public TaxService(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }
        /// <summary>
        /// Gets the applicable tax rate for a given municipality on a specific date.
        /// </summary>
        public async Task<decimal> GetTaxRate(string municipality, DateOnly date)
        {
            var taxes = await _taxRepository.AllTaxesWithInputs(municipality, date);
            if (!taxes.Any())
            {
                throw new Exception("No tax records found for the specified municipality and date.");
            }

            var taxWithHighPriority = taxes.OrderBy(t => t.endDate.DayNumber - t.startDate.DayNumber).First(); //shorter period has higher priority
            return taxWithHighPriority.taxRate;
        }
        /// <summary>
        /// Add a new tax record to the database.
        /// </summary>
        public async Task AddTaxRecord(AddTaxRequestDto request)
        {
            // Check if the tax record exists
            if (await _taxRepository.TaxRecordExists(request))
            {
                throw new Exception("Tax record already exists for the specified municipality and date range.");
            }
            if (!await ValidateInputs(request))
            {
                throw new Exception("Inputs are incorrect!");
            }
            var newTaxRecord = new Tax
            {
                startDate = request.startDate,
                endDate = request.endDate,
                taxRate = request.taxRate,
                MunicipalityId = request.MunicipalityId,
            };
            await _taxRepository.AddTaxRecord(newTaxRecord);
        }

        // validate inputs for add a new tax record. can improved by returning more accureate error messages
        private async Task<bool> ValidateInputs(AddTaxRequestDto request)
        {
            //Check dates
            if (request.endDate < request.startDate)
                return false;
            //Check tax rate
            if (request.taxRate > 1 || request.taxRate < 0)
                return false;
            if (!await _taxRepository.MunicipalityExists(request.MunicipalityId))
                return false;
            return true;
        }
    }
}
