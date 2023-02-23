using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserEntity.Dtos;
using UserEntity.GenericRepository;
using UserEntity.Model;
using UserEntity.Service.UserServices;

namespace UserEntity.Service.UserServices.UserServicesImplementation
{
    public class UserService : IUserService
    {
        private readonly UserDbContext _context;
        public readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IGenericRepository<User> _repository;

        public UserService(UserDbContext context, ILogger<UserService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _repository = new GenericRepository<User>(_context);
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _repository.GetAll();
            return users;
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                var user = await _repository.GetById(id);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{0}, {1}", ex.Message, ex);
                throw;
            }
        }
        public async Task<User> AddNewUser(AddUserDto newUser)
        {
            try
            {
                var user = _mapper.Map<User>(newUser);
                var insertedUser = await _repository.Insert(user);
                await _repository.Save();
                return insertedUser;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{0}, {1}", ex.Message, ex);
                throw;
            }
        }

        public async Task<User> UpdateOldUser(User updateUser)
        {
            try
            {
                var user = await _repository.GetById(updateUser.Id);

                if (user != null)
                {
                    user.FirstName = updateUser.FirstName;
                    user.LastName = updateUser.LastName;
                    await _repository.Update(user);
                    await _repository.Save();
                    return user;
                }
                else
                {
                    throw new ArgumentException("Nothing Found!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{0}, {1}", ex.Message, ex);
                throw;
            }
        }

        public async Task DelUserById(int id)
        {
            try
            {
                await _repository.Delete(id);
                await _repository.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{0}, {1}", ex.Message, ex);
                throw;
            }
        }
    }
}
