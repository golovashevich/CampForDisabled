using System;
using Camp.Interfaces;
using Camp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;


namespace Web.Tests.Controllers {
	[TestClass]
	public class CampControllerTest : CampControllerTestBase<CampModel> {
		[ClassInitialize]
		public static void ClassInit(TestContext context) {
			BaseClassInit(context);
		}

		#region	Camp controller specifics
		protected override ICampEntityController<CampModel> CreateController() {
			return new CampController(_campDB);
		}

		protected override void SetupEntityMock() {
			_mock.Setup(p => p.Camps).Returns(_entityMock.Object);
		}

		protected override CampModel CreateTestEntity(int newId) {
			return new CampModel() {
				Id = newId,
				History = "History" + newId,
				Name = "Name" + newId,
				Description = "Description" + newId,
				BeginDate = DateTime.Now.AddDays(-1),
				EndDate = DateTime.Now.AddDays(1),
				Theme = "Theme" + newId
			};
		}

		protected override string ControllerName {
			get { return "Camps"; }
		}

		protected override System.Data.Entity.IDbSet<CampModel> Entities {
			get { return _campDB.Camps; }
		}

		protected override void GenerateModelError() {
			_controller.ModelState.AddModelError("Name", new ArgumentOutOfRangeException("Name"));
		}
		#endregion
	}
}
