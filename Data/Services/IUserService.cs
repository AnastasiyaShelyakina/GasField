using GasField.DTOs;
using GasField.Models;

namespace GasField.Data.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserUNDto>> GetAll();
        Task<UserUNDto> GetById(int id);
        Task<UserDTO> Update(int id, UserDTO userDto);

        Task<User> Add(UserDTO userDto);
        Task Delete(int id);
    }
}
