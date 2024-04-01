using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SportsStore.Data.Repositories.EntityFrameworkRepositories;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Repositories;

namespace SportsStore.Data.Repositories.AdoNetRepositories;

public class OrderDetailRepository : IOrderDetailRepository
{
	readonly DatabaseConnection database;

	public OrderDetailRepository(DatabaseConnection database)
		=> this.database = database;

	public async Task<OrderDetail> Create(OrderDetail orderDetail)
	{
		var id = Guid.NewGuid();
		string sql = $@"INSERT INTO OrderDetails
		VALUES (@id, @orderId, @productIdParam, @countParam,GETDATE())";
		using (var connection = await database.GetConnection())
		{
			using (var command = new SqlCommand(sql, connection)) 
			{
				var idParam = command.CreateParameter();
				idParam.ParameterName = "id";
				idParam.Value = id;
				var orderIdParam = command.CreateParameter();
				orderIdParam.ParameterName = "orderId";
				orderIdParam.Value = orderDetail.OrderId;
				var productIdParam = command.CreateParameter();
				productIdParam.ParameterName = "productIdParam";
				productIdParam.Value = orderDetail.ProductId;
				var countParam = command.CreateParameter();
				countParam.ParameterName = "countParam";
				countParam.Value = orderDetail.Count;
				try
				{
					command.Parameters.Add(idParam);
					command.Parameters.Add(orderIdParam);
					command.Parameters.Add(productIdParam);
					command.Parameters.Add(countParam);
					command.CommandType = System.Data.CommandType.Text;
					await command.ExecuteNonQueryAsync();
				}
				catch (Exception ex)
				{
					await Console.Out.WriteLineAsync(ex.Message);
				}
			}
		}
		return await Get(id);
	}

	public async Task<OrderDetail> Delete(Guid Id)
	{
		var orderDetailRemove = await Get(Id);
		var sql = $"DELETE FROM OrderDetails WHere Id = '{Id}'";
		using (var connection = await database.GetConnection())
		{
			using (var command = new SqlCommand(sql, connection))
			{
				command.CommandType = System.Data.CommandType.Text;
				await command.ExecuteNonQueryAsync();
			}
		}
		return orderDetailRemove;
	}

	public async Task<OrderDetail> Get(Guid Id)
	{
		var sql = $@"SELECT * FROM OrderDetails Where Id = '{Id}'";
		using (var connection = await database.GetConnection())
		{
			using (var command = new SqlCommand(sql, connection))
			{
				command.CommandType= System.Data.CommandType.Text;
				using var reader = await command.ExecuteReaderAsync();

				if(!await reader.ReadAsync())
				{
					return null;
				}
				var orderDetail = new OrderDetail();
				orderDetail.Id = (Guid)reader["Id"];
				orderDetail.OrderId = (Guid)reader["OrderId"];
				orderDetail.ProductId = (Guid)reader["ProductId"];
				orderDetail.Count = (int)reader["Count"];
				orderDetail.CreateDate = (DateTime)reader["CreateDate"];

				return orderDetail;
			}
		}
	}

	public IQueryable<OrderDetail> GetAll(bool includeProduct)
	{
		var sql = includeProduct ? $@"SELECT od.Id,
		od.OrderId,
		od.ProductId,
		od.Count,
		od.CreateDate ,
		o.OrderStatus,
		o.CustomerName,
		o.CreateDate orderCreateDate,
		p.Name productName,
		p.Description productDescription,
		p.Price productPrice,
		p.Discount productDiscount,
		p.CreateDate productCreateDate,
		p.CategoryId productCategoryId
		FROM OrderDetails od
		JOIN Products p ON od.ProductId = p.Id
		JOIN Orders o ON od.OrderId = o.Id" : $"Select * from OrderDetails";
		using (var connection = database.GetConnection().Result)
		{
			List<OrderDetail> orderDetails = new List<OrderDetail>();
			using (var command = new SqlCommand(sql,connection))
			{
				command.CommandType= System.Data.CommandType.Text;

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

					orderDetail.Order = new Order();
					orderDetail.Order.Id = (Guid)reader["OrderId"];
					orderDetail.Order.Status = (OrderStatus)(int.Parse(reader["OrderStatus"].ToString()));
					orderDetail.Order.CustomerName = (string)reader["CustomerName"];
					orderDetail.Order.CreateDate = (DateTime)reader["orderCreateDate"];

					orderDetails.Add(orderDetail);
				}
			}
			return orderDetails.AsQueryable();
		}
	}

	public async Task<List<OrderDetail>> GetByOrderId(Guid orderId)
	{
		var sql = $@"SELECT 
		od.Id,
		od.OrderId,
		od.ProductId,
		od.Count,
		od.CreateDate,
		p.Name productName,
		p.Description productDescription,
		p.Price productPrice,
		p.Discount productDiscount,
		p.CategoryId productCategoryId,
		p.CreateDate productCreateDate

		FROM OrderDetails  od
		join Products p on p.Id = od.ProductId
		WHERE OrderId = '{orderId}'";
		using (var connection = await database.GetConnection())
		{
			List<OrderDetail> orderDetails = new List<OrderDetail>();
			using (var command = new SqlCommand(sql, connection))
			{
				command.CommandType = System.Data.CommandType.Text;

				using var reader = command.ExecuteReader();
				while (reader.Read())
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
			return orderDetails.ToList();
		}
	}

	public async Task<OrderDetail> Update(OrderDetail orderDetail)
	{
		var id = Guid.NewGuid();
		string sql = $@"UPDATE OrderDetails
		SET OrderId = @orderIdParam,ProductId = @orderDetailProductIdParam, Count = @countParam
		Where id = @idParam";
		using (var connection = await database.GetConnection())
		{
			using(var  command = new SqlCommand(sql, connection))
			{
				var idParam = command.CreateParameter();
				idParam.ParameterName = "idParam";
				idParam.Value = orderDetail.Id;
				var orderIdParam = command.CreateParameter();
				orderIdParam.ParameterName = "orderIdParam";
				orderIdParam.Value = orderDetail.OrderId;
				var orderDetailProductIdParam = command.CreateParameter();
				orderDetailProductIdParam.ParameterName = "orderDetailProductIdParam";
				orderDetailProductIdParam.Value = orderDetail.ProductId;
				var countParam = command.CreateParameter();
				countParam.ParameterName = "countParam";
				countParam.Value = orderDetail.Count;

				command.Parameters.Add(idParam);
				command.Parameters.Add(orderIdParam);
				command.Parameters.Add(orderDetailProductIdParam);
				command.Parameters.Add(countParam);
				command.CommandType= System.Data.CommandType.Text;
				await command.ExecuteNonQueryAsync();
			}
		}
		return await Get(id);
	}
}
