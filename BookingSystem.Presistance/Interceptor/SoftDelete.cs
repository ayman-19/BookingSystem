using BookingSystem.Domain.SoftDeletable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BookingSystem.Presistance.Interceptor
{
	public class SoftDelete : SaveChangesInterceptor
	{
		public override InterceptionResult<int> SavingChanges(
															   DbContextEventData eventData,
															   InterceptionResult<int> result)
		{
			if (eventData.Context is null)
				return result;

			foreach (var entry in eventData.Context.ChangeTracker.Entries())
			{
				if (entry is not { Entity: ISoftDelete entity, State: EntityState.Deleted })
					continue;

				entry.State = EntityState.Modified;
				entity.Delete();
			}
			return result;
		}
	}
}
