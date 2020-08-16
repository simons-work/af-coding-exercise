using System;

namespace Web.Api.Core.Models
{
    public class CustomerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PolicyNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }
    }
}
