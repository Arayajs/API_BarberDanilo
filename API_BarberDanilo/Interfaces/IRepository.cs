namespace API_BarberDanilo.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity); // No necesita ser async
        void Delete(T entity); // No necesita ser async
        Task<int> SaveChangesAsync(); // Para persistir cambios
    }
}
