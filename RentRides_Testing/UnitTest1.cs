using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Rental_Rides.Controllers;
using Rental_Rides.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rental_Rides.DTO_Models;

namespace RentRides_Testing
{
    [TestFixture]
    public class AdminsControllerTests
    {
        private AdminsController _controller;
        private CarRentalDbContext _context;
        private IConfiguration _configuration;

        
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CarRentalDbContext>()
                            .UseInMemoryDatabase(databaseName: "CarRentalTestDb")
                            .Options;

            _context = new CarRentalDbContext(options);

            // Add some dummy data for admins with all required fields
            _context.Admins.Add(new Admin
            {
                Admin_ID = 1,
                Admin_Name = "Admin1",
                Admin_Email = "admin1@test.com",
                Admin_Password = "password123",
                Admin_PhoneNo = "1234567890",  // Ensure all required fields are set
                Role = "Admin"
            });
            _context.Admins.Add(new Admin
            {
                Admin_ID = 2,
                Admin_Name = "Admin2",
                Admin_Email = "admin2@test.com",
                Admin_Password = "password456",
                Admin_PhoneNo = "0987654321",  // Ensure all required fields are set
                Role = "Admin"
            });
            _context.SaveChanges();

            var inMemorySettings = new Dictionary<string, string> {
    {"JwtSection:Key", "ThisIsASecretKeyThatIsAtLeast32CharsLong"},
    {"JwtSection:Issuer", "TestIssuer"},
    {"JwtSection:Audience", "TestAudience"}
};



            _configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(inMemorySettings)
                            .Build();

            _controller = new AdminsController(_context, _configuration);
        }


        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AdminLogin_ValidCredentials_ReturnsSuccess()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "admin1@test.com",
                Password = "password123"
            };

            // Act
            var result = await _controller.LoginValidation(loginDto) as ObjectResult;
            var response = result.Value as GeneralResponse;

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(response.flag, Is.True);
                Assert.That(response.Message, Is.EqualTo("Login Successfull "));
                Assert.That(response.token, Is.Not.Null);
                Assert.That(response.Id, Is.EqualTo(1));
            });
        }

        [Test]
        public async Task AdminLogin_InvalidPassword_ReturnsUnsuccessful()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "admin1@test.com",
                Password = "wrongpassword"
            };

            // Act
            var result = await _controller.LoginValidation(loginDto) as ObjectResult;
            var response = result.Value as GeneralResponse;

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(response.flag, Is.False);
                Assert.That(response.Message, Is.EqualTo("Login UnSuccessfull"));
                Assert.That(response.token, Is.EqualTo(string.Empty));
                Assert.That(response.Id, Is.EqualTo(0));
            });
        }

        [Test]
        public async Task AdminLogin_InvalidEmail_ReturnsUnsuccessful()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "nonexistent@test.com",
                Password = "password123"
            };

            // Act
            var result = await _controller.LoginValidation(loginDto) as ObjectResult;
            var response = result.Value as GeneralResponse;

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(response?.flag, Is.False);
            });
            Assert.Multiple(() =>
            {
                Assert.That(response?.Message, Is.EqualTo("Login UnSuccessfull"));
                Assert.That(response.token, Is.EqualTo(string.Empty));
                Assert.That(response.Id, Is.EqualTo(0));
            });
        }
    }
}