using System;

namespace Web.Api.Infrastructure.Data.Entities
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
    }
}
