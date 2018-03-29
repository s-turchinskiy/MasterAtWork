using System.Web.Mvc;


namespace WebEmployees.Controllers
{
	public class TreeviewController : Controller
	{
		//
		// GET: /Treeview/

		public ActionResult Index()
		{
			return View();
		}

		//public ActionResult Simple()
		//{
		//    List<DepartmentNotModel> all = new List<DepartmentNotModel>();
		//    //using (MyDatabaseEntities dc = new MyDatabaseEntities())
		//    {
		//        DepartmentNotModel department = new DepartmentNotModel();

		//        //all = dc.SiteMenus.OrderBy(a => a.ParentMenuID).ToList();

		//        String connectionString = "Persist Security Info=False;User ID=s.turchinskiy;Pwd=s.turchinskiy;data source=DEVDB-V;initial catalog=Employees";
		//        SqlConnection connection = new SqlConnection(connectionString);
		//        string queryString = "SELECT Name FROM dbo.Department";
		//        SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);

		//        DataSet departments = new DataSet();
		//        adapter.Fill(departments, "Department");
		//        DataTable products = departments.Tables["Department"];

		//        DataContext db = new DataContext(connectionString);

		//        String SqlRequest = @"SELECT 
		//        *
		//        FROM dbo.Department
		//            order by Parent";
		//        IDbCommand command = new SqlCommand(SqlRequest);
		//        command.Connection = connection;
		//        connection.Open();
		//        IDataReader idr = command.ExecuteReader();
		//        while (idr.Read())
		//        {

		//            department = new DepartmentNotModel();
		//            department.IDDepartment = idr.GetGuid(0);
		//            department.Name = idr.GetString(1);
		//            if (idr.GetValue(2).ToString() != "")
		//            {
		//                department.Parent = idr.GetGuid(2);
		//            }
		//            all.Add(department);
		//        }

		//        //all = products.OrderBy(a => a.ParentMenuID).ToList();
		//    }
		//    return View(all);
		//}


	}
}
