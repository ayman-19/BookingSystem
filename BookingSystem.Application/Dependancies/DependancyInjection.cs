using BookingSystem.Application.Behevior;
using BookingSystem.Application.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.Application.Dependancies
{
	public static class DependancyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependancyInjection).Assembly));
			services.AddValidatorsFromAssembly(typeof(DependancyInjection).Assembly);
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(BeheviorValidation<,>));
			services.AddTransient<CustomMiddleware>();
			return services;
		}
	}
}
