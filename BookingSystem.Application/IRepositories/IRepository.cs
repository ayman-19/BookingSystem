using System.Linq.Expressions;

namespace BookingSystem.Application.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, string[] includes = default!, bool astracking = default);
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = default!, string[] includes = default!, bool astracking = default);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);

        Task<bool> IsAnyExistAsync(Expression<Func<TEntity, bool>> pridecate);
        Task<int> CountAsync();
    }
}
