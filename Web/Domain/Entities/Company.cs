using System.Collections.Generic;

namespace Web.Domain.Entities
{
	public class Company
	{
		public int Id { get; private set; }
		public string Name { get; set; }

		public virtual ICollection<Order> Orders { get; private set; }

		public Company()
		{
			this.Orders = new HashSet<Order>();
		}

		public Company(string name)
			: this()
		{
			this.Name = name;
		}

		public Company(int id, string name)
			: this()
		{
			this.Id = id;
			this.Name = name;
		}

		public void AddOrder(Order order)
		{
			order.AssignCompany(this);
			this.Orders.Add(order);
		}
	}
}