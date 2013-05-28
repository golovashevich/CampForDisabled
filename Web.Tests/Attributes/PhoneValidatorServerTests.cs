using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomValidation.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Web.Tests.Attributes {
	[TestClass]
	public class PhoneValidatorServerTests : PhoneValidatorTestsBase {
		private static PhoneNumberAttribute _validator;
		private static ValidationContext _context;

		[TestInitialize]
		public void TestInit() {
			_validator = new PhoneNumberAttribute();
			_context = new ValidationContext(_validator);
		}
														
		protected override void PerformChecks(IList<Tuple<bool, string>> checks) {
			foreach (var check in checks) {
				Assert.AreEqual(check.Item1, null == _validator.GetValidationResult(check.Item2, _context),
				"Phone '{0}' should be {1}valid", check.Item2, check.Item1 ? "" : "in");
			}
		}
	}
}
