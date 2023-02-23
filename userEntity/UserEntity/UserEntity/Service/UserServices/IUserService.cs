using UserEntity.Dtos;
using UserEntity.Model;

namespace UserEntity.Service.UserServices
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetUserById(int id);
        Task<User> AddNewUser(AddUserDto newUser);
        Task<User> UpdateOldUser(User newUser);
        Task DelUserById(int id);
    }
}
