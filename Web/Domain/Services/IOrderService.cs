using System.Collections.Generic;
using Web.Domain.Entities;

namespace Web.Domain.Services
{
	public interface IOrderService
	{
		List<Order> GetOrdersForCompany(int companyId);
	}
}
