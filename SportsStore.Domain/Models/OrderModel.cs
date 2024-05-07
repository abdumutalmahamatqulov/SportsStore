using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Models;

public  class OrderModel
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
    public User UserName { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreateDate { get; set; }
    public virtual OrderModel MapFromEntity(Order order)
    {
        Id = order.Id;
        Status = order.Status;
        UserName = order.UserName;
        UserId = order.UserId;
        CreateDate = order.CreateDate;

        return this;
    }
}
