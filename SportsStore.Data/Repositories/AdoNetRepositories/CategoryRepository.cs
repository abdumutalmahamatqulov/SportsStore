using Microsoft.Data.SqlClient;
using SportsStore.Data.Repositories.EntityFrameworkRepositories;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Repositories;

namespace SportsStore.Data.Repositories.AdoNetRepositories;

public class CategoryRepository : ICategoryRepository
{
	private readonly DatabaseConnection database;

	public CategoryRepository(DatabaseConnection database)
	{
		this.database = database;
	}

	public async Task<Category> Create(Category entity)
	{
		var id = Guid.NewGuid();
		string sql = @$"INSERT INTO Categories
						VALUES(@id, @Name, @Description, GETDATE())";
		using (var connection = await database.GetConnection())
		{
			using(var command = new SqlCommand(sql, connection))
			{
				var idParam = command.CreateParameter();
				idParam.ParameterName = "id";
				idParam.Value = entity.Id;
				var nameParam = command.CreateParameter();
				nameParam.ParameterName = "name";
				nameParam.Value = entity.Name;
				var descriptionParam = command.CreateParameter();
				descriptionParam.ParameterName = "description";
				descriptionParam.Value = entity.Description;

				command.Parameters.Add(idParam);
				command.Parameters.Add(nameParam);
				command.Parameters.Add(descriptionParam);
				command.CommandType = System.Data.CommandType.Text;

				await command.ExecuteNonQueryAsync();
			}
		}

		return await Get(id);

	}
	public async Task<Category> Delete(Guid id)
	{
		var categoryToRemove = await Get(id);
		var sql =$"DELETE  FROM Categories WHERE Id = '{id}'";
		using (var connection = await database.GetConnection())
		{
			using (var command = new SqlCommand(sql, connection))
			{
				command.CommandType = System.Data.CommandType.Text;

				await command.ExecuteNonQueryAsync();
			}
		}

		return categoryToRemove;
	}
	public async Task<Category> Get(Guid id)
	{
		var sql = 
			$"SELECT * FROM Categories WHERE Id = '{id}'";
		using (var connection = await database.GetConnection())
		{
			using (var command = new SqlCommand(sql, connection))
			{
				command.CommandType = System.Data.CommandType.Text;

				using var reader = await command.ExecuteReaderAsync();

				if(!await reader.ReadAsync())
				{
					return null;
				}

				var category = new Category();

				category.Id = (Guid)reader["Id"];
				category.Name = (string)reader["Name"];
				category.Description = (string)reader["Description"];
				category.CreateDate = (DateTime)reader["CreateDate"];

				return category;
			}
		}
	}

	public IQueryable<Category> GetAll(bool includecategory)
	{
		var sql =
	$"SELECT * FROM Categories ";
		using (var connection =  database.GetConnection().Result)
		{
			using (var command = new SqlCommand(sql, connection))
			{
				command.CommandType = System.Data.CommandType.Text;

				using var reader =  command.ExecuteReader();

				List<Category> list = new List<Category>();
				while(reader.Read())
				{
					var category = new Category();
					category.Id = (Guid)reader["Id"];
					category.Name = (string)reader["Name"];
					category.Description = (string)reader["Description"];
					category.CreateDate = (DateTime)reader["CreateDate"];
					list.Add(category);
				}
				return list.AsQueryable();
			}
		}
	}

	public async Task<Category> Update(Category entity)
	{
		var id = Guid.NewGuid();
		string sql = @$"update Categories 
		set Name = @name,Description = @description
		where id = @Id";
		using (var connection = await database.GetConnection())
		{
			using (var command = new SqlCommand(sql, connection))
			{
				var idParam = command.CreateParameter();
				idParam.ParameterName = "Id";
				idParam.Value = entity.Id;
				var nameParam = command.CreateParameter();
				nameParam.ParameterName = "name";
				nameParam.Value = entity.Name;
				var descriptionParam = command.CreateParameter();
				descriptionParam.ParameterName = "description";
				descriptionParam.Value = entity.Description;

				command.Parameters.Add(idParam);
				command.Parameters.Add(nameParam);
				command.Parameters.Add(descriptionParam);
				command.CommandType = System.Data.CommandType.Text;

				await command.ExecuteNonQueryAsync();
			}
		}

		return await Get(id);
	}
}
