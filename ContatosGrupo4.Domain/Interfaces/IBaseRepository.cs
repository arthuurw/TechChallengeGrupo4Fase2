namespace ContatosGrupo4.Domain.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetByIdAsync(String login);

    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(String login);
}