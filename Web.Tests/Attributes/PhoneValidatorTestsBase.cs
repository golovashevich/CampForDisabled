using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Validation.Tests;

namespace Web.Tests.Attributes {
	[TestClass]
	[Ignore]
	public abstract class PhoneValidatorTestsBase {
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

		[TestMethod]
		public void NullPhone() {
			PerformChecks(new Checks<bool, string>() { { true, null } });
		}

		[TestMethod]
		public void EmptyPhone() {
			PerformChecks(new Checks<bool, string>() { { true, "" } });
		}

		protected abstract void PerformChecks(IList<Tuple<bool, string>> checks);
	}
}
