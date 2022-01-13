using System.Collections.Generic;
using System.Linq;
using Web.Domain.Entities;
using Web.Domain.Services;
using Web.Infrastructure;

namespace Web.Services
{
	public class OrderService : IOrderService
	{
		private readonly BrainWareContext _dbContext;

		public OrderService(BrainWareContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<Order> GetOrdersForCompany(int companyId)
		{
			return _dbContext.Orders.ToList();
		}
	}
}