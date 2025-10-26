using GasField.DTOs;
using GasField.Models;
using Microsoft.EntityFrameworkCore;

namespace GasField.Data.Services
{
    public class UkpgService : IUkpgsService
    {
        private readonly ApiContext _context;
        public UkpgService(ApiContext context)
        {
            _context = context;

        }
        private static UkpgDto UkpgToDto(Ukpg ukpg) =>
            new UkpgDto
            {
                MonthlyProduction=ukpg.MonthlyProduction,
                WellCount =ukpg.WellCount

            };

        public async Task<Ukpg> Add(UkpgDto ukpgDto)
        {
            var ukpg = new Ukpg
            {
                MonthlyProduction = ukpgDto.MonthlyProduction,
                WellCount = ukpgDto.WellCount
            };
            _context.Ukpgs.Add(ukpg);
            await _context.SaveChangesAsync();
            return ukpg;
        }

        public async Task Delete(int id)
        {
            var ukpg = await _context.Ukpgs.FindAsync(id);
            if (ukpg != null) 
            { 
             _context.Ukpgs.Remove(ukpg);
             await _context.SaveChangesAsync();
            }
        }



        public async Task<IEnumerable<UkpgDto>> GetAll()
        {
            var ukpg = await _context.Ukpgs.Select(u => new UkpgDto()
            {

                MonthlyProduction = u.MonthlyProduction,
                WellCount = u.WellCount,
                Wells = u.Wells.ToList(),

            }).ToListAsync();
            return ukpg;

        }
        public async Task<UkpgDto> GetById(int id)
        {
            var ukpg = await _context.Ukpgs.Where(u => u.Id == id).Select(u => new UkpgDto()
            {
                MonthlyProduction = u.MonthlyProduction,
                WellCount = u.WellCount,
                Wells = u.Wells.ToList(),
            }).FirstOrDefaultAsync();
            return ukpg;
        }

        public async Task<UkpgDto> Update(int id, UkpgDto ukpgDto)
        {
            var ukpg = await _context.Ukpgs.FindAsync(id);
            if (ukpg != null) 
            {
                ukpg.MonthlyProduction = ukpgDto.MonthlyProduction;
                ukpg.WellCount = ukpgDto.WellCount;

                await _context.SaveChangesAsync();
            }
            return ukpgDto;
        }
    }

}
