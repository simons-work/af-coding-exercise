using System.Threading.Tasks;
using Web.Api.Infrastructure.Data.Entities;

namespace Web.Api.Infrastructure.Data.Repository
{
    public interface ICustomerRepository
    {
        Task CreateAsync(CustomerEntity customerEntity);

        Task<bool> SaveChangesAsync();

        Task<CustomerEntity> GetCustomerByEmailAsync(string email);

    }
}