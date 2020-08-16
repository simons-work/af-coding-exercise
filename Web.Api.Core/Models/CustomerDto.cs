using System;

namespace Web.Api.Core.Models
{
    /// <summary>
    /// Customer details.
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// Customer First Name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Customer Last Name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Customer Policy Number e.g. XX-123456.
        /// </summary>
        public string PolicyNumber { get; set; }

        /// <summary>
        /// Customer Date of Birth e.g. 2000-12-31.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Customer Email.
        /// </summary>
        public string Email { get; set; }
    }
}