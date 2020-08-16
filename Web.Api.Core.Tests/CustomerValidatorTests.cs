using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Api.Core.Models;
using Web.Api.Core.Validators;

namespace Web.Api.Core.Tests
{
    /// <summary>
    /// Unit tests for CustomerValidator SUT (Subject under test)
    /// </summary>
    [TestClass]
    public class CustomerValidatorTests
    {
        private CustomerDtoValidator _sut;

        [TestInitialize]
        public void Setup()
        {
            _sut = new CustomerDtoValidator();
        }

        [TestMethod]
        public void Should_not_have_error_when_FirstName_specified()
        {
            var model = new CustomerDto { FirstName = "Simon" };
            var result = _sut.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(c => c.FirstName);
        }

        [TestMethod]
        public void Should_have_error_when_FirstName_is_null()
        {
            var model = new CustomerDto();
            var result = _sut.TestValidate(model);
            result.ShouldHaveValidationErrorFor(c => c.FirstName).WithErrorCode("NotNullValidator");
        }

        [TestMethod]
        public void Should_have_error_when_FirstName_too_short()
        {
            var model = new CustomerDto { FirstName = "AA" };
            var result = _sut.TestValidate(model);
            result.ShouldHaveValidationErrorFor(c => c.FirstName).WithErrorCode("LengthValidator");
        }

        [TestMethod]
        public void Should_have_error_when_FirstName_exceeds_max_length()
        {
            var model = new CustomerDto { FirstName = new string('A', 51) };
            var result = _sut.TestValidate(model);
            result.ShouldHaveValidationErrorFor(c => c.FirstName).WithErrorCode("LengthValidator");
        }

    }
}
