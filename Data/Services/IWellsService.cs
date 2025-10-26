using GasField.DTOs;
using GasField.Models;

namespace GasField.Data.Services
{
    public interface IWellsService
    {
        Task<IEnumerable<WellDto>> GetAll();
        Task<WellDto> GetById(int id);
        Task<WellDto> Update(int id, WellDto wellDto);
        Task<Well> Add(WellDto wellDto);
        Task Delete(int id);
    }
}
