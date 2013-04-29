using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Camp.Interfaces;
using Camp.Models;

namespace Web.Controllers
{
    public class DriverController : CampEntityController, ICampEntityController<DriverModel>
    {
        public DriverController(ICampDB campDB) : base(campDB) { }


        public ActionResult Index()
        {
            List<DriverModel> drivers = new List<DriverModel>();
            foreach (var driver in CampDB.Drivers)
            {
                driver.Comments = Utils.PrepareStringForItemList(driver.Comments);
                driver.Contacts = Utils.PrepareStringForItemList(driver.Contacts);
                drivers.Add(driver);
            }

            return View(drivers);
        }


        public ActionResult Details(int? id)
        {
            if (null == id)
            {
                return RedirectToAction("Index");
            }
            var model = CampDB.Drivers.SingleOrDefault(r => r.Id == id);
            if (null == model)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }


        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IsCreate = true;
            return View("Edit", new DriverModel());
        }


        [Authorize]
        [HttpPost]
        public ActionResult Create(DriverModel driver)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IsCreate = true;
                return View("Edit", driver);
            }

            CampDB.Drivers.Add(driver);
            CampDB.SaveChanges();

            return RedirectToAction("Index");
        }


        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (null == id)
            {
                return RedirectToAction("Index");
            }

            var model = CampDB.Drivers.SingleOrDefault(r => r.Id == id);
            if (null == model)
            {
                return RedirectToAction("Index");
            }

            ViewBag.IsCreate = false;
            return View(model);
        }


        [HttpPost]
        [Authorize]
        public ActionResult Edit(int? id, DriverModel newDriver)
        {
            if (!ModelState.IsValid || null == id)
            {
                ViewBag.IsCreate = false; 
                return View(newDriver);
            }

            var p = from driver in CampDB.Drivers
                    where driver.Id == id
                    select driver;
            var existingDriver = p.FirstOrDefault();
            if (null == existingDriver)
            {
                ViewBag.IsCreate = false; 
                return View(newDriver);
            }
                                            
            existingDriver.FirstName = newDriver.FirstName;
            existingDriver.LastName = newDriver.LastName;
            existingDriver.Contacts = newDriver.Contacts;
            existingDriver.SitPlacesNum = newDriver.SitPlacesNum;
            existingDriver.WheelchairsNum = newDriver.WheelchairsNum;
            existingDriver.Comments = newDriver.Comments;

            CampDB.SaveChanges();
            return RedirectToAction("Index");
        }


        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (null == id)
            {
                return RedirectToAction("Index");
            }

            var existingDriver = CampDB.Drivers.SingleOrDefault(r => r.Id == id);
            if (null != existingDriver)
            {
                CampDB.Drivers.Remove(existingDriver);
                CampDB.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
