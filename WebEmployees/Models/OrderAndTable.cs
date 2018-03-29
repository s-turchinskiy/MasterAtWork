namespace WebEmployees
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;

	public class OrderAndTable
	{
		public orders order { get; set; }
		public List<works> works { get; set; }
		public List<worksorders> worksorder { get; set; }
		public List<int> worksId { get; set; }

		public OrderAndTable()
		{
		}

		public OrderAndTable(orders order)
		{
			this.order = order;
			worksId = new List<int>();

			using (var dc = new MySQLEntities())
			{
				works = dc.works.ToList();

				for (int i = 0; i <= works.Count-1; i++)
				{
					works[i].index = i;
					worksId.Add(i);
				}

				worksorder = (from item in dc.worksorders
							  where item.order == order.idOrders
							  select item).ToList();

				foreach (var item in worksorder)
				{
					item.index = works.Where(c => c.idWorks == item.work).First().index;
				}
			}
		}

		public void AddWorkOrder()
		{

			if (worksorder==null)
			{
				worksorder = new List<worksorders>();
			}

			var newObject = new worksorders();
			newObject.index = null;
			worksorder.Add(newObject);
		}

		public void RemoveWorkOrder(int index)
		{
			worksorder.RemoveAt(index);
		}

		public void SaveTable()
		{
			try
			{
				using (var dc = new MySQLEntities())
				{
					//работы
					var result = (from c in dc.worksorders
								  where c.order == order.idOrders
								  select c);

					if (result.Count()!=0)
					{
						foreach (var item in result)
						{
							dc.worksorders.Remove(item);
						}
					}

					if (worksorder!=null && worksorder.Count()!=0)
					{
						foreach (var item in worksorder)
						{
							//if (item.index == )
							item.order = order.idOrders;
							item.work = works[(int) item.index].idWorks;
							dc.worksorders.Add(item);
						}
					}

					dc.SaveChanges();
				}
			}
			catch (Exception ex)
			{

			}
		}
	}


}