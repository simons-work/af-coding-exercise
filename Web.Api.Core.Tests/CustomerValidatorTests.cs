using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

        #region LastName tests

        // Switching to the simplified form of the FluentValidation tests

        [TestMethod]
        public void Should_not_have_error_when_LastName_specified()
        {
            _sut.ShouldNotHaveValidationErrorFor(c => c.LastName, "Evans");
        }

        [TestMethod]
        public void Should_have_error_when_LastName_is_null()
        {
            _sut.ShouldHaveValidationErrorFor(c => c.LastName, null as string).WithErrorCode("NotNullValidator");
        }

        [TestMethod]
        public void Should_have_error_when_LastName_too_short()
        {
            _sut.ShouldHaveValidationErrorFor(c => c.LastName, "AA").WithErrorCode("LengthValidator");
        }

        [TestMethod]
        public void Should_have_error_when_LastName_exceeds_max_length()
        {
            _sut.ShouldHaveValidationErrorFor(c => c.LastName, new string('A', 51)).WithErrorCode("LengthValidator");
        }

        #endregion

        #region PolicyNumber tests

        [TestMethod]
        public void Should_not_have_error_when_PolicyNumber_specified()
        {
            _sut.ShouldNotHaveValidationErrorFor(c => c.PolicyNumber, "AB-123456");
        }

        [TestMethod]
        public void Should_have_error_when_PolicyNumber_is_null()
        {
            _sut.ShouldHaveValidationErrorFor(c => c.PolicyNumber, null as string).WithErrorCode("NotNullValidator");
        }

        [TestMethod]
        public void Should_have_error_when_PolicyNumber_is_not_correct_pattern()
        {
            _sut.ShouldHaveValidationErrorFor(c => c.PolicyNumber, "AB-12345").WithErrorCode("RegularExpressionValidator");
        }

        [TestMethod]
        public void Should_have_error_when_PolicyNumber_is_another_incorrect_pattern()
        {
            _sut.ShouldHaveValidationErrorFor(c => c.PolicyNumber, "AB12345").WithErrorCode("RegularExpressionValidator");
        }

        [TestMethod]
        public void Should_have_error_when_PolicyNumber_is_not_correct_case()
        {
            _sut.ShouldHaveValidationErrorFor(c => c.PolicyNumber, "ab-123456").WithErrorCode("RegularExpressionValidator");
        }

        #endregion

        #region DateOfBirth tests

        [TestMethod]
        public void Should_not_have_error_when_DateOfBirth_18_years_ago()
        {
            _sut.ShouldNotHaveValidationErrorFor(c => c.DateOfBirth, DateTime.Today.AddYears(-18));
        }

        [TestMethod]
        public void Should_have_error_when_DateOfBirth_less_than_18_years_ago()
        {
            _sut.ShouldHaveValidationErrorFor(c => c.DateOfBirth, DateTime.Today.AddYears(-18).AddDays(1));
        }

        #endregion

        #region Email tests

        [TestMethod]
        public void Should_not_have_error_when_Email_has_correct_uk_suffix()
        {
            _sut.ShouldNotHaveValidationErrorFor(c => c.Email, "simon@test.co.uk");
        }

        [TestMethod]
        public void Should_not_have_error_when_Email_has_correct_com_suffix()
        {
            _sut.ShouldNotHaveValidationErrorFor(c => c.Email, "simon@test.com");
        }

        [TestMethod]
        public void Should_have_error_when_Email_has_incorrect_suffix()
        {
            _sut.ShouldHaveValidationErrorFor(c => c.Email, "simon@test.net").WithErrorMessage("'Email' must end with '.co.uk', or '.com' suffix.");
        }

        [TestMethod]
        public void Should_have_error_when_Email_is_null_and_DateOfBirth_missing()
        {
            var model = new CustomerDto { Email = null };
            var result = _sut.TestValidate(model);
            result.ShouldHaveValidationErrorFor(c => c.Email).WithErrorMessage("'Email' must end with '.co.uk', or '.com' suffix.");
        }

        [TestMethod]
        public void Should_not_have_error_when_Email_is_null_and_DateOfBirth_specified()
        {
            // This is an odd one, but when Date of Birth specified, then Email is optional so want to test that it doesn't complain about lack of suffix on a null email
            var model = new CustomerDto { Email = null, DateOfBirth = DateTime.Today.AddYears(-18) };
            var result = _sut.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(c => c.Email);
        }

        #endregion

        #region DateOfBirth optionality tests

        [TestMethod]
        public void Should_not_have_error_when_DateOfBirth_missing_but_Email_specified()
        {
            var model = new CustomerDto { Email = "test@test.co.uk" };
            var result = _sut.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(c => c.DateOfBirth);
        }

        [TestMethod]
        public void Should_not_have_error_when_DateOfBirth_and_Email_specified()
        {
            var model = new CustomerDto { DateOfBirth = DateTime.Today.AddYears(-18), Email = "test@test.co.uk" };
            var result = _sut.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(c => c.DateOfBirth);
        }


        [TestMethod]
        public void Should_have_error_when_DateOfBirth_and_Email_missing()
        {
            var model = new CustomerDto { };
            var result = _sut.TestValidate(model);
            result.ShouldHaveValidationErrorFor(c => c.DateOfBirth).WithErrorCode("NotNullValidator");
        }

        #endregion

        #region Email optionality tests

        [TestMethod]
        public void Should_not_have_error_when_Email_missing_but_DateOfBirth_specified()
        {
            var model = new CustomerDto { DateOfBirth = DateTime.Today.AddYears(-18) };
            var result = _sut.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(c => c.Email);
        }

        [TestMethod]
        public void Should_not_have_error_when_Email_and_DateOfBirth_specified()
        {
            var model = new CustomerDto { Email = "test@test.co.uk", DateOfBirth = DateTime.Today.AddYears(-18) };
            var result = _sut.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(c => c.Email);
        }


        [TestMethod]
        public void Should_have_error_when_Email_and_DateOfBirth_missing()
        {
            var model = new CustomerDto();
            var result = _sut.TestValidate(model);
            result.ShouldHaveValidationErrorFor(c => c.Email).WithErrorCode("NotNullValidator");
        }

        #endregion
    }
}
