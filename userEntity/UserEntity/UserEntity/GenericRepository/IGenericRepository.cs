namespace UserEntity.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(object id);
        Task<T> Insert(T obj);
        Task Update(T obj);
        Task Delete(object id);
        Task Save();
    }
}
