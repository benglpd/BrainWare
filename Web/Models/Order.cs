using System.Collections.Generic;

namespace Web.Models
{
	public class Order
    {
        public int OrderId { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public decimal OrderTotal { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }

        public Order()
        {

        }

        public Order(Web.Domain.Entities.Order entity)
        {
            this.OrderId = entity.Id;
            this.CompanyName = entity.Company.Name;
            this.Description = entity.Description;
            this.OrderTotal = entity.Total;
            this.OrderProducts = new List<OrderProduct>();
            foreach (Domain.Entities.OrderProduct detail in entity.Details)
            {
                this.OrderProducts.Add(new OrderProduct(detail));
            }
        }
    }


    public class OrderProduct
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    
        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public OrderProduct()
        {

        }

        public OrderProduct(Domain.Entities.OrderProduct entity)
            : this()
        {
            this.OrderId = entity.OrderId;
            this.ProductId = entity.ProductId;
            this.Price = entity.Price;
            this.Quantity = entity.Quantity;
            this.Product = new Product(entity.Product);
        }
    }

    public class Product
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public Product()
        {

        }

        public Product(Domain.Entities.Product entity)
            : this()
        {
            this.Name = entity.Name;
            this.Price = entity.Price;
        }
    }
}