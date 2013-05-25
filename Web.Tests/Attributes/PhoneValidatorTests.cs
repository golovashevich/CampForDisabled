using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomValidation.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Validation.Tests;

namespace Web.Tests.Attributes {
	[TestClass]
	public class PhoneValidatorTests {
		private static PhoneNumberAttribute _validator;
		private static ValidationContext _context;

		[TestInitialize]
		public void TestInit() {
			_validator = new PhoneNumberAttribute();
			_context = new ValidationContext(_validator);
		}

		[TestMethod]
		public void PhoneNumbersWithSymbols() {
			PerformChecks(new Checks<bool, string>() {
                { true, "+7 495 555-55-55" },
				{ true, "+7 (495) 555_55-55#" },
                { false, "+7 (495) 555d55—55#" },
				{ false, "+7 (495) 555-55{55#" }
            });
		}

		[TestMethod]
		public void PhoneNumbersWithPluses() {
			PerformChecks(new Checks<bool, string>() {
                { true, "+01234567 890" }, 
                { true, "+01 2345678901" }, 
                { false, "+ 0 12345 6789012" }, 
                { false, "+0123456789" }, 
                { false, "+01234" } 
            });
		}

		[TestMethod]
		public void PhoneNumberLength() {
			PerformChecks(new Checks<bool, string>()  {
                { true, "01234567890123456789" }, 
                { true, "01234" }, 
                { false, "0123" }, 
                { false, "012345678901234567890" } 
            });
		}

		private void PerformChecks(IList<Tuple<bool, string>> checks) {
			foreach (var check in checks) {
				Assert.AreEqual(check.Item1, null == _validator.GetValidationResult(check.Item2, _context),
				"Phone {0} should be {1}valid", check.Item2, check.Item1 ? "" : "in");
			}
		}

		[TestMethod]
		public void NullPhone() {
			Assert.IsNull(_validator.GetValidationResult(null, _context), "Null phone is valid");
		}

		[TestMethod]
		public void EmptyPhone() {
			Assert.IsNull(_validator.GetValidationResult(null, _context), "Empty phone is valid"); ;
		}
	}
}
