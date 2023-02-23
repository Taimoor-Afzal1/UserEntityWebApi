
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UserEntity.Model;
using UserEntity.Service.UserServices.UserServicesImplementation;

namespace UserEntity.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly UserDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(UserDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            try
            {
                var item = await _dbSet.FindAsync(id);
                if (item is null)
                {
                    throw new ArgumentException("Nothing Found!");
                }
                else
                {
                    return item;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> Insert(T obj)
        {
            try
            {
                var newItem = await _dbSet.AddAsync(obj);
                return obj;
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(T obj)
        {
            try
            {
                _dbSet.Attach(obj);
                _context.Entry(obj).State = EntityState.Modified;
            }
            catch
            {
                throw;
            }
            
        }

        public async Task Delete(object id)
        {
            try
            {
                T? existingItem = await _dbSet.FindAsync(id);
                if (existingItem != null)
                {
                    _dbSet.Remove(existingItem);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch
            {
                throw;
            }
            
        }

        public async Task Save()
        {
             await _context.SaveChangesAsync();
        }
    }
}
