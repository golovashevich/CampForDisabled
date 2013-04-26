using Camp.Interfaces;
using Camp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;

namespace Web.Tests.Controllers
{
    [TestClass]
    public class CamperControllerTest
    {
        private static ICampDB _campDB;
        private static CamperController _controller;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _campDB = new CampDB();
            _controller = new CamperController(_campDB);
        }


        [TestMethod]
        public void Campers_NullIdRedirectsToIndex()
        {
            new InvalidIdChecks<CamperModel>(_controller).NullIdRedirectsToIndex();
        }


		[TestMethod]
		public void StripPhoneNumberReturnsNullWhenParameterIsNull()
		{
			var info = typeof(CamperController).GetMethod("StripPhoneNumber");
			string result = (string) info.Invoke(_controller, new[] { (string)null });

			Assert.IsNull(result, "StripPhoneNumber must return null if parameter is null");
		}

    }
}

