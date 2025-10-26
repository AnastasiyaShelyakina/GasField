using GasField.DTOs;
using GasField.Models;
using Microsoft.EntityFrameworkCore;

namespace GasField.Data.Services
{
    public class UserService : IUserService
    {
        private readonly ApiContext _context;
        public UserService(ApiContext context)
        {
            _context=context;
        }
        private static UserDTO UserToDto(User user) =>
            new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                PersonalNumber = user.PersonalNumber,
                UsernameId = user.UsernameId,
            };

        public async Task<User> Add(UserDTO userDto)
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Age = userDto.Age,
                PersonalNumber = userDto.PersonalNumber,
                UsernameId= userDto.UsernameId
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;

        }

        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null) 
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserUNDto>> GetAll()
        {
            var user = await _context.Users.Select(u=> new UserUNDto()
            { 
                FirstName=u.FirstName,
                LastName=u.LastName,
                Age = u.Age,    
                PersonalNumber = u.PersonalNumber,
                Username=u.Username.Name,

            }).ToListAsync();
            return user;
        }

        public async Task<UserUNDto> GetById(int id)
        {
            var user = await _context.Users.Where(u=>u.Id==id).Select(u => new UserUNDto()
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Age = u.Age,
                PersonalNumber = u.PersonalNumber,
                Username = u.Username.Name,

            }).FirstOrDefaultAsync();
            return user;
        }

        public async Task<UserDTO> Update(int id, UserDTO userDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null) 
            { 
                user.FirstName = user.FirstName;
                user.LastName = user.LastName;
                user.Age = userDto.Age;
                user.PersonalNumber = userDto.PersonalNumber;
                user.UsernameId= userDto.UsernameId;

                await _context.SaveChangesAsync();
            }
            return userDto;

        }
    }
}
