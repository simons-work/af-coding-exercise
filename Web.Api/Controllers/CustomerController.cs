using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Web.Api.Core.Models;
using Web.Api.Core.Services;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        public CustomersController(ILogger<CustomersController> logger, ICustomerService customerService)
        {
            this.logger = logger;
            this.customerService = customerService;
        }

        /// <summary>
        /// Register (/create) new customer.
        /// </summary>
        /// <param name="customerDto">New customer details.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CustomerDto customerDto)
        {
            try
            {
                var customerId = customerService.Create(customerDto);
                return Ok(new { CustomerId = customerId });
            }
            catch (Exception ex)
            {
                logger.LogError("Customer Create error occurred", ex);
                throw;
            }
        }

        private readonly ILogger<CustomersController> logger;
        private readonly ICustomerService customerService;
    }
}
