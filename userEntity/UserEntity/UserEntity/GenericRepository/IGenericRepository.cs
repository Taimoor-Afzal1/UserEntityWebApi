using System.Linq.Expressions;
using UserEntity.Dtos;
using UserEntity.Model;

namespace UserEntity.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<PaginationDto<T>> GetAll(int Currentpage, float PageItems);

        Task<PaginationDto<T>> SearchItem(string name, int Currentpage, float PageItems, Expression<Func<User, bool>> filter,
            string orderby);
        Task<T> GetById(object id);
        Task<T> Insert(T obj);
        Task Update(T obj);
        Task Delete(object id);
        Task Save();
    }
}
