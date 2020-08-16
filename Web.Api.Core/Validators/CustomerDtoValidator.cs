using FluentValidation;
using System;
using System.Linq;
using Web.Api.Core.Models;

namespace Web.Api.Core.Validators
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull().Length(3, 50);

            RuleFor(x => x.LastName)
                .NotNull().Length(3, 50);

            RuleFor(x => x.PolicyNumber)
                .NotNull().Matches(@"^[A-Z][A-Z]-\d{6,6}$"); // 2 Uppercase letters followed by 6 digits only

            RuleFor(x => x.Email)
                .NotNull().Unless(x => x.DateOfBirth != null)
                .WithMessage("'{PropertyName}' or 'Date Of Birth' must be specified.")
                .Matches(@"^\S{4,}@\S{2,}$") // at least 4 non whitespace chars before @ symbol, at least 2 non whitespace chars after
                .Must(email => HasCorrectSuffix(email, ".co.uk", ".com")).Unless(x => x.DateOfBirth != null && x.Email == null)
                .WithMessage("'{PropertyName}' must end with '.co.uk', or '.com' suffix.");

            RuleFor(x => x.DateOfBirth)
                .NotNull().Unless(x => x.Email != null)
                .WithMessage("'{PropertyName}' or 'Email' must be specified.")
                .Must(dob => AgeMustBeEqualOrGreaterThan(18, dob))
                .WithMessage("'{PropertyName}' indicates user is not aged 18 or over.");
        }

        public bool HasCorrectSuffix(string value, params string[] suffixes)
        {
            return suffixes.Any(suffix => value?.EndsWith(suffix, StringComparison.CurrentCultureIgnoreCase) ?? false);
        }

        public bool AgeMustBeEqualOrGreaterThan(int age, DateTime? dob)
        {
            return AgeMustBeEqualOrGreaterThan(age, dob, DateTime.Today);
        }
        public bool AgeMustBeEqualOrGreaterThan(int age, DateTime? dob, DateTime currentDate)
        {
            return dob == null || dob <= currentDate.AddYears(-age);
        }
    }
}
