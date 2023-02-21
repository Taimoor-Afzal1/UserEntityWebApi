using UserEntity.Dtos;
using UserEntity.Model;

namespace UserEntity.Service
{
    public interface IUserService
    {
        Task<ServiceResponse<List<User>>> GetAll();
        Task<ServiceResponse<User>> GetUserById(int id);
        Task<ServiceResponse<List<User>>> AddNewUser(AddUserDto newUser);
        Task<ServiceResponse<List<User>>> UpdateOldUser(User newUser);
        Task<ServiceResponse<List<User>>> DelUserById(int id);
    }
}
