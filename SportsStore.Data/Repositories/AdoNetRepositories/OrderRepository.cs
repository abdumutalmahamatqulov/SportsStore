using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Repositories;

namespace SportsStore.Data.Repositories.AdoNetRepositories;

public class OrderRepository : IOrderRepository
{
	private readonly DatabaseConnection database;
	private readonly IOrderDetailRepository _orderDetailRepository;

	public OrderRepository(IOrderDetailRepository orderDetailRepository, DatabaseConnection database)
	{
		_orderDetailRepository = orderDetailRepository;
		this.database = database;
	}


	public async Task<Order> Create(Order order)
	{
		var id = Guid.NewGuid();
		string sql = $@"INSERT INTO Orders
				VALUES(@id, @orderStatusParam, @customerName, GETDATE())";
		using (var connection = await database.GetConnection())
		{
			using (var command = new SqlCommand(sql, connection))
			{
				var idParam = command.CreateParameter();
				idParam.ParameterName = "id";
				idParam.Value = id;
				var orderStatusParam = command.CreateParameter();
				orderStatusParam.ParameterName = "orderStatusParam";
				orderStatusParam.Value = (int)order.Status;
				var customerNameParam = command.CreateParameter();
				customerNameParam.ParameterName = "customerName";
				customerNameParam.Value = order.CustomerName;
				try
				{
					command.Parameters.Add(idParam);
					command.Parameters.Add(orderStatusParam);
					command.Parameters.Add(customerNameParam);
					command.CommandType = System.Data.CommandType.Text;
					await command.ExecuteNonQueryAsync();
				}
				catch (Exception ex)
				{
					await Console.Out.WriteLineAsync(ex.Message);
				}
			}
		}
		return await Get(id, true);
	}

	public async Task<Order> Delete(Guid Id)
	{
		var orderRemove = await Get(Id, true);
		var sql = $"DELETE FROM WHERE Id = '{Id}'";
		using (var connection = await database.GetConnection())
		{
			using (var command = new SqlCommand(sql,connection))
			{
				command.CommandType= System.Data.CommandType.Text; 
				await command.ExecuteNonQueryAsync();
			}
		}
		return orderRemove;
	}

	public async Task<Order> Get(Guid Id, bool includeDetail)
	{
		var sql = $"SELECT * FROM Orders Where Id = '{Id}'";
		using (var connection = await database.GetConnection())
		{
			var order = new Order();
			using (var command = new SqlCommand(sql, connection))
			{
				command.CommandType = System.Data.CommandType.Text;

				using var reader = await command.ExecuteReaderAsync();

				if(!await reader.ReadAsync())
				{
					return null;
				}

				order.Id = (Guid)reader["Id"];
				var s = reader["OrderStatus"];
				order.Status = (OrderStatus)(int.Parse(reader["OrderStatus"].ToString()));
				order.CustomerName = (string)reader["CustomerName"];
				order.CreateDate = (DateTime)reader["CreateDate"];

			}

			if (includeDetail)
			{
				order.OrderDetails =(await _orderDetailRepository.GetByOrderId(order.Id)).ToArray();
			}

			return order;
		}
	}

	public IQueryable<Order> GetAll(bool includeProduct)
	{
		var sql = $@"
		SELECT * FROM Orders";
		using (var connection= database.GetConnection().Result)
		{
			List<Order> orders = new List<Order>();
			using (var command = new SqlCommand(sql, connection))
			{
				command.CommandType = System.Data.CommandType.Text;

				using var reader = command.ExecuteReader();

				while (reader.Read())
				{
					var order = new Order();
					order.Id = (Guid)reader["Id"];
					var s = reader["OrderStatus"];
					order.Status = (OrderStatus)(int.Parse(reader["OrderStatus"].ToString()));
					order.CustomerName = (string)reader["CustomerName"];
					order.CreateDate = (DateTime)reader["CreateDate"];
					orders.Add(order);
				}
			}
			if (includeProduct)
			{
				var orderIds = string.Join(", ", orders.Select(x => $"'{x.Id}'"));

				sql = $@"
						SELECT od.Id,
						od.OrderId,
						od.ProductId,
						od.Count,
						od.CreateDate ,
						p.Name productName,
						p.Description productDescription,
						p.Price productPrice,
						p.Discount productDiscount,
						p.CreateDate productCreateDate,
						p.CategoryId productCategoryId
						FROM OrderDetails od
						JOIN Products p ON od.ProductId = p.Id
						WHERE OrderId IN ({orderIds})";

				var orderDetails = new List<OrderDetail>();
				using (var command = new SqlCommand(sql, connection))
				{
					command.CommandType = System.Data.CommandType.Text;
					using var reader = command.ExecuteReader();

					while(reader.Read())
					{
						var orderDetail = new OrderDetail();
						orderDetail.Id = (Guid)reader["Id"];
						orderDetail.OrderId = (Guid)reader["OrderId"];
						orderDetail.ProductId = (Guid)reader["ProductId"];
						orderDetail.Count = (int)reader["Count"];
						orderDetail.CreateDate = (DateTime)reader["CreateDate"];

						orderDetail.Product = new Product();
						orderDetail.Product.Id = (Guid)reader["ProductId"];
						orderDetail.Product.Name = (string)reader["productName"];
						orderDetail.Product.Description = (string)reader["productDescription"];
						orderDetail.Product.Price = (decimal)reader["productPrice"];
						orderDetail.Product.Discount = (int)reader["productDiscount"];
						orderDetail.Product.CategoryId = (Guid)reader["productCategoryId"];
						orderDetail.Product.CreateDate = (DateTime)reader["productCreateDate"];

						orderDetails.Add(orderDetail);
					}
				}




                foreach (var item in orders)
                {
					item.OrderDetails = orderDetails
						.Where(t => t.OrderId == item.Id).ToArray();

                }


            }
			return orders.AsQueryable();
		}

	}

	public async Task<Order> Update(Order order)
	{
		var id = Guid.NewGuid();
		string sql = $@"UPDATE Orders
		SET CustomerName = @customerName, OrderStatus = @status,
		Where id = @id";
		using (var connection = await database.GetConnection())
		{
			using (var command = new SqlCommand(sql, connection))
			{
				var idParam = command.CreateParameter();
				idParam.ParameterName = "id";
				idParam.Value = order.Id;
				var customerNameParam = command.CreateParameter();
				customerNameParam.ParameterName = "customerName";
				customerNameParam.Value = order.CustomerName;
				var orderStatusParam = command.CreateParameter();
				orderStatusParam.ParameterName = "status";
				orderStatusParam.Value = order.Status;

				command.Parameters.Add(idParam);
				command.Parameters.Add(customerNameParam);
				command.Parameters.Add(orderStatusParam);
				command.CommandType = System.Data.CommandType.Text;

				await command.ExecuteNonQueryAsync();
			}
		}
		return await Get(id, true);
	}
}
