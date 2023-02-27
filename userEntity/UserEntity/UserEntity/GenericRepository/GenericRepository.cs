
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using UserEntity.Dtos;
using UserEntity.Model;
using UserEntity.Service.UserServices.UserServicesImplementation;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

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

        public async Task<PaginationDto<T>> GetAll(int Currentpage, float PageItems)
        {
            var users =  await _dbSet
                .Skip((Currentpage - 1) * (int)PageItems)
                .Take((int)PageItems)
                .ToListAsync();

            var totalrecord = _dbSet.Count();
            var totalPages = Math.Ceiling(_dbSet.Count() / PageItems);

            var response = new PaginationDto<T>
            {
                Values = users,
                CurrentPage = Currentpage,
                TotalPages = totalPages,
                TotalRecords = totalrecord
            };
            return response;
        }

        public async Task<PaginationDto<T>> SearchItem(string name, int Currentpage, float PageItems, 
            Expression<Func<User, bool>> filter,string orderby)
        {
            try
            {
                IQueryable<T> query = _dbSet;

                query = query.Where(filter);

                    var queryPagin = query.OrderBy(orderby)
                        .Skip((Currentpage - 1) * (int)PageItems)
                        .Take((int)PageItems);


                    var totalrecord = query.Count();
                    var totalPages = Math.Ceiling(totalrecord / PageItems);

                    var response = new PaginationDto<T>
                    {
                        Values = await queryPagin.ToListAsync(),
                        CurrentPage = Currentpage,
                        TotalPages = totalPages,
                        TotalRecords = totalrecord
                    };
                    return response;
            }
            catch
            {
                throw;
            }
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
