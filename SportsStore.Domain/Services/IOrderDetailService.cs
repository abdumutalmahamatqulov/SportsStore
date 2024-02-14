using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Models;

namespace SportsStore.Domain.Services;

public  interface IOrderDetailService
{
	Task<List<OrderDetailModel>>GetOrderDetailsAsync();
}
