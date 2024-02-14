using SportsStore.Domain.Entities;

namespace SportsStore.Data.Repositories.FakeRepositories
{
	public class DataStore
	{
		public static List<Category> Categories { get; set; } = new List<Category>()
		{
			new Category{Id =  Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 
				Name = "Football",Description = "Footbal Appartment",CreateDate = DateTime.Now},

			new Category{Id =  Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaabaaaa"),
				Name = "Tenis",Description = "Tenis Appartment",CreateDate = DateTime.Now}
		};
		public static List<Product> Products { get; set; } = new List<Product>()
		{
			new Product{Id = Guid.Parse("ffffffff-ffff-ffff-ffff-aaaaaaaaffff"),Name = "To'p",Description = "Koptok",Price = 1245.45m,
				CategoryId =  Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),CreateDate = DateTime.Now,Discount = 10},

			new Product{Id = Guid.Parse("ffffffff-ffff-ffff-ffff-aaaaaaaafbff"),Name = "Futbolka",Description = "Futbol Kiyimi",Price = 45889.45m,
				CategoryId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaabaaaa"),CreateDate = DateTime.Now,Discount = 0}
		};
		public static List<Order> Orders { get; set; } = new List<Order>()
		{
			new Order{Id = Guid.Parse("bbbbbbbb-dddd-dddd-dddd-abdabdabdabd"), Status = OrderStatus.Created,CustomerName = "Shomilbek",CreateDate = DateTime.Now},
			new Order{Id = Guid.Parse("aabbbbbb-dddd-dddd-dddd-abdabdabdabd"), Status = OrderStatus.InProgress,CustomerName = "Abduvali",CreateDate = DateTime.Now},
		};
		public static List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>()
		{
			new OrderDetail{Id = Guid.Parse("bbaaaaaa-eeee-eeee-eeee-fefefefefefe"),
				OrderId = Guid.Parse("bbbbbbbb-dddd-dddd-dddd-abdabdabdabd"),
				ProductId = Guid.Parse("ffffffff-ffff-ffff-ffff-aaaaaaaaffff"),Count = 100,CreateDate = DateTime.Now},

			new OrderDetail{Id = Guid.Parse("eeaaeeaa-eeee-eeee-eeee-fefefefefefe"),
				OrderId = Guid.Parse("aabbbbbb-dddd-dddd-dddd-abdabdabdabd"),
				ProductId = Guid.Parse("ffffffff-ffff-ffff-ffff-aaaaaaaafbff"),Count = 500,CreateDate = DateTime.Now}
		};
			
	}
}
