using GasField.DTOs;
using GasField.Models;

namespace GasField.Data.Services
{

    public interface IUkpgsService
    {
        Task<IEnumerable<UkpgDto>> GetAll();
        Task<UkpgDto> GetById(int id);
        Task<UkpgDto> Update(int id, UpdateUkpgDto ukpgDto);
        Task<UkpgDto> Add(UpdateUkpgDto ukpgDto);
        Task Delete(int id);
    }
}
