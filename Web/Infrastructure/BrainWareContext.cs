using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Web.Domain.Entities;

namespace Web.Infrastructure
{
	public class BrainWareContext
	{
		private readonly string _connectionString;

		public IQueryable<Company> Companies { get; private set; }
		public IQueryable<Order> Orders { get; private set; }
		public IQueryable<Product> Products { get; private set; }
		public IQueryable<OrderProduct> OrderProducts { get; private set; }

		public BrainWareContext()
		{
			ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["BrainWareConnectionString"];
			_connectionString = settings.ConnectionString;
			LoadData();
		}

		public void LoadData()
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				string sql = "SELECT company_id, name FROM Company;";
				sql += "SELECT order_id, description, company_id FROM [Order];";
				sql += "SELECT product_id, name, price FROM Product;";
				sql += "SELECT order_id, product_id, price, quantity FROM OrderProduct;";
				SqlCommand cmd = new SqlCommand(sql, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				LoadCompanies(reader);
				reader.NextResult();
				LoadOrders(reader);
				reader.NextResult();
				LoadProducts(reader);
				reader.NextResult();
				LoadOrderProducts(reader);
				ProcessEagerLoading();
			}
		}

		private void ProcessEagerLoading()
		{
			foreach (Company company in Companies)
			{
				foreach (Order order in Orders.Where(x => x.CompanyId == company.Id))
				{
					foreach (OrderProduct detail in OrderProducts.Where(x => x.OrderId == order.Id))
					{
						order.AddDetail(Products.FirstOrDefault(x => x.Id == detail.ProductId), detail.Price, detail.Quantity);
					}
					company.AddOrder(order);
				}
			}
		}

		private void LoadCompanies(SqlDataReader reader)
		{
			List<Company> list = new List<Company>();
			while (reader.Read())
			{
				list.Add(new Company(reader.GetInt32(0), reader.GetString(1)));
			}
			Companies = list.AsQueryable();
		}

		private void LoadOrders(SqlDataReader reader)
		{
			List<Order> list = new List<Order>();
			while (reader.Read())
			{
				list.Add(new Order(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
			}
			Orders = list.AsQueryable();
		}

		private void LoadProducts(SqlDataReader reader)
		{
			List<Product> list = new List<Product>();
			while (reader.Read())
			{
				list.Add(new Product(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2)));
			}
			Products = list.AsQueryable();
		}

		private void LoadOrderProducts(SqlDataReader reader)
		{
			List<OrderProduct> list = new List<OrderProduct>();
			while (reader.Read())
			{
				list.Add(new OrderProduct(
					reader.GetInt32(0),
					reader.GetInt32(1),
					reader.IsDBNull(2) ? 0.0m : reader.GetDecimal(2),
					reader.GetInt32(3)
				));
			}
			OrderProducts = list.AsQueryable();
		}
	}
}