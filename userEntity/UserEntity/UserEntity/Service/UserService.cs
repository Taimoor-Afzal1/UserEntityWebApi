using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserEntity.Dtos;
using UserEntity.Model;

namespace UserEntity.Service
{
    public class UserService : IUserService
    {
        private readonly UserDbContext _Context;

        public readonly IMapper _mapper;

        public UserService(UserDbContext context, IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }

        public async Task<List<User>> GetAll()
        {
            return (await _Context.Users.ToListAsync());
        }

        public async Task<ServiceResponse<User>> GetUserById(int id)
        {
            var serviceResponse = new ServiceResponse<User>();
            serviceResponse.Data = await _Context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (serviceResponse.Data == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User Not found";
            }
            else
            {
                serviceResponse.Message = "Data Fetched Successfully!";
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<User>>> AddNewUser(AddUserDto newUser)
        {
            var user = _mapper.Map<User>(newUser);
            await _Context.Users.AddAsync(user);
            await _Context.SaveChangesAsync();

            var serviceResponse = new ServiceResponse<List<User>>();
            serviceResponse.Data = await _Context.Users.ToListAsync();
            serviceResponse.Message = "New User Added!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<User>>> UpdateOldUser(User updateUser)
        {
            var serviceResponse = new ServiceResponse<List<User>>();
            try
            {
                var user = await _Context.Users.FirstOrDefaultAsync(u => u.Id == updateUser.Id);
                user.FirstName = updateUser.FirstName;
                user.LastName = updateUser.LastName;

                _Context.Users.Update(user);
                await _Context.SaveChangesAsync();

                serviceResponse.Data = await _Context.Users.ToListAsync();
                serviceResponse.Message = "User Updated!";
            }
            catch 
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User Not found";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<User>>> DelUserById(int id)
        {
            var serviceResponse = new ServiceResponse<List<User>>();
            try
            {
                var user = await _Context.Users.FirstOrDefaultAsync(u => u.Id == id);
                _Context.Users.Remove(user);
                await _Context.SaveChangesAsync();

                serviceResponse.Data = await _Context.Users.ToListAsync();
                serviceResponse.Message = "User Deleted!";
            }
            catch 
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User Not found";
            }

            return serviceResponse;
        }
    }
}
