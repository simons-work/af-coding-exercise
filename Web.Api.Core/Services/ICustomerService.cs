using Web.Api.Core.Models;

namespace Web.Api.Core.Services
{
    public interface ICustomerService
    {
        int Create(CustomerDto customerDto);
    }
}