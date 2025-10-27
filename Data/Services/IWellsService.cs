using GasField.DTOs;
using GasField.Models;

namespace GasField.Data.Services
{
    public interface IWellsService
    {
        Task<IEnumerable<WellDto>> GetAll();
        Task<WellDto> GetById(int id);
        Task<WellDto> Update(int id, UpdateWellDto wellDto);
        Task<WellDto> Add(UpdateWellDto wellDto);
        Task Delete(int id);
        Task<IEnumerable<WellDto>> GetHighWaterCutByUkpg(int ukpgId, double threshold);
        //Task<IEnumerable<WellDto>> GetTopWellsByExtraction(int topCount);
        Task<IEnumerable<WellDto>> GetTopWellsByExtraction(int topCount, int ukpgId);

    }
}
