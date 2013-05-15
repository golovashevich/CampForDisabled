using Camp.Interfaces;
using Camp.Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Web.Controllers
{
	public class CamperController : CampEntityController, ICampEntityController<CamperModel>
	{
        public CamperController(ICampDB campDB) : base(campDB) { }

		
		public ActionResult Index()
		{
            List<CamperModel> campers = new List<CamperModel>();
            foreach (var camper  in CampDB.Campers)
            {
                camper.Comments = Utils.PrepareStringForItemList(camper.Comments, 30);
                campers.Add(camper);
            }

			return View(campers);
		}

		
		public ActionResult Details(int? id)
		{
            if (null == id)
            {
                return RedirectToAction("Index");
            }

			var camper = CampDB.Campers.SingleOrDefault(r => r.Id == id);
			if (camper == null)
			{
				return RedirectToAction("Index");
			}

			return View(camper);
		}


        [Authorize]
        public ActionResult Delete(int? id)
		{
            if (null == id)
            {
                return RedirectToAction("Index");
            }

			var camper = CampDB.Campers.SingleOrDefault(r => r.Id == id);
			if (camper != null)
			{
				CampDB.Campers.Remove(camper);
				CampDB.SaveChanges();
			}
			return RedirectToAction("Index");
		}


        [Authorize]
        public ActionResult Create()
		{
			ViewBag.IsCreate = true;
			return View("Edit", new CamperModel());
		}


		[HttpPost]
        [Authorize]
        public ActionResult Create(CamperModel camper)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.IsCreate = true;
				return View("Edit", camper);
			}

			CampDB.Campers.Add(camper);
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

			var camper = CampDB.Campers.SingleOrDefault(r => r.Id == id);
			if (camper == null)
			{
				return RedirectToAction("Index");	
			}

			ViewBag.IsCreate = false;
			return View(camper);
        }


        [HttpPost]
        [Authorize]
        public ActionResult Edit(int? id, CamperModel newCamper)
        {
            if (null == id)
            {
                return RedirectToAction("Index");
            }
			
			var p = from camper in CampDB.Campers
					where camper.Id == id
					select camper;
			var existingCamper = p.FirstOrDefault();
			
			if (!ModelState.IsValid)
			{
				ViewBag.IsCreate = false;
				if (existingCamper != null)
				{
					ViewBag.FullName = existingCamper.FullName;
				}

				return View(newCamper);
			}

			if (existingCamper != null)
			{
				existingCamper.FirstName = newCamper.FirstName;
				existingCamper.LastName = newCamper.LastName;
				existingCamper.DateOfBirth = newCamper.DateOfBirth;
				existingCamper.PostIndex = newCamper.PostIndex;
				existingCamper.CityId = newCamper.CityId;
				existingCamper.District = newCamper.District;
				existingCamper.Street = newCamper.Street;
				existingCamper.HomeNumber = newCamper.HomeNumber;
				existingCamper.AppartmentNumber = newCamper.AppartmentNumber;
				existingCamper.HomePhone = StripPhoneNumber(newCamper.HomePhone);
				existingCamper.AddressNote = newCamper.AddressNote;
				existingCamper.Contacts = newCamper.Contacts;
				existingCamper.Email = newCamper.Email;
				existingCamper.Skype = newCamper.Skype;
				existingCamper.DisabilityGrade = newCamper.DisabilityGrade;
				existingCamper.MedicalNote = newCamper.MedicalNote;
				existingCamper.Comments = newCamper.Comments;
				//TODO: Add Foto field
			}
			else
			{
				CampDB.Campers.Add(newCamper);
			}
	
			CampDB.SaveChanges();
			return RedirectToAction("Index");
        }


		public string StripPhoneNumber(string phone)
		{
			if (phone == null)
			{
				return null;
			}
			
			string result = String.Empty;

			string onlyDigitsPhone = Regex.Replace(phone, @"[^\d]", String.Empty);
			
			if (Regex.IsMatch(phone, @"^( +)?\+([^+]+)?$") && (onlyDigitsPhone.Length == 11 || onlyDigitsPhone.Length == 12))
			{
				result = "+";
			}

			result += onlyDigitsPhone;

			return result;
		}
    }
}
