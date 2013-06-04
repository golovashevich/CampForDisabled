using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Camp.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Web.Tests.Controllers {
	public abstract class CampControllerTestBase<T> where T: class, ICampModel, new() {
		protected static ICampDB _campDB;
		protected static ICampEntityController<T> _controller;
		protected static Mock<IDbSet<T>> _entityMock;
		protected static Mock<ICampDB> _mock;

		#region Abstraction stuff
		protected abstract ICampEntityController<T> CreateController();
		protected abstract void SetupEntityMock();
		protected abstract T CreateTestEntity(int newId);
		protected abstract string ControllerName { get; }
		protected abstract IDbSet<T> Entities { get; }
		protected abstract void GenerateModelError();
		#endregion

		public static void BaseClassInit(TestContext testContext) {
			_mock = new Mock<ICampDB>();
			_entityMock = SetupEntityListMock();
			_campDB = _mock.Object;
		}

		private static Mock<IDbSet<T>> SetupEntityListMock(){
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
		public virtual void TestInit() {
			SetupEntityMock();
			_controller = CreateController();
			_controller.ModelState.Clear();

			Entities.Local.Clear();
			for (int i = 0; i < 10; i++) {
				Entities.Add(CreateTestEntity(i));
			}
		}

		[TestMethod]
		public void NullIdRedirectsToIndex() {
			new InvalidIdChecks<T>(_controller).NullIdRedirectsToIndex();
		}


		[TestMethod]
		public void NonExistingIdRedirectsToIndex() {
			new InvalidIdChecks<T>(_controller).NonExistingIdRedirectsToIndex(Int32.MinValue);
		}

		[TestMethod]
		public void Create_Get() {
			var result = _controller.Create();

			ControllerUtils.CheckForView(result, ControllerName, "Create");

			Assert.AreEqual(new T(), (result as ViewResult).Model);
		}

		[TestMethod]
		public void Create_Post() {
			int oldCount = Entities.Count();
			int newId = oldCount + 1;
			var entity = CreateTestEntity(newId);

			var result = _controller.Create(entity);

			Assert.AreEqual(oldCount + 1, Entities.Count(), "Create action should add new entity");
			var savedEntity = Entities.SingleOrDefault(d => d.Id == newId);
			Assert.IsNotNull(savedEntity, "Entity does not seem to be added");
			Assert.IsTrue(entity.Equals(savedEntity), "Original and saved entities differ");

			ControllerUtils.CheckForRedirectToIndex(result, ControllerName, "Create");
		}

		/// <summary>
		/// Create with invalid model does not add anything and returns to Create view
		/// </summary>
		[TestMethod]
		public void Create_IndvalidModel() {
			int oldCount = Entities.Count();
			int newId = oldCount + 1;
			var entity = CreateTestEntity(newId);
			GenerateModelError();

			var result = _controller.Create(entity);

			Assert.AreEqual(oldCount, Entities.Count(),
					"Create action with invalid model should not add anything");

			ControllerUtils.CheckForView(result, ControllerName, "Create");
		}

		[TestMethod]
		public void Details() {
			var id = new Random().Next(Entities.Count());
			var expectedEntity = Entities.Single(d => d.Id == id);

			var result = _controller.Details(id);

			ControllerUtils.CheckForView(result, ControllerName, "Details");

			Assert.AreEqual(expectedEntity, (result as ViewResult).Model);
		}

		[TestMethod]
		public void Edit_Get() {
			var id = new Random().Next(Entities.Count());
			var expectedEntity = Entities.Single(d => d.Id == id);

			var result = _controller.Edit(id);

			ControllerUtils.CheckForView(result, ControllerName, "Edit");
			Assert.AreEqual(expectedEntity, (result as ViewResult).Model);
		}

		[TestMethod]
		public void Edit_Post() {
			var rand = new Random();
			var newEntity = CreateTestEntity(rand.Next(Int32.MaxValue));
			var id = rand.Next(Entities.Count());
			newEntity.Id = id;

			var result = _controller.Edit(id, newEntity);

			var updatedEntity = Entities.Single(d => d.Id == id);
			Assert.AreEqual(newEntity, updatedEntity);
			ControllerUtils.CheckForRedirectToIndex(result, ControllerName, "Edit");
		}

		[TestMethod]
		public void Edit_InvalidModel() {
			int oldCount = Entities.Count();
			int newId = oldCount + 1;
			var newEntity = CreateTestEntity(newId);
			var id = new Random().Next(Entities.Count());
			var oldEntity = Entities.Single(d => d.Id == id);
			GenerateModelError();

			var result = _controller.Edit(id, newEntity);

			var testedEntity = Entities.Single(d => d.Id == id);
			Assert.AreEqual(testedEntity, oldEntity,
					"Edit with invalid model state should not change anything");

			ControllerUtils.CheckForView(result, ControllerName, "Edit");
		}

		[TestMethod]
		public void Delete() {
			var id = new Random().Next(Entities.Count());

			var result = _controller.Delete(id);

			Assert.IsNull(Entities.SingleOrDefault(d => d.Id == id),
					"Delete should remove the specified entity from list ({0})", id);
			ControllerUtils.CheckForRedirectToIndex(result, ControllerName, "Delete");
		}
	}
}
