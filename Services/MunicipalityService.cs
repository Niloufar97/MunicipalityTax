using Microsoft.EntityFrameworkCore;
using MunicipalityTax.Data;
using MunicipalityTax.Dtos;
using MunicipalityTax.Exceptions;
using MunicipalityTax.Models;

namespace MunicipalityTax.Services
{
    public class MunicipalityService
    {
        private readonly AppDbContext _context;

        public MunicipalityService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Municipality>> GetAllMunicipalitiesAsync()
        {
            var allMunicipalities = await _context.Municipalities.ToListAsync();
            return allMunicipalities;
        }

        public async Task AddMunicipalityAsync(AddMunicipalityRequestDto request)
        {
            if (string.IsNullOrEmpty(request.MunicipalityName)) {
                throw new BadRequestException("Municipality name is required");
            }
            if (await _context.Municipalities.AnyAsync(m => m.MunicipalityName == request.MunicipalityName))
            {
                throw new BadRequestException("Municipality already exists");
            }
            var newMunicipality = new Municipality
            {
                MunicipalityName = request.MunicipalityName
            };
            await _context.Municipalities.AddAsync(newMunicipality);
            await _context.SaveChangesAsync();
        }
    }
}