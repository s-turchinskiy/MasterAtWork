using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace WebEmployees.Controllers
{
	public class MastersController : Controller
	{

		[WebMethod]
		public JsonResult GetMasters()
		{
			using (var dc = new MySQLEntities())
			{
				var result = (from w in dc.masters
							  select new
							  {
								  w.idMasters,
								  w.name,
								  w.IdFirebase,
								  w.email,
								  w.nameInMail,
								  w.deposit
							  })
				.OrderBy(i => i.name).ToArray();

				return Json(result, JsonRequestBehavior.AllowGet);
			}
		}
		[HttpGet]
		public ActionResult MasterDetails(string idMastersAsString)
		{
			try
			{
				using (var dc = new MySQLEntities())
				{

					//SelectList districts = new SelectList(dc.districts.ToList(), "iddistricts", "name");
					//ViewBag.Districts = districts;

					//SelectList worktypes = new SelectList(dc.worktypes.ToList(), "idWorkTypes", "name");
					//ViewBag.Worktypes = worktypes;

					var master = new masters();
					if (idMastersAsString == "")
					{
						master.idMasters = Guid.NewGuid();
					}
					else
					{
						Guid idMasters = new Guid(idMastersAsString);
						master = dc.masters.Find(idMasters);
					}

					var model = new MasterAndTable(master)
					{
						master = master,
					};

					ViewBag.districtsmaster = model.districtsmaster;

					return PartialView(model);
				};
			}
			catch (Exception ex)
			{
				return HttpNotFound();
			}
		}

		[HttpPost]
		public ActionResult MasterDetails(MasterAndTable model)
		{
			System.Diagnostics.Debug.WriteLine("MasterDetails1" + DateTime.Now);
			Thread.Sleep(1000);
			return RedirectToAction("../Home/Masters");

		}

		public ActionResult SaveMaster(MasterAndTable model)
		{
			System.Diagnostics.Debug.WriteLine("SaveMaster1" + DateTime.Now);
			model.Save();
			System.Diagnostics.Debug.WriteLine("SaveMaster2" + DateTime.Now);
			return Json(model);
		}

		public ActionResult AddDistrictMaster(MasterAndTable model)
		{
			model.AddDistrictMaster();
			return Json(model);
		}

		public ActionResult RemoveDistrictMaster(MasterAndTable model, int index)
		{
			model.RemoveDistrictMaster(index);
			return Json(model);
		}

		public ActionResult AddWorktypeMaster(MasterAndTable model)
		{
			model.AddWorktypeMaster();
			return Json(model);
		}

		public ActionResult RemoveWorktypeMaster(MasterAndTable model, int index)
		{
			model.RemoveWorktypeMaster(index);
			return Json(model);
		}

		//public ViewResult AddNewDistrictsMasters()
		//{
		//	try
		//	{
		//		using (var dc = new MySQLEntities())
		//		{
		//			SelectList districts = new SelectList(dc.districts.ToList(), "iddistricts", "name");
		//			ViewBag.Districts = districts;

		//		}

		//		return View("districtsmasters", new districtsmasters());
		//	}
		//	catch (Exception ex)
		//	{
		//		return View();
		//	}
		//}

		public ActionResult Masters()
		{
			return View();
		}

	}

}



