using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Camp.Interfaces;
using Camp.Models;

namespace Web.Controllers
{
	public class CamperForYearController : CampEntityController
	{
        public CamperForYearController(ICampDB campDb) : base(campDb) { }


		public ActionResult Index(int? id) {
			if (id == null) {
				return RedirectToAction("Index", "Camp");
			}

			var camp = CampDB.Camps.SingleOrDefault(r => r.Id == id);
			if (camp == null) {
				return RedirectToAction("Index", "Camp");
			}

			var campers = from camperForYear in CampDB.CampersForYear
						  where camperForYear.CampId == id.Value
						  select camperForYear.Camper;

			ViewBag.CampName = camp.CampName;
			return View(campers.Distinct().ToList());
		}


		[Authorize]
		public ActionResult Delete(int? id, int? campId) {
			if (campId == null || CampDB.Camps.SingleOrDefault(r => r.Id == campId) == null) {
				return RedirectToAction("Index", "Camp");
			}

			if (id == null || CampDB.Campers.SingleOrDefault(r => r.Id == id) == null) {
				return RedirectToAction("Index", new { id = campId });
			}

			foreach (var camperForYear in CampDB.CampersForYear) {
				if (camperForYear.CampId == campId && camperForYear.CamperId == id) {
					CampDB.CampersForYear.Remove(camperForYear);
				}
			}

			CampDB.SaveChanges();
			return RedirectToAction("Index", new { id = campId });
		}

		
		public ActionResult Details(int? id, int? campId) {
			if (campId == null) {
				return RedirectToAction("Index", "Camp");
            }

			var camp = CampDB.Camps.SingleOrDefault(r => r.Id == campId);
			if (camp == null) {
				return RedirectToAction("Index", "Camp");
			}

            if (id == null) {
				return RedirectToAction("Index", new { id = campId});
            }
	
			var wantedCamper = CampDB.Campers.SingleOrDefault(r => r.Id == id);
			if (wantedCamper == null ) {
				return RedirectToAction("Index", new { id = campId });
			}

			// check is the camper present in campId year campers list
			var campers = from camper in CampDB.CampersForYear
						  where camper.CampId == campId.Value && camper.CamperId == id.Value
						  select camper.Camper;

			if (campers.Count() == 0) {
				return RedirectToAction("Index", new { id = campId });
			}

			ViewBag.CampName = camp.CampName;
			return View(wantedCamper);
		}


		[Authorize]
        public ActionResult EditList(int? id)
        {
			if (id == null) {
				return RedirectToAction("Index", "Camp");
			}

			var camp = CampDB.Camps.SingleOrDefault(r => r.Id == id);
			if (camp == null) {
				return RedirectToAction("Index", "Camp");
			} 
			
			var campers = 
				from camperForYear in CampDB.CampersForYear
				where camperForYear.CampId == id.Value
				select camperForYear.Camper;

			SelectCamperModel model = new SelectCamperModel();
			model.ChosenCampers = campers.Distinct();
			model.AvailableCampers = CampDB.Campers.ToList().Except(model.ChosenCampers, new CamperComparer());

			ViewBag.CampName = camp.CampName;
			return View(model);
        }


        [HttpPost]
        [Authorize]
        public ActionResult EditList(int? id, SelectCamperPostModel model)
        {
			if (id == null) {
				return RedirectToAction("Index", "Camp");
			}

			var camp = CampDB.Camps.SingleOrDefault(r => r.Id == id);
			if (camp == null) {
				return RedirectToAction("Index", "Camp");
			}

			var oldCampersForYear = from camperForYear in CampDB.CampersForYear
									where camperForYear.CampId == id.Value
									select camperForYear;

			foreach (var oldCamperForYear in oldCampersForYear) {
				CampDB.CampersForYear.Remove(oldCamperForYear);
			}

			foreach (var camper in model.ChosenCampers) {
				var camperForYear = new CamperForYearModel();
				camperForYear.CampId = id.Value;
				camperForYear.CamperId = camper;
				CampDB.CampersForYear.Add(camperForYear);
			}

			CampDB.SaveChanges();
			return RedirectToAction("Index", new { id = id });
		}
	}
}
