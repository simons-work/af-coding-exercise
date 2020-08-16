using System.Threading.Tasks;
using Web.Api.Core.Models;

namespace Web.Api.Core.Services
{
    public interface ICustomerService
    {
        Task<int> CreateAsync(CustomerDto customerDto);
    }
}