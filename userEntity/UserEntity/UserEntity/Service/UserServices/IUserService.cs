using UserEntity.Dtos;
using UserEntity.Model;

namespace UserEntity.Service.UserServices
{
    public interface IUserService
    {
        Task<PaginationDto<User>> GetAll(int Currentpage, int PageItems);
        Task<PaginationDto<User>> SearchUser(string name, int Currentpage, float PageItems, string filterCol, string orderby);
        Task<User> GetUserById(int id);
        Task<User> AddNewUser(AddUserDto newUser);
        Task<User> UpdateOldUser(User newUser);
        Task DelUserById(int id);
    }
}
