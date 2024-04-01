using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SportsStore.Data.Repositories.EntityFrameworkRepositories;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Entities.Interfaces;
using SportsStore.Domain.Repositories;

namespace SportsStore.Data.Repositories.AdoNetRepositories;

public class ProductRepository : IProductRepository
{
	readonly DatabaseConnection database;

	public ProductRepository(DatabaseConnection database)
	{
		this.database = database;
	}

	public async Task<Product> Create(Product product)
	{
		var id = Guid.NewGuid();
		string sql = @$"INSERT INTO Products 
				VALUES(@id, @Name, @description,
				@Price,@Discount, GETDATE(),'{product.CategoryId}')";
		using (var connection = await database.GetConnection())
		{
			using (var command = new SqlCommand(sql, connection))
			{
				var idParam = command.CreateParameter();
				idParam.ParameterName = "@id";
				idParam.Value = id;
				var descriptionParam = command.CreateParameter();
				descriptionParam.ParameterName = "@description";
				descriptionParam.Value = product.Description;
				var nameParam  = command.CreateParameter();
				nameParam.ParameterName = "Name";
				nameParam.Value = product.Name;
				var priceParam = command.CreateParameter();
				priceParam.ParameterName = "Price";
				priceParam.Value = product.Price;
				var discountParam = command.CreateParameter();
				discountParam.ParameterName = "Discount";
				discountParam.Value = product.Discount;
				try
				{
					command.Parameters.Add(idParam);
					command.Parameters.Add(descriptionParam);
					command.Parameters.Add(nameParam);
					command.Parameters.Add(priceParam);
					command.Parameters.Add(discountParam);
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

	public async Task<Product> Delete(Guid Id)
	{
		var productToRemove = await Get(Id);
		var sql = $"DELETE  FROM Products WHERE Id = '{Id}'";
		using (var connection = await database.GetConnection())
		{
			using (var command = new SqlCommand(sql, connection))
			{

				command.CommandType = System.Data.CommandType.Text;
				try
				{
					await command.ExecuteNonQueryAsync();

				}
				catch(Exception e)
				{

				}
			}
		}

		return productToRemove;
	}

	public async Task<Product> Get(Guid Id)
	{
		var sql =
			$"SELECT * FROM Products WHERE Id = '{Id}'";
		using (var connection = await database.GetConnection())
		{
			using (var command = new SqlCommand(sql, connection))
			{
				command.CommandType = System.Data.CommandType.Text;

				using var reader = await command.ExecuteReaderAsync();

				if (!await reader.ReadAsync())
				{
					return null;
				}

				var product = new Product();

				product.Id = (Guid)reader["Id"];
				product.Name = (string)reader["Name"];
				product.Description = (string)reader["Description"];
				product.Price = (decimal)reader["Price"];
				product.Discount = (int)reader["Discount"];
				product.CategoryId = (Guid)reader["CategoryId"];
				product.CreateDate = (DateTime)reader["CreateDate"];

				return product;
			}
		}
	}

	public IQueryable<Product> GetAll(bool includeCategory)
	{
		var sql =includeCategory?$@"
		SELECT p.Id,p.Name,p.Description,p.Price,p.Discount,p.CreateDate,p.CategoryId, c.Name CategoryName, c.Description CategoryDescription,c.CreateDate CategoryCreateDate
		FROM Products p
		JOIN Categories c ON p.CategoryId = c.Id":
					$"SELECT * FROM Products ";
		using (var connection = database.GetConnection().Result)
		{
			using (var command = new SqlCommand(sql, connection))
			{
				command.CommandType = System.Data.CommandType.Text;

				using var reader = command.ExecuteReader();

				List<Product> list = new List<Product>();
				while (reader.Read())
				{
					var product = new Product();

					product.Id = (Guid)reader["Id"];
					product.Name = (string)reader["Name"];
					product.Description = (string)reader["Description"];
					product.Price = (decimal)reader["Price"];
					product.Discount = (int)reader["Discount"];
					product.CategoryId = (Guid)reader["CategoryId"];
					product.CreateDate = (DateTime)reader["CreateDate"];
					if(includeCategory)
					{
						product.Category = new Category();
						product.Category.Id = (Guid)reader["CategoryId"];
						product.Category.Name = (string)reader["CategoryName"];
						product.Category.Description = (string)reader["CategoryDescription"];
						product.Category.CreateDate = (DateTime)reader["CategoryCreateDate"];
					}
					list.Add(product);
				}
				return list.AsQueryable();
			}
		}
	}

	public async Task<Product> Update(Product product)
	{
		var id = Guid.NewGuid();
		string sql = @$"update Products 
		set Name = @Name,Description = @Description,Price = @Price,Discount = @Discount,CategoryId='{product.CategoryId}'
		where id = @Id";
		using (var connection = await database.GetConnection())
		{
			using (var command = new SqlCommand(sql, connection))
			{
				var idParam = command.CreateParameter();
				idParam.ParameterName = "@id";
				idParam.Value = id;
				var descriptionParam = command.CreateParameter();
				descriptionParam.ParameterName = "@description";
				descriptionParam.Value = product.Description;
				var nameParam = command.CreateParameter();
				nameParam.ParameterName = "Name";
				nameParam.Value = product.Name;
				var priceParam = command.CreateParameter();
				priceParam.ParameterName = "Price";
				priceParam.Value = product.Price;
				var discountParam = command.CreateParameter();
				discountParam.ParameterName = "Discount";
				discountParam.Value = product.Discount;

				command.Parameters.Add(idParam);
				command.Parameters.Add(descriptionParam);
				command.Parameters.Add(nameParam);
				command.Parameters.Add(priceParam);
				command.Parameters.Add(discountParam);
				command.CommandType = System.Data.CommandType.Text;

				await command.ExecuteNonQueryAsync();
			}
		}
 
		return await Get(id);
	}
}

