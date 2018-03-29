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
	public class HomeController : Controller
	{

		[WebMethod]
		public bool UpdateDataInMySQLSuccessfully(string guid1C, string type, string jsonData) //ActionResult
		{
			if (guid1C==null)
				return false;

			switch (type)
			{
				case "Документ._ЗаказыМастерам":
					var order = new JavaScriptSerializer().Deserialize<OrderInJSON>(jsonData);
					return UpdateOrderInMySQL(order);
				case "Справочник._Клиенты":
					var element2 = new JavaScriptSerializer().Deserialize<CustomerInJSON>(jsonData);
					return UpdateCustomerInMySQL(element2);
				case "Справочник._Районы":
					var element3 = new JavaScriptSerializer().Deserialize<DistrictInJSON>(jsonData);
					return UpdateDistrictInMySQL(element3);
				case "Справочник._Мастера":
					var element4 = new JavaScriptSerializer().Deserialize<MasterInJSON>(jsonData);
					return UpdateMasterInMySQL(element4);
				case "Справочник._СтатусыЗаявокМастерам":
					var element5 = new JavaScriptSerializer().Deserialize<StatusInJSON>(jsonData);
					return UpdateStatusInMySQL(element5);
				case "Справочник._Работы":
					var element6 = new JavaScriptSerializer().Deserialize<WorkInJSON>(jsonData);
					return UpdateWorkInMySQL(element6);
				case "Справочник._ВидыРабот":
					var element7 = new JavaScriptSerializer().Deserialize<WorkTypeInJSON>(jsonData);
					return UpdateWorkTypeInMySQL(element7);

				default:
					return false;
			};
			//int IndexFirst = jsonData.IndexOf(guid1C);
			//jsonData = jsonData.Remove(IndexFirst, guid1C.Length).Insert(IndexFirst, "root");
		}

		public bool UpdateOrderInMySQL(OrderInJSON order)
		{
			try
			{
				using (var dc = new MySQLEntities())
				{

					Guid idOrders = new Guid(order.idOrders);
					var elementMySQL = dc.orders.Find(idOrders);
					if (elementMySQL == null)
					{
						elementMySQL = dc.orders.Add(new orders());
					}

					elementMySQL.idOrders = new Guid(order.idOrders);
					elementMySQL.formationDate = DateTime.Parse(order.formationDate);
					elementMySQL.Customer = new Guid(order.customer);
					elementMySQL.workType = new Guid(order.workType);
					elementMySQL.CustomerComment = order.customerComment;
					elementMySQL.District = new Guid(order.district);
					elementMySQL.InitialAmount = order.initialAmount;
					elementMySQL.status = order.status;
					elementMySQL.orderNumber = order.orderNumber;
					if (order.master.IsEmpty())
					{
						elementMySQL.Master = null;
					}
					else
					{
						masters masterInMySQL = dc.masters.SingleOrDefault(item => item.IdFirebase == order.master);
						elementMySQL.Master = masterInMySQL.idMasters;
					}
					elementMySQL.MasterComment = order.masterComment;
					elementMySQL.TotalAmount = order.totalAmount;
					//elementMySQL.Work = new Guid(order.work);

					//	var newValue = DateTime.ParseExact(value[0], "yyyy-mm-dd", null);

					dc.SaveChanges();
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}

		}

		public bool UpdateMasterInMySQL(MasterInJSON elementInJSON)
		{
			try
			{
				using (var dc = new MySQLEntities())
				{
					Guid idMasters = new Guid(elementInJSON.idMasters);
					var elementMySQL = dc.masters.Find(idMasters);
					if (elementMySQL == null)
					{
						elementMySQL = dc.masters.Add(new masters());
					}

					elementMySQL.idMasters = new Guid(elementInJSON.idMasters);
					elementMySQL.email = elementInJSON.email;
					elementMySQL.IdFirebase = elementInJSON.idFirebase;
					elementMySQL.name = elementInJSON.nameIn1C;
					elementMySQL.nameInMail = elementInJSON.nameInEmail;

					dc.SaveChanges();
				}
				return true;
			}
			catch
			{
				return false;
			}

		}

		public bool UpdateCustomerInMySQL(CustomerInJSON elementInJSON)
		{
			try
			{
				using (var dc = new MySQLEntities())
				{
					Guid idcustomers = new Guid(elementInJSON.idcustomers);
					var elementMySQL = dc.customers.Find(idcustomers);
					if (elementMySQL == null)
					{
						elementMySQL = dc.customers.Add(new customers());
					}

					elementMySQL.idcustomers = new Guid(elementInJSON.idcustomers);
					elementMySQL.district = new Guid(elementInJSON.district);
					elementMySQL.name = elementInJSON.name;
					elementMySQL.phone = elementInJSON.phone;
					elementMySQL.privateAddress = elementInJSON.privateAddress;
					elementMySQL.publicAddress = elementInJSON.publicAddress;

					dc.SaveChanges();
				}
				return true;
			}
			catch
			{
				return false;
			}

		}

		public bool UpdateDistrictInMySQL(DistrictInJSON elementInJSON)
		{
			try
			{
				using (var dc = new MySQLEntities())
				{
					Guid iddistricts = new Guid(elementInJSON.iddistricts);
					var elementMySQL = dc.districts.Find(iddistricts);
					if (elementMySQL == null)
					{
						elementMySQL = dc.districts.Add(new districts());
					}

					elementMySQL.iddistricts = new Guid(elementInJSON.iddistricts);
					elementMySQL.name = elementInJSON.name;

					dc.SaveChanges();
				}
				return true;
			}
			catch
			{
				return false;
			}

		}

		public bool UpdateWorkTypeInMySQL(WorkTypeInJSON elementInJSON)
		{
			try
			{
				using (var dc = new MySQLEntities())
				{
					Guid idWorkTypes = new Guid(elementInJSON.idWorkTypes);
					var elementMySQL = dc.worktypes.Find(idWorkTypes);
					if (elementMySQL == null)
					{
						elementMySQL = dc.worktypes.Add(new worktypes());
					}

					elementMySQL.idWorkTypes = new Guid(elementInJSON.idWorkTypes);
					elementMySQL.name = elementInJSON.name;

					dc.SaveChanges();
				}
				return true;
			}
			catch
			{
				return false;
			}

		}

		public bool UpdateStatusInMySQL(StatusInJSON elementInJSON)
		{
			try
			{
				using (var dc = new MySQLEntities())
				{
					var idstatus = elementInJSON.codeStatuses;
					var elementMySQL = dc.status.Find(idstatus);
					if (elementMySQL == null)
					{
						elementMySQL = dc.status.Add(new status());
					}

					elementMySQL.idstatus = elementInJSON.codeStatuses;
					elementMySQL.name = elementInJSON.name;

					dc.SaveChanges();
				}
				return true;
			}
			catch
			{
				return false;
			}

		}

		public bool UpdateWorkInMySQL(WorkInJSON elementInJSON)
		{
			try
			{
				using (var dc = new MySQLEntities())
				{
					Guid idWorks = new Guid(elementInJSON.idworks);
					var elementMySQL = dc.works.Find(idWorks);
					if (elementMySQL == null)
					{
						elementMySQL = dc.works.Add(new works());
					}

					elementMySQL.idWorks = new Guid(elementInJSON.idworks);
					elementMySQL.name = elementInJSON.name;
					elementMySQL.Price = elementInJSON.price;

					dc.SaveChanges();
				}
				return true;
			}
			catch
			{
				return false;
			}

		}

		[WebMethod]
		public bool RemoveElement(string elementType, string idElementAsString)
		{
			if (idElementAsString == "")
			{
				return false;
			}

			idElementAsString = idElementAsString.Replace("[", "").Replace("]", "").Replace("\"", "");
			string[] ids = idElementAsString.Split(new char[] { ',' });
			try
			{
				using (var dc = new MySQLEntities())
				{
					foreach (var item in ids)
					{
						switch (elementType)
						{
							case "works":
								Guid idWorks = new Guid(item);
								var elementMySQL1 = dc.works.Find(idWorks);
								dc.works.Remove(elementMySQL1);
								break;
							case "masters":
								Guid idMasters = new Guid(item);
								var elementMySQL2 = dc.masters.Find(idMasters);
								dc.masters.Remove(elementMySQL2);
								break;
							case "districts":
								Guid idDistricts = new Guid(item);
								var elementMySQL3 = dc.districts.Find(idDistricts);
								dc.districts.Remove(elementMySQL3);
								break;
							case "customers":
								Guid idCustomers = new Guid(item);
								var elementMySQL4 = dc.customers.Find(idCustomers);
								dc.customers.Remove(elementMySQL4);
								break;
							default:
								break;
						}
					}

					dc.SaveChanges();
					return true;
				};
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		//-------------------------------------------------------------------------

		[WebMethod]
		public JsonResult GetWorks()
		{
			using (var dc = new MySQLEntities())
			{
				var result = (from w in dc.works
							  select new
							  {
								  w.idWorks,
								  w.name,
								  w.Price
							  })
				.OrderBy(i => i.name).ToArray();

				return Json(result, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpGet]
		public ActionResult WorkDetails(string idWorksAsString)
		{
			if (idWorksAsString == "")
			{
				return PartialView();
			}

			try
			{
				using (var dc = new MySQLEntities())
				{
					Guid idWorks = new Guid(idWorksAsString);
					var elementMySQL = dc.works.Find(idWorks);
					return PartialView(elementMySQL);
				};
			}
			catch (Exception ex)
			{
				return HttpNotFound();
			}
		}

		[HttpPost]
		public ActionResult WorkDetails(works work)
		{
			using (var dc = new MySQLEntities())
			{
				if (work.idWorks == Guid.Empty)
				{

					work.idWorks = Guid.NewGuid();
					dc.works.Add(work);
					dc.SaveChanges();
				}
				else
				{

					dc.Entry(work).State = EntityState.Modified;
					dc.SaveChanges();
				}
			}

			return RedirectToAction("Works");

			//if (idWorksAsString == null)
			//{
			//	return HttpNotFound();
			//}

			//try
			//{
			//	using (var dc = new MySQLEntities())
			//	{
			//		Guid idWorks = new Guid(idWorksAsString);
			//		var elementMySQL = dc.works.Find(idWorks);
			//		return PartialView(elementMySQL);
			//	};
			//}
			//catch (Exception ex)
			//{
			//return HttpNotFound();
			//}

		}

		//-------------------------------------------------------------------------

		[WebMethod]
		public JsonResult GetDistricts()
		{
			using (var dc = new MySQLEntities())
			{
				var result = (from w in dc.districts
							  select new
							  {
								  w.iddistricts,
								  w.name
							  })
				.OrderBy(i => i.name).ToArray();

				return Json(result, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpGet]
		public ActionResult DistrictDetails(string idDistrictsAsString)
		{
			if (idDistrictsAsString == "")
			{
				return PartialView();
			}

			try
			{
				using (var dc = new MySQLEntities())
				{
					Guid idDistricts = new Guid(idDistrictsAsString);
					var elementMySQL = dc.districts.Find(idDistricts);
					return PartialView(elementMySQL);
				};
			}
			catch (Exception ex)
			{
				return HttpNotFound();
			}
		}

		[HttpPost]
		public ActionResult DistrictDetails(districts district)
		{
			using (var dc = new MySQLEntities())
			{
				if (district.iddistricts == Guid.Empty)
				{

					district.iddistricts = Guid.NewGuid();
					dc.districts.Add(district);
					dc.SaveChanges();
				}
				else
				{

					dc.Entry(district).State = EntityState.Modified;
					dc.SaveChanges();
				}
			}

			return RedirectToAction("Districts");
		}


		//-------------------------------------------------------------------------

		[WebMethod]
		public JsonResult GetWorktypes()
		{
			using (var dc = new MySQLEntities())
			{
				var result = (from w in dc.worktypes
							  select new
							  {
								  w.idWorkTypes,
								  w.name
							  })
				.OrderBy(i => i.name).ToArray();

				return Json(result, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpGet]
		public ActionResult WorktypeDetails(string idWorktypesAsString)
		{
			if (idWorktypesAsString == "")
			{
				return PartialView();
			}

			try
			{
				using (var dc = new MySQLEntities())
				{
					Guid idWorkTypes = new Guid(idWorktypesAsString);
					var elementMySQL = dc.worktypes.Find(idWorkTypes);
					return PartialView(elementMySQL);
				};
			}
			catch (Exception ex)
			{
				return HttpNotFound();
			}
		}

		[HttpPost]
		public ActionResult WorktypeDetails(worktypes worktype)
		{
			using (var dc = new MySQLEntities())
			{
				if (worktype.idWorkTypes == Guid.Empty)
				{

					worktype.idWorkTypes = Guid.NewGuid();
					dc.worktypes.Add(worktype);
					dc.SaveChanges();
				}
				else
				{

					dc.Entry(worktype).State = EntityState.Modified;
					dc.SaveChanges();
				}
			}

			return RedirectToAction("Worktypes");
		}

		//-------------------------------------------------------------------------

		[WebMethod]
		public JsonResult GetCustomers()
		{
			using (var dc = new MySQLEntities())
			{
				var result = (from w in dc.customers
							  join d in dc.districts on w.district equals d.iddistricts into ps4
							  from d in ps4.DefaultIfEmpty()
							  select new
							  {
								  w.idcustomers,
								  w.name,
								  districtName = d.name,
								  w.phone,
								  w.privateAddress,
								  w.publicAddress
							  })
				.OrderBy(i => i.name).ToArray();

				return Json(result, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpGet]
		public ActionResult CustomerDetails(string idCustomersAsString)
		{
			try
			{
				using (var dc = new MySQLEntities())
				{
					SelectList districts = new SelectList(dc.districts.ToList(), "iddistricts", "name");
					ViewBag.Districts = districts;

					if (idCustomersAsString == "")
					{
						return PartialView();
					}


					Guid idCustomers = new Guid(idCustomersAsString);
					var elementMySQL = dc.customers.Find(idCustomers);
					return PartialView(elementMySQL);
				};
			}
			catch (Exception ex)
			{
				return HttpNotFound();
			}
		}

		[HttpPost]
		public ActionResult CustomerDetails(customers customer)
		{
			using (var dc = new MySQLEntities())
			{
				if (customer.idcustomers == Guid.Empty)
				{

					customer.idcustomers = Guid.NewGuid();
					dc.customers.Add(customer);
					dc.SaveChanges();
				}
				else
				{

					dc.Entry(customer).State = EntityState.Modified;
					dc.SaveChanges();
				}
			}

			return RedirectToAction("Customers");
		}

		//-------------------------------------------------------------------------
		public ActionResult Details(string id)
		{
			//if (id == null)
			//{
			//	return HttpNotFound();
			//}

			//try
			//{
			//	using (var dc = new MyDatabaseEntities())
			//	{

			//		var request = (from emp in dc.Employees
			//					   join pos in dc.Positions on emp.PositionID equals pos.IDPosition
			//					   join dp in dc.Departments on emp.DepartmentID equals dp.IDDepartment
			//					   join block in dc.Departments on dp.ParentID equals block.IDDepartment into blockvr
			//					   from block in blockvr.DefaultIfEmpty()
			//					   join div in dc.Departments on emp.DivisionID equals div.IDDepartment
			//					   join org in dc.Organizations on emp.OrganizationID equals org.IDOrganization
			//					   join empPhoto in dc.EmployeePhotoes on emp.EmployeePhotoID equals empPhoto.IDEmployeePhoto into ps
			//					   from empPhoto in ps.DefaultIfEmpty()
			//					   where emp.IDEmployee == id
			//					   select new
			//					   {
			//						   emp.IDEmployee,
			//						   emp.LastName,
			//						   emp.FirstName,
			//						   emp.MiddleName,
			//						   emp.Phones,
			//						   emp.Office,
			//						   emp.Floor,
			//						   emp.Room,
			//						   emp.Code,
			//						   emp.Birthday,
			//						   PositionName = pos.Name,
			//						   DepartmentName = dp.Name,
			//						   BlockName = block.Name,
			//						   DivizionName = div.Name,
			//						   OrganizationName = org.Name,
			//						   Photo = empPhoto.Photo,
			//					   });
			//		var v = request.First();

			//		var edm = new EmployeeDetailsModel();
			//		edm.IDEmployee = v.IDEmployee;
			//		edm.FirstName = v.FirstName;
			//		edm.LastName = v.LastName;
			//		edm.MiddleName = v.MiddleName;
			//		edm.Office = v.Office;
			//		edm.Floor = v.Floor;
			//		edm.Room = v.Room;
			//		edm.Code = v.Code;
			//		edm.PositionName = v.PositionName;
			//		edm.DepartmentName = v.DepartmentName;
			//		edm.BlockName = v.BlockName;
			//		edm.DivizionName = v.DivizionName;
			//		edm.OrganizationName = v.OrganizationName;
			//		edm.Birthday = v.Birthday  == null ? "" : ((DateTime) v.Birthday).ToString("dd MMMM");
			//		edm.Code = edm.Code.Replace(" ", string.Empty);
			//		edm.Photo = v.Photo;



			//		edm.Phones = v.Phones == null ? new string[0] : XElement.Parse(v.Phones).Elements("i").Select(i => (string) i.Attribute("num")).ToArray();
			//		//ICollection<Phone> phones = dc.Phones.Where(a => a.EmployeeID == id).OrderBy(a => a.LineNumber).ToList();


			var db = new MySQLEntities();
			SelectList masters = new SelectList(db.masters, "idMasters", "name");
			ViewBag.Masters = masters;

			return PartialView();
			//		}
			//	}
			//			#pragma warning disable CS0168 // The variable 'ex' is declared but never used
			//			catch (Exception ex)
			//			#pragma warning restore CS0168 // The variable 'ex' is declared but never used
			//			{
			//				return HttpNotFound();
			//}

		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult IDataFillingWindow()
		{
			return View();
		}

		public ActionResult Masters()
		{
			return View();
		}

		public ActionResult Works()
		{
			return View();
		}

		public ActionResult Districts()
		{
			return View();
		}

		public ActionResult Worktypes()
		{
			return View();
		}

		public ActionResult Customers()
		{
			return View();
		}

		public ActionResult IndexDepartment()
		{
			ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
			return RedirectToAction("Index", "Home");
		}

		//[WebMethod]
		//public JsonResult GetData() //ActionResult
		//{
		//	using (var dc = new MyDatabaseEntities())
		//	{
		//		var query = (from emp in dc.Employees
		//						 //where emp.IsWorking
		//					 join pos in dc.Positions on emp.PositionID equals pos.IDPosition
		//					 join dp in dc.Departments on emp.DepartmentID equals dp.IDDepartment
		//					 join empPhoto in dc.EmployeePhotoes on emp.IDEmployee equals empPhoto.EmployeeID into ps
		//					 from empPhoto in ps.DefaultIfEmpty()
		//					 select new
		//					 {
		//						 emp.IDEmployee,
		//						 Fio = emp.LastName + " " + emp.FirstName + " " + emp.MiddleName,
		//						 emp.LastName,
		//						 emp.FirstName,
		//						 emp.MiddleName,
		//						 Phone = emp.Phones,
		//						 emp.DateWorkEnd,
		//						 PositionName = pos.Name,
		//						 DepartmentName = dp.Name,
		//						 Place = emp.Office + " " + emp.Floor + " " + emp.Room,
		//						 emp.Office,
		//						 emp.Floor,
		//						 emp.Room,
		//						 //PhotoByte = empPhoto.Photo,
		//						 Photo = ""
		//					 })
		//		.OrderBy(a => a.Fio);

		//		var result = query.AsEnumerable()
		//				   .Select(item => new
		//				   {
		//					   IDEmployee = item.IDEmployee,
		//					   Fio = item.LastName + " " + item.FirstName + " " + item.MiddleName,
		//					   LastName = item.LastName,
		//					   FirstName = item.FirstName,
		//					   MiddleName = item.MiddleName,
		//					   Phone = item.Phone == null ? null : (string) XElement.Parse(item.Phone).Elements("i").Select(i => (string) i.Attribute("num")).First(),
		//					   DateWorkEnd = item.DateWorkEnd,
		//					   PositionName = item.PositionName,
		//					   DepartmentName = item.DepartmentName,
		//					   Place = item.Place,
		//					   Office = item.Office,
		//					   Floor = item.Floor,
		//					   Room = item.Room
		//				   }).ToArray();

		//		return Json(result, JsonRequestBehavior.AllowGet);
		//	}
		//}

		//		public ActionResult Details(Guid? id)
		//		{
		//			if (id == null)
		//			{
		//				return HttpNotFound();
		//			}

		//			try
		//			{
		//				using (var dc = new MyDatabaseEntities())
		//				{

		//					var request = (from emp in dc.Employees
		//								   join pos in dc.Positions on emp.PositionID equals pos.IDPosition
		//								   join dp in dc.Departments on emp.DepartmentID equals dp.IDDepartment
		//								   join block in dc.Departments on dp.ParentID equals block.IDDepartment into blockvr
		//								   from block in blockvr.DefaultIfEmpty()
		//								   join div in dc.Departments on emp.DivisionID equals div.IDDepartment
		//								   join org in dc.Organizations on emp.OrganizationID equals org.IDOrganization
		//								   join empPhoto in dc.EmployeePhotoes on emp.EmployeePhotoID equals empPhoto.IDEmployeePhoto into ps
		//								   from empPhoto in ps.DefaultIfEmpty()
		//								   where emp.IDEmployee == id
		//								   select new
		//								   {
		//									   emp.IDEmployee,
		//									   emp.LastName,
		//									   emp.FirstName,
		//									   emp.MiddleName,
		//									   emp.Phones,
		//									   emp.Office,
		//									   emp.Floor,
		//									   emp.Room,
		//									   emp.Code,
		//									   emp.Birthday,
		//									   PositionName = pos.Name,
		//									   DepartmentName = dp.Name,
		//									   BlockName = block.Name,
		//									   DivizionName = div.Name,
		//									   OrganizationName = org.Name,
		//									   Photo = empPhoto.Photo,
		//								   });
		//					var v = request.First();

		//					var edm = new EmployeeDetailsModel();
		//					edm.IDEmployee = v.IDEmployee;
		//					edm.FirstName = v.FirstName;
		//					edm.LastName = v.LastName;
		//					edm.MiddleName = v.MiddleName;
		//					edm.Office = v.Office;
		//					edm.Floor = v.Floor;
		//					edm.Room = v.Room;
		//					edm.Code = v.Code;
		//					edm.PositionName = v.PositionName;
		//					edm.DepartmentName = v.DepartmentName;
		//					edm.BlockName = v.BlockName;
		//					edm.DivizionName = v.DivizionName;
		//					edm.OrganizationName = v.OrganizationName;
		//					edm.Birthday = v.Birthday  == null ? "" : ((DateTime) v.Birthday).ToString("dd MMMM");
		//					edm.Code = edm.Code.Replace(" ", string.Empty);
		//					edm.Photo = v.Photo;


		//					////string pathToCasheDirectory = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
		//					//// string pathToDirectory = pathToCasheDirectory + @"\ImagesEmployee";
		//					////string pathToImage = pathToDirectory + @"\" + edm.Code.ToString() + ".jpg";
		//					////string pathToImage = edm.Code.ToString() + ".jpg"; //@"~/" + 
		//					////if (!System.IO.File.Exists(pathToImage))
		//					////{
		//					//    //if (!System.IO.Directory.Exists(pathToDirectory))
		//					//    //{
		//					//    //    System.IO.Directory.CreateDirectory(pathToDirectory);
		//					//    //}

		//					////    if (v.Photo != null)
		//					////    {
		//					////        using (BinaryWriter writer = new BinaryWriter(System.IO.File.Open(pathToImage, FileMode.Create)))
		//					////        {
		//					////            writer.Write(v.Photo);
		//					////        }
		//					////    }

		//					////}

		//					////edm.PathToImage = pathToImage;

		//					edm.Phones = v.Phones == null ? new string[0] : XElement.Parse(v.Phones).Elements("i").Select(i => (string) i.Attribute("num")).ToArray();
		//					//ICollection<Phone> phones = dc.Phones.Where(a => a.EmployeeID == id).OrderBy(a => a.LineNumber).ToList();


		//					return PartialView(edm);
		//				}
		//			}
		//#pragma warning disable CS0168 // The variable 'ex' is declared but never used
		//			catch (Exception ex)
		//#pragma warning restore CS0168 // The variable 'ex' is declared but never used
		//			{
		//				return HttpNotFound();
		//			}

		//			//if (c != null)
		//			//    return PartialView(c);
		//			//return HttpNotFound();
		//		}

		//public ActionResult GetImage(string Id)
		//{
		//	using (var dc = new MyDatabaseEntities())
		//	{

		//		var v = (from p in dc.EmployeePhotoes where p.EmployeeID == new Guid("54F611A2-8242-11E6-814F-000C29DD9978") select p).First();
		//		return File(v.Photo, "image/jpg");
		//	}
		//}

	}

	/////

	//int amountOfAttemps = 0;
	//int amountWindow = 0;
	//int positiveImageNumberForm;
	//Button positiveButton;



	//private void Window_Loaded(object sender, RoutedEventArgs e)
	//{
	//	SetImage();
	//	amountWindow++;
	//}

	//private void ButtonImage_Click(object sender, RoutedEventArgs e)
	//{

	//	amountOfAttemps = amountOfAttemps + 1;

	//	if (sender != positiveButton)
	//	{
	//		return;
	//	}

	//	amountWindow++;
	//	SetImage();

	//	if (amountWindow!=2) //1
	//	{
	//		return;
	//	}

	//	PreliminaryResultWindow w1 = new PreliminaryResultWindow();
	//	w1.Percent = 100*10 / amountOfAttemps;
	//	w1.NextWindow = "WordsSearchDescriptionWindow";

	//	App currentApp = (App) Application.Current;
	//	currentApp.RootGrid.Children.Clear();
	//	currentApp.RootGrid.Children.Add(w1);

	public static class StringExt
	{
		public static bool IsEmpty(this string s)
		{
			return string.IsNullOrEmpty(s);
		}
	}

}



