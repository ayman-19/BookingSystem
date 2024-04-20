using BookingSystem.Application.Dependancies;
using BookingSystem.Application.Middleware;
using BookingSystem.Presintation.Endpoints.Categories;
using BookingSystem.Presintation.Endpoints.Reservations;
using BookingSystem.Presintation.Endpoints.Rooms;
using BookingSystem.Presintation.Endpoints.User;
using BookingSystem.Presistance.Dependancies;
using BookingSystem.Presistance.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BookingSystem.Presintation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplication().AddPresistance(builder.Configuration.GetConnectionString("myconnection")!);
            builder.Services.Configure<jWTSettings>(builder.Configuration.GetSection("jWTSettings"));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();


            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking System ", Version = "v1" });
                options.DescribeAllParametersInCamelCase();
                options.InferSecuritySchemes();
            });
            builder.Services.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                        new string[]{}
                            }
                        });
            });
            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["jWTSettings:Issuer"],
                        ValidAudience = builder.Configuration["jWTSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jWTSettings:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<CustomMiddleware>();
            app.MapUserEntpoints();
            app.MapRoomEntpoints();
            app.MapReservationEntpoints();
            app.MapCategoryEntpoints();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
