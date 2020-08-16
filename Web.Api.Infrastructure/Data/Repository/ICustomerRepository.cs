using Web.Api.Infrastructure.Data.Entities;

namespace Web.Api.Infrastructure.Data.Repository
{
    public interface ICustomerRepository
    {
        void Create(CustomerEntity customerEntity);
        bool SaveChanges();
        CustomerEntity GetCustomerByEmail(string email);
    }
}