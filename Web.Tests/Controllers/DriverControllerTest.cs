using Camp.Interfaces;
using Camp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;

namespace Web.Tests.Controllers
{
    [TestClass]
    public class DriverControllerTest
    {
        private static ICampDB _campDB;
        private static DriverController _controller; 

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _campDB = new CampDB();
            _controller = new DriverController(_campDB);
        }

        [TestMethod]
        public void Drivers_NullIdRedirectsToIndex()
        {
            new InvalidIdChecks<DriverModel>(_controller).NullIdRedirectsToIndex();
        }
    }
}
