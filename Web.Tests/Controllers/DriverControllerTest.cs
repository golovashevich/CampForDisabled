using System;
using Camp.Interfaces;
using Camp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;

namespace Web.Tests.Controllers {
	[TestClass]
	public class DriverControllerTest : CampControllerTestBase<DriverModel> {
		[ClassInitialize]
		public static void ClassInit(TestContext context) {
			BaseClassInit(context);
		}

		#region	Camp controller specifics
		protected override ICampEntityController<DriverModel> CreateController() {
			return new DriverController(_campDB);
		}

		protected override void SetupEntityMock() {
			_mock.Setup(p => p.Drivers).Returns(_entityMock.Object);
		}

		protected override DriverModel CreateTestEntity(int newId) {
			return new DriverModel() {
				Id = newId,
				FirstName = "First Name" + newId,
				LastName = "Last Name" + newId,
				Contacts = "Contacts" + newId,
				Comments = "Comments" + newId,
				SitPlacesNum = newId,
				WheelchairsNum = newId
			};
		}

		protected override string ControllerName {
			get { return "Drivers"; }
		}

		protected override System.Data.Entity.IDbSet<DriverModel> Entities {
			get { return _campDB.Drivers; }
		}

		protected override void GenerateModelError() {
			_controller.ModelState.AddModelError("FirstName", new ArgumentOutOfRangeException("FirstName"));
		}
		#endregion
	}
}
