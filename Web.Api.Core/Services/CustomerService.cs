using Mapster;
using System;
using Web.Api.Core.Models;
using Web.Api.Infrastructure.Data.Entities;
using Web.Api.Infrastructure.Data.Repository;

namespace Web.Api.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        /// <summary>
        /// Register new customer.
        /// </summary>
        /// <param name="customerDto">New customer details.</param>
        public int Create(CustomerDto customerDto)
        {
            var customerEntity = customerDto.Adapt<CustomerEntity>();

            customerRepository.Create(customerEntity);
            var changesSaved = customerRepository.SaveChanges();
            if (!changesSaved) throw new ApplicationException("CustomerService.SaveChanges() unexpected result. The number of state entries written to the database was zero");

            return customerEntity.Id;
        }
    }
}
