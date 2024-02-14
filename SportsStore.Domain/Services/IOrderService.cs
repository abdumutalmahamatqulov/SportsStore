using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Models;

namespace SportsStore.Domain.Services;

public interface IOrderService
{
    Task <List<OrderModel>> GetOrdersAsync ();
	Task Create(List<Guid> IdList, string customerName);
	Task<OrderExtendedModel> GetByOrderId(Guid id);
}
