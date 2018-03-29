namespace WebEmployees
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;

	public class MasterAndTable
	{
		public masters master { get; set; }
		public List<districts> districts { get; set; }
		public List<worktypes> worktypes { get; set; }
		public List<districtsmasters> districtsmaster { get; set; }
		public List<worktypesmasters> worktypesmaster { get; set; }
		public List<int> districtsId { get; set; }
		public List<int> worktypesId { get; set; }

		public MasterAndTable()
		{
		}

		public MasterAndTable(masters master)
		{
			this.master = master;
			districtsId = new List<int>();
			worktypesId = new List<int>();

			using (var dc = new MySQLEntities())
			{
				districts = dc.districts.ToList();

				for (int i = 0; i <= districts.Count-1; i++)
				{
					districts[i].index = i;
					districtsId.Add(i);
				}

				worktypes = dc.worktypes.ToList();
				for (int i = 0; i <= worktypes.Count-1; i++)
				{
					worktypes[i].index = i;
					worktypesId.Add(i);
				}

				districtsmaster = (from item in dc.districtsmasters
								   where item.master == master.idMasters
								   select item).ToList();

				foreach (var item in districtsmaster)
				{
					item.index = districts.Where(c => c.iddistricts == item.district).First().index;
				}

				worktypesmaster = (from item in dc.worktypesmasters
								   where item.master == master.idMasters
								   select item).ToList();

				foreach (var item in worktypesmaster)
				{
					item.index = worktypes.Where(c => c.idWorkTypes == item.worktype).First().index;
				}
			}
		}

		public void AddDistrictMaster()
		{

			if (districtsmaster==null)
			{
				districtsmaster = new List<districtsmasters>();
			}

			var newObject = new districtsmasters();
			newObject.index = null;
			districtsmaster.Add(newObject);
		}

		public void RemoveDistrictMaster(int index)
		{
			districtsmaster.RemoveAt(index);
		}

		public void AddWorktypeMaster()
		{

			if (worktypesmaster==null)
			{
				worktypesmaster = new List<worktypesmasters>();
			}

			var newObject = new worktypesmasters();
			newObject.index = null;
			worktypesmaster.Add(newObject);
		}

		public void RemoveWorktypeMaster(int index)
		{
			worktypesmaster.RemoveAt(index);
		}

		public void Save()
		{
			try
			{
				using (var dc = new MySQLEntities())
				{
					if (master.idMasters == Guid.Empty|| dc.masters.Find(master.idMasters)==null)
					{

						master.idMasters = Guid.NewGuid();
						dc.masters.Add(master);
					}
					else
					{

						dc.Entry(master).State = EntityState.Modified;
					}

					//районы
					var result = (from c in dc.districtsmasters
								  where c.master == master.idMasters
								  select c);

					if (result.Count()!=0)
					{
						foreach (districtsmasters item in result)
						{
							dc.districtsmasters.Remove(item);
						}
					}

					if (districtsmaster!=null && districtsmaster.Count()!=0)
					{
						foreach (var item in districtsmaster)
						{
							//if (item.index == )
							item.master = master.idMasters;
							item.district = districts[(int) item.index].iddistricts;
							dc.districtsmasters.Add(item);
						}
					}

					//виды деятельности
					var result2 = (from c in dc.worktypesmasters
								   where c.master == master.idMasters
								   select c);

					if (result.Count()!=0)
					{
						foreach (worktypesmasters item in result2)
						{
							dc.worktypesmasters.Remove(item);
						}
					}

					if (worktypesmaster!=null && worktypesmaster.Count()!=0)
					{
						foreach (var item in worktypesmaster)
						{
							//if (item.index == )
							item.master = master.idMasters;
							item.worktype = worktypes[(int) item.index].idWorkTypes;
							dc.worktypesmasters.Add(item);
						}
					}
					//-

					dc.SaveChanges();

					//foreach (var item in worktypesmasters)
					//{
					//	item.master = master.idMasters;
					//	dc.districtsmasters.Add(item);
					//	dc.SaveChanges();
					//}
				}
			}
			catch (Exception ex)
			{

			}
		}
	}


}