namespace Web.Domain.Entities
{
	public class OrderProduct
	{
		public int OrderId { get; private set; }
		public int ProductId { get; private set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }

		public virtual Order Order { get; set; }
		public virtual Product Product { get; set; }

		public OrderProduct(int orderId, int productId, decimal price, int quantity)
		{
			this.OrderId = orderId;
			this.ProductId = productId;
			this.Price = price;
			this.Quantity = quantity;
		}

		public OrderProduct(Order order, Product product, decimal price, int quantity)
			: this(order.Id, product.Id, price, quantity)
		{
			this.Order = order;
			this.Product = product;
		}
	}
}