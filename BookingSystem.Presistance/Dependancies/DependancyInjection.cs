using BookingSystem.Application.IAuthentication;
using BookingSystem.Application.IRepositories;
using BookingSystem.Domain.Model;
using BookingSystem.Presistance.Authentication;
using BookingSystem.Presistance.Data;
using BookingSystem.Presistance.Helper;
using BookingSystem.Presistance.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.Presistance.Dependancies
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddPresistance(this IServiceCollection services, string strConnection)
        {
            services.AddDbContext<BookingDbContext>(opt => opt.UseSqlServer(strConnection));
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(url =>
            {
                var actionContext = url.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = url.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext!);
            });
            services.AddDataProtection();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISharedBetweenUserAndRoomRepository, UserRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<ISharedBetweenUserAndRoomRepository, RoomRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<IFloorRepository, FloorRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAccount, Account>();
            services.AddScoped(typeof(Cookie));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<BookingDbContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
            });
            services.AddAuthentication();
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            return services;
        }
    }
}
