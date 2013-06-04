using System;
using Camp.Interfaces;
using Camp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;

namespace Web.Tests.Controllers
{
    [TestClass]
    public class CamperControllerTest : CampControllerTestBase<CamperModel>	{
		[ClassInitialize]
		public static void ClassInit(TestContext context) {
			BaseClassInit(context);
		}

		#region	Camp controller specifics
		protected override ICampEntityController<CamperModel> CreateController() {
			return new CamperController(_campDB);
		}

		protected override void SetupEntityMock() {
			_mock.Setup(p => p.Campers).Returns(_entityMock.Object);
		}

		protected override CamperModel CreateTestEntity(int newId) {
			return new CamperModel() {
				Id = newId,
				FirstName = "FirstName" + newId,
				LastName = "LastName" + newId,
				DateOfBirth = DateTime.Now,
				CityId = 1,
				District = "District" + newId,		
				Street = "Street" + newId,
				HomeNumber = "Home #" + newId,
				AppartmentNumber = "Appt. #" + newId,
				HomePhone = "1234567890" + newId,
				AddressNote = "AddressNote" + newId,
				Contacts = "Contacts" + newId,
				Email = "Email" + newId,
				Skype = "Skype" + newId,
				DisabilityGrade = 0,
				MedicalNote = "MedicalNote" + newId,
				Comments = "Comments" + newId,
			};
		}

		protected override string ControllerName {
			get { return "Campers"; }
		}

		protected override System.Data.Entity.IDbSet<CamperModel> Entities {
			get { return _campDB.Campers; }
		}

		protected override void GenerateModelError() {
			_controller.ModelState.AddModelError("FirstName", new ArgumentOutOfRangeException("FirstName"));
		}
		#endregion


		[TestMethod]
		public void StripPhoneNumberReturnsNullWhenParameterIsNull()
		{
			var info = typeof(CamperController).GetMethod("StripPhoneNumber");
			string result = (string) info.Invoke(_controller, new[] { (string)null });

			Assert.IsNull(result, "StripPhoneNumber must return null if parameter is null");
		}

    }
}

