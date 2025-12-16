using GasField.DTOs;
using GasField.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GasField.Data.Services
{
    public class WellsService : IWellsService
    {
        private readonly ApiContext _context;
        public WellsService(ApiContext context)
        {
            _context = context;

        }

        private static WellDto WellToDto(Well well) =>
            new WellDto
            {
                Id = well.Id,
                RoofGvk =well.RoofGvk,
                BottomGvk=well.BottomGvk,
                RoofPerforation=well.RoofPerforation,
                BottomPerforation=well.BottomPerforation,
                WaterCut = well.WaterCut,
                Extraction=well.Extraction,
                 
    };
        /*        public async Task<WellDto> Add(UpdateWellDto wellDto)
                {
                    var well = new Well()
                    {

                        RoofGvk = wellDto.RoofGvk,
                        BottomGvk = wellDto.BottomGvk,
                        RoofPerforation = wellDto.RoofPerforation,
                        BottomPerforation = wellDto.BottomPerforation,
                        Extraction = wellDto.Extraction,
                        UkpgId = wellDto.UkpgId,
                    };
                    _context.Wells.Add(well);
                    await _context.SaveChangesAsync();



                    return WellToDto(well);

                }*/
        public async Task<WellDto> Add(UpdateWellDto wellDto)
        {
            var well = new Well()
            {
                RoofGvk = wellDto.RoofGvk,
                BottomGvk = wellDto.BottomGvk,
                RoofPerforation = wellDto.RoofPerforation,
                BottomPerforation = wellDto.BottomPerforation,
                Extraction = wellDto.Extraction,
                UkpgId = wellDto.UkpgId,
            };
            _context.Wells.Add(well);
            await _context.SaveChangesAsync();

            return WellToDto(well);
        }


        public async Task Delete(int id)
        {
            var well = await _context.Wells.FindAsync(id);
            if (well != null) 
            {
            _context.Wells.Remove(well);
            await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<WellDto>> GetAll()
        {
            return await _context.Wells.Select(x => WellToDto(x)).ToListAsync();
        }

        public async Task<WellDto> GetById(int id)
        {
            var well = await _context.Wells.FindAsync(id);
            return WellToDto(well);

        }


        /*        public async Task<WellDto> Update(int id, UpdateWellDto wellDto)
                {
                    var well = await _context.Wells.FindAsync(id);
                    if (well != null)
                    { 
                    well.RoofGvk=wellDto.RoofGvk;
                    well.BottomGvk=wellDto.BottomGvk;
                    well.RoofPerforation=wellDto.RoofPerforation;
                    well.BottomPerforation= wellDto.BottomPerforation;
                    well.Extraction = wellDto.Extraction;
                    well.UkpgId=wellDto.UkpgId;
                    }
                    return WellToDto(well);
                }
        */
        /*        public async Task<WellDto?> Update(int id, UpdateWellDto wellDto)
                {
                    var well = await _context.Wells.FindAsync(id);
                    if (well == null)
                        return null;

                    well.RoofGvk = wellDto.RoofGvk;
                    well.BottomGvk = wellDto.BottomGvk;
                    well.RoofPerforation = wellDto.RoofPerforation;
                    well.BottomPerforation = wellDto.BottomPerforation;
                    well.Extraction = wellDto.Extraction;
                    well.UkpgId = wellDto.UkpgId;

                    await _context.SaveChangesAsync(); // 

                    return WellToDto(well);
                }
        */
        public async Task<WellDto?> Update(int id, UpdateWellDto wellDto)
        {
            var well = await _context.Wells.FindAsync(id);
            if (well == null) return null;

            well.RoofGvk = wellDto.RoofGvk;
            well.BottomGvk = wellDto.BottomGvk;
            well.RoofPerforation = wellDto.RoofPerforation;
            well.BottomPerforation = wellDto.BottomPerforation;
            well.Extraction = wellDto.Extraction;
            well.UkpgId = wellDto.UkpgId;

            await _context.SaveChangesAsync();
            return WellToDto(well);
        }
        public async Task<IEnumerable<WellDto>> GetHighWaterCutByUkpg(int ukpgId, double threshold)
        {
            var wells = await _context.Wells
                .Where(w => w.UkpgId == ukpgId)
                .ToListAsync();

            var filtered = wells
                .Where(w => w.WaterCut > threshold)
                .Select(w => new WellDto
                {
                    Id = w.Id,
                    RoofGvk = w.RoofGvk,
                    BottomGvk = w.BottomGvk,
                    RoofPerforation = w.RoofPerforation,
                    BottomPerforation = w.BottomPerforation,
                    Extraction = w.Extraction,
                    WaterCut = w.WaterCut,
                })
                .ToList();

            return filtered;
        }


        /*public async Task<IEnumerable<WellDto>> GetTopWellsByExtraction(int ukpgId, int thresho)
        {
            var wells = await _context.Wells
                .Where(w => w.UkpgId == ukpgId)      // фильтруем по УКПГ
                .OrderByDescending(w => w.Extraction) // сортировка по добыче
                .Take(thresho)                        // топ N
                .Select(w => new WellDto
                {
                    Extraction = w.Extraction,
                })
                .ToListAsync();

            return wells;
        }*/
        public async Task<IEnumerable<WellDto>> GetTopWellsByExtraction(int ukpgId, int count)
        {
            var wells = await _context.Wells
                .Where(w => w.UkpgId == ukpgId)
                .OrderByDescending(w => w.Extraction)
                .Take(count)
                .Select(w => new WellDto
                {
                    Id = w.Id,
                    RoofGvk = w.RoofGvk,
                    BottomGvk = w.BottomGvk,
                    RoofPerforation = w.RoofPerforation,
                    BottomPerforation = w.BottomPerforation,
                    Extraction = w.Extraction,
                    WaterCut = w.WaterCut
                })
                .ToListAsync();

            return wells;
        }



    }
}
