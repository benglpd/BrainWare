using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Domain.Entities
{
	public class Order
	{
		public int Id { get; private set; }
		public string Description { get; set; }
		public int CompanyId { get; private set; }

		public virtual Company Company { get; private set; }
		public virtual ICollection<OrderProduct> Details { get; private set; }

		public decimal Total => Details.Sum(x => x.Price * x.Quantity);

		public Order()
		{
			this.Details = new HashSet<OrderProduct>();
		}

		public Order(Company company)
			: this()
		{
			this.CompanyId = company.Id;
			this.Company = company;
		}

		public Order(int id, string description, int companyId)
			: this()
		{
			this.Id = id;
			this.Description = description;
			this.CompanyId = companyId;
		}

		public void AssignCompany(Company company)
		{
			if (company.Id != this.CompanyId)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.Company = company;
		}

		public void AddDetail(Product product, decimal price, int quantity)
		{
			this.Details.Add(new OrderProduct(this, product, price, quantity));
		}
	}
}