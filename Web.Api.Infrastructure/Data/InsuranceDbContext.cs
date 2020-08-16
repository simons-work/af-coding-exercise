using Microsoft.EntityFrameworkCore;
using Web.Api.Infrastructure.Data.Entities;

namespace Web.Api.Infrastructure.Data
{
    public class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options) { }

        public DbSet<CustomerEntity> Customers { get; set; }
    }
}

/* Reminder for updating database e.g.
    cd Web.Api.Infrastructure
    dotnet ef -s ..\Web.Api\  migrations add InitialDb  -o Data\Migrations  
    dotnet ef -s ..\Web.Api\  database update
*/
