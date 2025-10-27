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
        /*private static UkpgDto UkpgToDto(Ukpg ukpg) =>
            new UkpgDto
            {
                Id = ukpg.Id,
                MonthlyProduction=ukpg.MonthlyProduction,
                WellCount =ukpg.WellCount

            };*/

        private static UkpgDto UkpgToDto(Ukpg ukpg) =>
            new UkpgDto
            {
                Id = ukpg.Id,
                Name = ukpg.Name,
                WellCount = ukpg.Wells?.Count ?? 0,
                AverageExtraction = ukpg.Wells?.Any() == true ? ukpg.Wells.Average(w => w.Extraction) : 0,
                Wells = ukpg.Wells?.Select(w => new WellDto
                {
                    Id = w.Id,
                    Extraction = w.Extraction
                }).ToList()
            };


        /*        private static UkpgDto UkpgToDto(Ukpg ukpg) =>
                    new UkpgDto
                    {
                        Id = ukpg.Id,
                        Name = ukpg.Name,
                        WellCount = ukpg.WellCount,
                        AverageExtraction = ukpg.AverageExtraction,
                        Wells = ukpg.Wells
                    };
        */
        /*        public async Task<UkpgDto> Add(UpdateUkpgDto ukpgDto)
                {
                    var ukpg = new Ukpg
                    {
                        MonthlyProduction = ukpgDto.MonthlyProduction,
                        WellCount = ukpgDto.WellCount
                    };

                    _context.Ukpgs.Add(ukpg);
                    await _context.SaveChangesAsync();

                    // возвращаем DTO, а не саму модель
                    return UkpgToDto(ukpg);
                }*/

        public async Task<UkpgDto> Add(UpdateUkpgDto ukpgDto)
        {
            var ukpg = new Ukpg
            {
                Name = ukpgDto.Name
            };

            _context.Ukpgs.Add(ukpg);
            await _context.SaveChangesAsync();

            return UkpgToDto(ukpg);
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



        /*public async Task<IEnumerable<UkpgDto>> GetAll()
        {
            var ukpg = await _context.Ukpgs.Select(u => new UkpgDto()
            {
                Id=u.Id,
                MonthlyProduction = u.MonthlyProduction,
                WellCount = u.WellCount,
                Wells = u.Wells.ToList(),

            }).ToListAsync();
            return ukpg;

        }*/

        public async Task<IEnumerable<UkpgDto>> GetAll()
        {
            var ukpgs = await _context.Ukpgs
                .Include(u => u.Wells)
                .ToListAsync();

            return ukpgs.Select(UkpgToDto);
        }
        /*public async Task<UkpgDto> GetById(int id)
        {
            var ukpg = await _context.Ukpgs.Where(u => u.Id == id).Select(u => new UkpgDto()
            {   Id = u.Id,
                MonthlyProduction = u.MonthlyProduction,
                WellCount = u.WellCount,
                Wells = u.Wells.ToList(),
            }).FirstOrDefaultAsync();
            return ukpg;
        }*/
        public async Task<UkpgDto?> GetById(int id)
        {
            var ukpg = await _context.Ukpgs
                .Include(u => u.Wells)
                .FirstOrDefaultAsync(u => u.Id == id);

            return ukpg == null ? null : UkpgToDto(ukpg);
        }

        /*public async Task<UkpgDto> Update(int id, UpdateUkpgDto ukpgDto)
        {
            var ukpg = await _context.Ukpgs.FindAsync(id);
*//*            if (ukpg == null)
                throw new Exception($"УКПГ с ID {id} не найден.");*//*

            ukpg.MonthlyProduction = ukpgDto.MonthlyProduction;
            ukpg.WellCount = ukpgDto.WellCount;

            await _context.SaveChangesAsync();

            return UkpgToDto(ukpg);
        }*/
        public async Task<UkpgDto?> Update(int id, UpdateUkpgDto ukpgDto)
        {
            var ukpg = await _context.Ukpgs.FindAsync(id);
            if (ukpg == null)
                return null;

            ukpg.Name = ukpgDto.Name;

            await _context.SaveChangesAsync();

            // Подгружаем скважины для расчета AverageExtraction
            await _context.Entry(ukpg).Collection(u => u.Wells!).LoadAsync();

            return UkpgToDto(ukpg);
        }

        }

}
