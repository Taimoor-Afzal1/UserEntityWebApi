using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserEntity.Dtos;
using UserEntity.Model;

namespace UserEntity.Service
{
    public class UserService : IUserService
    {
        private readonly UserDbContext _context;
        public readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserService(UserDbContext context, IMapper mapper, ILogger logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<User>> GetAll()
        {
            return (await _context.Users.ToListAsync());
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user is null)
                {
                    throw new ArgumentException("No user exists!");
                }
                else
                {
                    return user;
                }
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
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return user;
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
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == updateUser.Id);

                if (user is null)
                {
                    throw new ArgumentException("No user exists!");
                }

                user.FirstName = updateUser.FirstName;
                user.LastName = updateUser.LastName;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch(Exception ex)
            {
                _logger.LogError($"{0}, {1}", ex.Message, ex);
                throw;
            }
        }

        public async Task<List<User>> DelUserById(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (user is null)
                {
                    throw new ArgumentException("No user exists!");
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return (await _context.Users.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError($"{0}, {1}", ex.Message, ex);
                throw;
            }
        }
    }
}
