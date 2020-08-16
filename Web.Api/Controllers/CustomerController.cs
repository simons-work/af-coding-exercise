using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
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
        /// <returns>New customer Id if the customer can be registered</returns>
        [HttpPost]
        public async Task<ActionResult> Create(CustomerDto customerDto)
        {
            try
            {
                var customerId = await customerService.CreateAsync(customerDto);
                return customerId > 0 ?
                    Ok(new { CustomerId = customerId }) :
                    BadRequest($"Cannot register as customer already exists with email '{customerDto.Email}'") as ActionResult;
            }
            catch (Exception ex)
            {
                logger.LogError("CustomerController.Create() exception:", ex);
                throw;
            }
        }

        private readonly ILogger<CustomersController> logger;
        private readonly ICustomerService customerService;
    }
}
