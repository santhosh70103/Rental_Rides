
using Microsoft.EntityFrameworkCore;
using Rental_Rides.IRepo;
using Rental_Rides.Models;
using Rental_Rides.Services;

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

            builder.Services.AddDbContext<CarRentalDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));
            builder.Services.AddScoped<IReturnService, ReturnService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
