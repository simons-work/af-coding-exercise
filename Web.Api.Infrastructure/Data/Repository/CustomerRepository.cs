using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Infrastructure.Data.Entities;

namespace Web.Api.Infrastructure.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerRepository(InsuranceDbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task CreateAsync(CustomerEntity customerEntity)
        {
            await ctx.Customers.AddAsync(customerEntity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<CustomerEntity> GetCustomerByEmailAsync(string email)
        {
            if (email == null) return null;
            var customer = await ctx.Customers.Where(c => c.Email == email).FirstOrDefaultAsync();
            return customer;
        }

        private readonly InsuranceDbContext ctx;
    }
}
