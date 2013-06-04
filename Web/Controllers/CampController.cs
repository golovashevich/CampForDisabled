using Camp.Interfaces;
using Camp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
	public class CampController : CampEntityController, ICampEntityController<CampModel>
	{

        public CampController(ICampDB campDb) : base(campDb) { }

		
		public ActionResult Index()
		{
            List<CampModel> camps = new List<CampModel>();
            foreach (var camp  in CampDB.Camps)
            {
                camps.Add(camp);
            }

			return View(camps);
		}

		
		public ActionResult Details(int? id)
		{
            if (null == id)
            {
                return RedirectToAction("Index");
            }

			var camp = CampDB.Camps.SingleOrDefault(r => r.Id == id);
            if (null == camp)
            {
                return RedirectToAction("Index");
            }

			return View(camp);
		}


        [Authorize]
        public ActionResult Delete(int? id)
		{
            if (null == id)
            {
                return RedirectToAction("Index");
            }
			var camp = CampDB.Camps.SingleOrDefault(r => r.Id == id);
			if (camp != null)
			{
				CampDB.Camps.Remove(camp);
				CampDB.SaveChanges();
			}
			return RedirectToAction("Index");
		}


        [Authorize]
		public ActionResult Create()
		{
			ViewBag.IsCreate = true;
			return View("Edit", new CampModel());
		}


		[HttpPost]
        [Authorize]
		public ActionResult Create(CampModel camp)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.IsCreate = true;
				return View("Edit", camp);
			}

			CampDB.Camps.Add(camp);
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

			var camp = CampDB.Camps.SingleOrDefault(r => r.Id == id);
			if (camp == null)
			{
				return RedirectToAction("Index");	
			}

			ViewBag.IsCreate = false; 
			return View(camp);
        }


        [HttpPost]
        [Authorize]
        public ActionResult Edit(int? id, CampModel newCamp)
        {
            if (null == id)
            {
                return RedirectToAction("Index");
            }

			var p = from camp in CampDB.Camps
					where camp.Id == id
					select camp;
			var existingCamp = p.FirstOrDefault();
			
			if (!ModelState.IsValid)
			{
				ViewBag.IsCreate = false;
				if (existingCamp != null)
				{
					ViewBag.Name = existingCamp.Name;
				}

				return View(newCamp);
			}

			if (existingCamp != null)
			{
				existingCamp.Name = newCamp.Name;
				existingCamp.Theme = newCamp.Theme;
				existingCamp.BeginDate = newCamp.BeginDate;
				existingCamp.EndDate = newCamp.EndDate;
				existingCamp.Description = newCamp.Description;
				existingCamp.History = newCamp.History;
			}
			else
			{
				CampDB.Camps.Add(newCamp);
			}
	
			CampDB.SaveChanges();
			return RedirectToAction("Index");
        }
	}
}
