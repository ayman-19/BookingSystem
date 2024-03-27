using BookingSystem.Application.IRepositories;
using BookingSystem.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingSystem.Presistance.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private readonly BookingDbContext _context;
		private readonly DbSet<TEntity> _entities;

		public Repository(BookingDbContext context)
		{
			_context = context;
			_entities = _context.Set<TEntity>();
		}
		public async Task AddAsync(TEntity entity) => await _entities.AddAsync(entity);

		public async Task<int> CountAsync() => await _entities.CountAsync();

		public Task DeleteAsync(TEntity entity) => Task.Run(() => _entities.Remove(entity));
		public async Task<bool> IsAnyExistAsync(Expression<Func<TEntity, bool>> pridecate) => await _entities.AnyAsync(pridecate);

		public Task UpdateAsync(TEntity entity) => Task.Run(() => _entities.Update(entity));

		public Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null!, string[] includes = null!, bool astracking = false)
		{
			var src = _entities.AsQueryable();

			if (!astracking)
				src = src.AsNoTracking();

			if (predicate != null)
				src = src.Where(predicate);

			if (includes != null)
				foreach (var include in includes)
					src = src.Include(include);

			return Task.FromResult(src);
		}
		public Task<IQueryable<TEntity>> GetPaginationAsync(int pageSize, int pageNumber, Expression<Func<TEntity, bool>> predicate = null!, string[] includes = null!)
		{
			var src = _entities.AsQueryable().AsNoTracking();

			if (predicate != null)
				src = src.Where(predicate);

			if (includes != null)
				foreach (var include in includes)
					src = src.Include(include);

			pageSize = pageSize == 0 ? 10 : pageSize;
			pageNumber = pageNumber == 0 ? 1 : pageNumber;

			return Task.FromResult(src.Skip((pageNumber - 1) * pageSize).Take(pageSize));
		}
		public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, string[] includes = null!, bool astracking = false)
		{
			var src = _entities.AsQueryable();
			if (!astracking)
				src = src.AsNoTracking();

			if (includes != null)
				foreach (var include in includes)
					src = src.Where(predicate).Include(include);
			return src.FirstAsync(predicate);
		}
	}
}
