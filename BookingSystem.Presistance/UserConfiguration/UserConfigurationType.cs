using BookingSystem.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystem.Presistance.UserConfiguration
{
	internal class UserConfigurationType : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");
			//builder.HasQueryFilter(u => !u.IsDeleted);
		}
	}
}
