namespace Web.Domain.Entities
{
	public class Product
	{
		public int Id { get; private set; }
		public string Name { get; set; }
		public decimal Price { get; set; }

		public Product()
		{

		}

		public Product(int id, string name, decimal price)
			: this()
		{
			this.Id = id;
			this.Name = name;
			this.Price = price;
		}
	}
}