using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Camp.Interfaces;
using Camp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Web.Controllers;

namespace Web.Tests.Controllers {
	[TestClass]
	public class DriverControllerTest {

		private static ICampDB _campDB;
		private static DriverController _controller;

		[ClassInitialize]
		public static void ClassInit(TestContext testContext) {
			var mock = new Mock<ICampDB>();

			var driversMock = SetupEntityListMock<DriverModel>();

			mock.Setup(p => p.Drivers).Returns(driversMock.Object);
			_campDB = mock.Object;
			_controller = new DriverController(_campDB);
		}

		private static Mock<IDbSet<T>> SetupEntityListMock<T>() where T : class {
			var entityMock = new Mock<IDbSet<T>>();
			var collection = new ObservableCollection<T>();
			var queryable = collection.AsQueryable();
			entityMock.SetupGet(p => p.ElementType).Returns(queryable.ElementType);
			entityMock.SetupGet(p => p.Expression).Returns(queryable.Expression);
			entityMock.Setup(p => p.GetEnumerator()).Returns(queryable.GetEnumerator());
			entityMock.SetupGet(p => p.Provider).Returns(queryable.Provider);
			entityMock.Setup(p => p.Add(It.IsAny<T>()))
					.Callback((T d) => collection.Add(d));
			entityMock.Setup(p => p.Remove(It.IsAny<T>())).Callback((T d) => collection.Remove(d));
			entityMock.SetupGet(p => p.Local).Returns(collection);

			return entityMock;
		}

		[TestInitialize]
		public void TestInit() {
			_campDB.Drivers.Local.Clear();
			for (int i = 0; i < 10; i++) {
				_campDB.Drivers.Add(CreateTestDriver(i));
			}
			_controller.ModelState.Clear();
		}

		private static DriverModel CreateTestDriver(int i) {
			return new DriverModel() {
				Id = i,
				FirstName = "First Name" + i,
				LastName = "Last Name" + i,
				Contacts = "Contacts" + i,
				Comments = "Comments" + i,
				SitPlacesNum = i,
				WheelchairsNum = i
			};
		}

		[TestMethod]
		public void Drivers_NullIdRedirectsToIndex() {
			new InvalidIdChecks<DriverModel>(_controller).NullIdRedirectsToIndex();
		}


		[TestMethod]
		public void Drivers_NonExistingIdRedirectsToIndex() {
			new InvalidIdChecks<DriverModel>(_controller).NonExistingIdRedirectsToIndex(Int32.MinValue);
		}


		[TestMethod]
		public void Drivers_Create_Get() {
			var result = _controller.Create();

			ControllerUtils.CheckForView(result, "Drivers", "Create");

			Assert.AreEqual(new DriverModel(), (result as ViewResult).Model);
		}

		[TestMethod]
		public void Drivers_Create_Post() {
			int oldCount = _campDB.Drivers.Count();
			int newId = oldCount + 1;
			var driver = CreateTestDriver(newId);

			var result = _controller.Create(driver);

			Assert.AreEqual(oldCount + 1, _campDB.Drivers.Count(), "Create action should add new entity");
			var savedDriver = _campDB.Drivers.SingleOrDefault(d => d.Id == newId);
			Assert.IsNotNull(savedDriver, "Driver does not seem to be added");
			Assert.IsTrue(driver.Equals(savedDriver), "Original and saved drivers differ");

			ControllerUtils.CheckForRedirectToIndex(result, "Drivers", "Create");
		}

		/// <summary>
		/// Create with invalid model does not add anything and returns to Create view
		/// </summary>
		[TestMethod]
		public void Drivers_Create_IndvalidModel() {
			int oldCount = _campDB.Drivers.Count();
			int newId = oldCount + 1;
			var driver = CreateTestDriver(newId);
			_controller.ModelState.AddModelError("FistName", new ArgumentOutOfRangeException("FirstName"));

			var result = _controller.Create(driver);

			Assert.AreEqual(oldCount, _campDB.Drivers.Count(), 
					"Create action with invalid model should not add anything");

			ControllerUtils.CheckForView(result, "Drivers", "Create");
		}

		[TestMethod]
		public void Drivers_Details() {
			var id = new Random().Next(_campDB.Drivers.Count());
			var expectedDriver = _campDB.Drivers.Single(d => d.Id == id);

			var result = _controller.Details(id);

			ControllerUtils.CheckForView(result, "Drivers", "Details");

			Assert.AreEqual(expectedDriver, (result as ViewResult).Model);
		}

		[TestMethod]
		public void Drivers_Edit_Get() {
			var id = new Random().Next(_campDB.Drivers.Count());
			var expectedDriver = _campDB.Drivers.Single(d => d.Id == id);

			var result = _controller.Edit(id);

			ControllerUtils.CheckForView(result, "Drivers", "Edit");
			Assert.AreEqual(expectedDriver, (result as ViewResult).Model);
		}

		[TestMethod]
		public void Drivers_Edit_Post() {
			var rand = new Random();
			var newDriver = CreateTestDriver(rand.Next(Int32.MaxValue));
			var id = rand.Next(_campDB.Drivers.Count());

			var result = _controller.Edit(id, newDriver);

			var updatedDriver = _campDB.Drivers.Single(d => d.Id == id);
			Assert.AreEqual(newDriver, updatedDriver);
			ControllerUtils.CheckForRedirectToIndex(result, "Drivers", "Create");
		}

		[TestMethod]
		public void Drivers_Edit_InvalidModel() {
			int oldCount = _campDB.Drivers.Count();
			int newId = oldCount + 1;
			var newDriver = CreateTestDriver(newId);
			var id = new Random().Next(_campDB.Drivers.Count());
			var oldDriver = _campDB.Drivers.Single(d => d.Id == id);
			_controller.ModelState.AddModelError("FistName", new ArgumentOutOfRangeException("FirstName"));

			var result = _controller.Edit(id, newDriver);

			var testedDriver = _campDB.Drivers.Single(d => d.Id == id);
			Assert.AreEqual(testedDriver, oldDriver, 
					"Edit with invalid model state should not change anything");

			ControllerUtils.CheckForView(result, "Drivers", "Create");
		}

		[TestMethod]
		public void Drivers_Delete() {
			var id = new Random().Next(_campDB.Drivers.Count());

			var result = _controller.Delete(id);

			Assert.IsNull(_campDB.Drivers.SingleOrDefault(d => d.Id == id),
					"Delete should remove the specified entity from list");
			ControllerUtils.CheckForRedirectToIndex(result, "Driver", "Delete");
		}
	}
}
