using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Web.Tests.Attributes {
	[TestClass]
	public class PhoneValidatorClientTests : PhoneValidatorTestsBase {
		private static TestContext _testContext;
		private JavaScriptTestHelper _jsHelper;

		[ClassInitialize]
		public static void ClassInit(TestContext context) {
			_testContext = context;
		}

		[TestInitialize]
		public void TestInit() {
			_jsHelper = new JavaScriptTestHelper(_testContext);
			_jsHelper.LoadFile(@".\Attributes\jsUnit.js");
			_jsHelper.LoadFile(@".\Scripts\PhoneValidator.js");
		}

		protected override void PerformChecks(IList<Tuple<bool, string>> checks) {
			foreach (var check in checks) {
				Assert.AreEqual(check.Item1, _jsHelper.ExecuteMethod("isValidPhone", check.Item2),
				"Phone {0} should be {1}valid", check.Item2, check.Item1 ? "" : "in");
			}
		}
	}
}
