using System.Linq;
using Web.Api.Infrastructure.Data.Entities;

namespace Web.Api.Infrastructure.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly InsuranceDbContext ctx;

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
    }
}
