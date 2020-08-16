﻿using Mapster;
using System;
using Web.Api.Core.Models;
using Web.Api.Infrastructure.Data.Entities;
using Web.Api.Infrastructure.Data.Repository;

namespace Web.Api.Core.Services
{
    public class CustomerService : ICustomerService
    {
        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        /// <summary>
        /// Register (/create) new customer.
        /// </summary>
        /// <param name="customerDto">New customer details.</param>
        /// <returns>-1 if customer was already registerd at that email address if specified, otherwise the customerId of the new customer registration</returns>
        /// <remarks>It is unlikely but possible for two or more people to share the same name and birthdate but assuming
        /// email address would be expected to be globally unique so if customer already exists then a new one is not created so that correct 400 status can be returned by the Web API layer</remarks>
        public int Create(CustomerDto customerDto)
        {
            var customerEntity = customerDto.Adapt<CustomerEntity>();

            if (customerRepository.GetCustomerByEmail(customerEntity.Email) != null) return -1;

            customerRepository.Create(customerEntity);
            var changesSaved = customerRepository.SaveChanges();
            if (!changesSaved) throw new ApplicationException("CustomerService.SaveChanges() unexpected result. The number of state entries written to the database was zero");

            return customerEntity.Id;
        }

        private readonly ICustomerRepository customerRepository;
    }
}
