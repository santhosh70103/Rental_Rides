using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Rental_Rides.Models
{
    public class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
            : base(options)
        {
        }


        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Rented_Car> Rented_Cars { get; set; }
        public DbSet<Returned_Car> Returned_Cars { get; set; }
        public DbSet<User_Feedback> User_Feedbacks { get; set; }
        public DbSet<Car_Details> Car_Details { get; set; }
        public DbSet<Refund> Refunds { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Order>()
                .HasOne(o => o.Rented_Car)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.Rental_Id);


            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Rented_Car)
                .WithMany(r => r.Payments)
                .HasForeignKey(p => p.Rental_Id);


            modelBuilder.Entity<Rented_Car>()
                .HasOne(r => r.Car_Details)
                .WithMany(c => c.Rented_Car)
                .HasForeignKey(r => r.Car_Id);

            modelBuilder.Entity<Rented_Car>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rented_Car)
                .HasForeignKey(r => r.Customer_ID);


            modelBuilder.Entity<Returned_Car>()
                .HasOne(rc => rc.Rented_Car)
                .WithMany(r => r.Returned_Car)
                .HasForeignKey(rc => rc.Rental_Id);


            modelBuilder.Entity<User_Feedback>()
                .HasOne(f => f.Car_Details)
                .WithMany(c => c.User_Feedback)
                .HasForeignKey(p => p.Car_Id);

            modelBuilder.Entity<User_Feedback>()
                .HasOne(uf => uf.Customer)
                .WithMany(c => c.User_Feedback)
                .HasForeignKey(uf => uf.Customer_Id);
        }
    }
}
