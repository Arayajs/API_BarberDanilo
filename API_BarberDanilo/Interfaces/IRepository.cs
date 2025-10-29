namespace API_BarberDanilo.Interfaces
{
    /// <summary>
    /// Patrón Repository genérico - Cumple con los principios SOLID:
    /// - S: Responsabilidad única (solo acceso a datos)
    /// - O: Abierto/Cerrado (extensible sin modificación)
    /// - L: Sustitución de Liskov (cualquier implementación puede usarse)
    /// - I: Segregación de interfaces (interfaz específica y concisa)
    /// - D: Inversión de dependencias (depende de abstracción, no de implementación)
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Obtiene todas las entidades
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Obtiene una entidad por su ID
        /// </summary>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Agrega una nueva entidad
        /// </summary>
        Task AddAsync(T entity);

        /// <summary>
        /// Actualiza una entidad existente
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// Elimina una entidad
        /// </summary>
        void Delete(T entity);

        /// <summary>
        /// Persiste los cambios en la base de datos
        /// </summary>
        Task<int> SaveChangesAsync();
    }
}