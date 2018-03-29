using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace WebEmployees.Controllers
{
	public class OrdersController : Controller
	{

		[WebMethod]
		public JsonResult GetOrders()
		{
			using (var dc = new MySQLEntities())
			{
				var query = (from o in dc.orders
							 join m in dc.masters on o.Master equals m.idMasters into ps1
							 from m in ps1.DefaultIfEmpty()
							 join wt in dc.worktypes on o.workType equals wt.idWorkTypes into ps2
							 from wt in ps2.DefaultIfEmpty()
							 join c in dc.customers on o.Customer equals c.idcustomers into ps3
							 from c in ps3.DefaultIfEmpty()
							 join d in dc.districts on o.District equals d.iddistricts into ps4
							 from d in ps4.DefaultIfEmpty()
							 join s in dc.status on o.status equals s.idstatus into ps5
							 from s in ps5.DefaultIfEmpty()
							 select new
							 {
								 orderNumber = o.orderNumber,
								 idOrders = o.idOrders,
								 formationDate = o.formationDate,
								 customerName = c.name,
								 workTypeName = wt.name,
								 districtName = d.name,
								 masterName = m.name,
								 takenDate = o.takenDate,
								 statusName = s.name,
								 o.InitialAmount,
								 o.TotalAmount,
								 o.CustomerComment

							 })
							 .AsEnumerable()
							 .Select(i => new
							 {
								 i.orderNumber,
								 i.idOrders,
								 formationDate = String.Format("{0:dd/MM/yyyy hh:mm:ss}", i.formationDate),
								 i.customerName,
								 i.workTypeName,
								 i.districtName,
								 i.masterName,
								 i.takenDate,
								 i.statusName,
								 i.InitialAmount,
								 i.TotalAmount,
								 i.CustomerComment
							 }
							 )
				.OrderBy(a => a.formationDate);

				var result = query.ToArray();

				//formationDate = String.Format("{0:dd/MM/yyyy hh:mm:ss}", o.formationDate),

				return Json(result, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpGet]
		public ActionResult OrderDetails(string idOrdersAsString)
		{
			try
			{
				using (var dc = new MySQLEntities())
				{
					SelectList districts = new SelectList(dc.districts.ToList(), "iddistricts", "name");
					ViewBag.Districts = districts;

					SelectList statuses = new SelectList(dc.status.ToList(), "idstatus", "name");
					ViewBag.Statuses = statuses;

					SelectList customers = new SelectList(dc.customers.ToList(), "idcustomers", "name");
					ViewBag.Customers = customers;

					SelectList worktypes = new SelectList(dc.worktypes.ToList(), "idWorkTypes", "name");
					ViewBag.Worktypes = worktypes;

					SelectList masters = new SelectList(dc.masters.ToList(), "idMasters", "name");
					ViewBag.Masters = masters;

					SelectList works = new SelectList(dc.works.ToList(), "idWorks", "name");
					ViewBag.Works = works;

					var order = new orders();
					if (idOrdersAsString == "")
					{
						order.idOrders = Guid.NewGuid();
					}
					else
					{
						Guid idOrders = new Guid(idOrdersAsString);
						order = dc.orders.Find(idOrders);
					}

					var model = new OrderAndTable(order)
					{
						order = order,
					};
					return PartialView(model);
				};
			}
			catch (Exception ex)
			{
				return HttpNotFound();
			}
		}

		[HttpPost]
		public ActionResult OrderDetails(OrderAndTable model)
		{
			var order = model.order;

			using (var dc = new MySQLEntities())
			{
				var orderInDb = dc.orders.Find(order.idOrders);

				if (orderInDb==null)
				{

					order.idOrders = Guid.NewGuid();
					dc.orders.Add(order);
					dc.SaveChanges();
				}
				else
				{
					//dc.Set<order>().AddOrUpdate(order);

					dc.Entry(orderInDb).CurrentValues.SetValues(order);
					dc.Entry(orderInDb).State = EntityState.Modified;
					dc.SaveChanges();
				}
			}

			Thread.Sleep(1000);
			return RedirectToAction("../Home/Index");
		}

		public ActionResult SaveOrder(OrderAndTable model)
		{
			model.SaveTable();
			return Json(model);
		}

		public ActionResult AddWorkOrder(OrderAndTable model)
		{
			model.AddWorkOrder();
			return Json(model);
		}

		public ActionResult RemoveWorkOrder(OrderAndTable model, int index)
		{
			model.RemoveWorkOrder(index);
			return Json(model);
		}
	}
}



