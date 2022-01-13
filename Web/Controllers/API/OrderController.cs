using System.Collections.Generic;
using System.Web.Http;

namespace Web.Controllers
{
	using System.Web.Mvc;
	using Infrastructure;
	using Models;
	using Web.Domain.Services;
	using Web.Services;

	public class OrderController : ApiController
    {
        private readonly IOrderService _orderService;

		public OrderController()
		{
            _orderService = new OrderService(new BrainWareContext());
		}

        [HttpGet]
        public IEnumerable<Order> GetOrders(int id = 1)
        {
            var orders = _orderService.GetOrdersForCompany(id);
            List<Order> returnOrderList = new List<Order>();
            foreach (Domain.Entities.Order order in orders)
            {
                returnOrderList.Add(new Order(order));
            }
            return returnOrderList;
        }
    }
}
