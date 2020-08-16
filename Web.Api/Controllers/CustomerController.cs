using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Web.Api.Core.Models;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        public CustomersController(ILogger<CustomersController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Register new customer.
        /// </summary>
        /// <param name="customerDto">New customer details.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CustomerDto customerDto)
        {
            try
            {
                var customerId = 1;
                return Ok(new { CustomerId = customerId });
            }
            catch (Exception ex)
            {
                logger.LogError("Customer Create error occurred", ex);
                throw;
            }
        }

        private readonly ILogger<CustomersController> logger;
    }
}
