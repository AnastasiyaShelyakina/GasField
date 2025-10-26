using GasField.DTOs;
using GasField.Models;

namespace GasField.Data.Services
{
    public interface IUkpgsService
    {
        Task<IEnumerable<UkpgDto>> GetAll();
        Task<UkpgDto> GetById(int id);
        Task<UkpgDto> Update(int id, UkpgDto ukpgDto);
        Task<Ukpg> Add(UkpgDto ukpgDto);
        Task Delete(int id);
    }
}
