using Camp.Interfaces;
using Camp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;


namespace Web.Tests.Controllers
{
    [TestClass]
    public class CampControllerTest
    {
        private static ICampDB _campDB;
        private static CampController _controller;


        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _campDB = new CampDB();
            _controller = new CampController(_campDB);
        }


        [TestMethod]
        public void Campers_NullIdRedirectsToIndex()
        {
            new InvalidIdChecks<CampModel>(_controller).NullIdRedirectsToIndex();
        }
    }
}
