using UserEntity.Dtos;

namespace UserEntity.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<PaginationDto<T>> GetAll(int Currentpage, float PageItems);
        Task<T> GetById(object id);
        Task<T> Insert(T obj);
        Task Update(T obj);
        Task Delete(object id);
        Task Save();
    }
}
