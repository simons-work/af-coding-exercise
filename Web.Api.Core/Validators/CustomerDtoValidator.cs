using FluentValidation;
using Web.Api.Core.Models;

namespace Web.Api.Core.Validators
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull().Length(3, 50);
        }
    }
}
