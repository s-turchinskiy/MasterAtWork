namespace WebEmployees
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;

	public partial class districtsmasters
	{
		public int? index { get; set; }
	}

	public partial class worksorders
	{
		public int? index { get; set; }
	}

	public partial class worktypesmasters
	{
		public int? index { get; set; }
	}

	public partial class districts
	{
		public int index { get; set; }
	}

	public partial class works
	{
		public int index { get; set; }
	}

	public partial class worktypes
	{
		public int index { get; set; }
	}

	public class WorkInJSON
	{
		public int price { get; set; }
		public string idworks { get; set; }
		public string name { get; set; }
	}

	public class OrderInJSON
	{
		public int status { get; set; }
		public string masterComment { get; set; }
		public string work { get; set; }
		public string customerComment { get; set; }
		public string formationDate { get; set; }
		public string customer { get; set; }
		public string idOrders { get; set; }
		public int initialAmount { get; set; }
		public string takenDate { get; set; }
		public string master { get; set; }
		public string masterGuidIn1C { get; set; }
		public string district { get; set; }
		public string orderNumber { get; set; }
		public string endDate { get; set; }
		public int totalAmount { get; set; }
		public string workType { get; set; }
	}

	public class MasterInJSON
	{
		public string idMasters { get; set; }
		public string idFirebase { get; set; }
		public string email { get; set; }
		public int deposit { get; set; }
		public string nameInEmail { get; set; }
		public string nameIn1C { get; set; }
	}

	public class CustomerInJSON
	{
		public string phone { get; set; }
		public string district { get; set; }
		public string privateAddress { get; set; }
		public string idcustomers { get; set; }
		public string name { get; set; }
		public string publicAddress { get; set; }
	}

	public class DistrictInJSON
	{
		public string name { get; set; }
		public string iddistricts { get; set; }
	}

	public class WorkTypeInJSON
	{
		public string name { get; set; }
		public string idWorkTypes { get; set; }
	}

	public class StatusInJSON
	{
		public int codeStatuses { get; set; }
		public string name { get; set; }
	}

	public class DepartmentNotModel
	{
		public Guid IDDepartment { get; set; }
		public string Name { get; set; }
		public Guid Parent { get; set; }
	}

	public class EmployeeDetailsModel
	{
		public System.Guid IDEmployee { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string Phone { get; set; }
		public string Office { get; set; }
		public string Floor { get; set; }
		public string Room { get; set; }
		public string Code { get; set; }

		public string PositionName { get; set; }
		public string DepartmentName { get; set; }
		public string BlockName { get; set; }
		public string DivizionName { get; set; }
		public string OrganizationName { get; set; }
		public string PathToImage { get; set; }

		public byte[] Photo { get; set; }
		public string Birthday { get; set; }
		public string ZodiacSign { get; set; }

		public string[] Phones { get; set; }
	}

	public class Phone
	{
		public string Number { get; set; }
	}

	public class FocusModification
	{
		public int positiveImageNumberForm { get; set; }
	}

}