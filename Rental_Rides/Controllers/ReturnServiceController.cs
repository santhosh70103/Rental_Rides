﻿using Microsoft.AspNetCore.Mvc;
using Rental_Rides.IRepo;
using System;
using System.Threading.Tasks;

namespace Rental_Rides.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnServiceController : ControllerBase
    {
        private readonly IReturnService _returnService;

        public ReturnServiceController(IReturnService returnService)
        {
            _returnService = returnService;
        }

        [HttpPost("{rentalId}")]
        public async Task<IActionResult> ReturnCar(int rentalId)
        {
            
            var result = await _returnService.ReturnCarAsync(rentalId, DateTime.Now);

            if (!result)
            {
                return NotFound(); // Or appropriate error response
            }

            return NoContent();
        }
    }
}