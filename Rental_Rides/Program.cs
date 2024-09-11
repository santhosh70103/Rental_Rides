
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Rental_Rides.IRepo;
using Rental_Rides.Models;
using Rental_Rides.Services;
using System.Text;

namespace Rental_Rides
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddScoped<IBookingService, BookingService>();

            builder.Services.AddControllers()
           .AddJsonOptions(options =>
           {
               // This will use the property names as defined in the C# model
               options.JsonSerializerOptions.PropertyNamingPolicy = null;
           });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Interface Services
            builder.Services.AddDbContext<CarRentalDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));
            builder.Services.AddScoped<IReturnService, ReturnService>();
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IRentalService, RentalService>();
            builder.Services.AddScoped<IOrderService, OrderService>();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = false;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    //ValidIssuer = builder.Configuration["JwtSection:Issuer"],
                    //ValidAudience = builder.Configuration["JwtSection:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSection:Key"]!))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = ctx =>
                    {
                        ctx.Request.Cookies.TryGetValue("AuthToken", out var accessToken);
                        if (!string.IsNullOrEmpty(accessToken)) ctx.Token = accessToken;
                        return Task.CompletedTask;
                    }
                };
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            

            app.MapControllers();

            app.Run();
        }
    }
}
