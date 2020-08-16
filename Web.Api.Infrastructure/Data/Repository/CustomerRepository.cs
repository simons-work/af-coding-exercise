using System.Linq;
using Web.Api.Infrastructure.Data.Entities;

namespace Web.Api.Infrastructure.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerRepository(InsuranceDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(CustomerEntity customerEntity)
        {
            ctx.Customers.Add(customerEntity);
        }

        public bool SaveChanges()
        {
            return ctx.SaveChanges() > 0;
        }

        public CustomerEntity GetCustomerByEmail(string email)
        {
            if (email == null) return null;
            var customer = ctx.Customers.Where(c => c.Email == email).FirstOrDefault();
            return customer;
        }

        private readonly InsuranceDbContext ctx;
    }
}
