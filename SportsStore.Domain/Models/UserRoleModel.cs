using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Models;

public class UserRoleModel
{
    public Guid UserId { get; set; }
    public  Guid RoleId { get; set; }
    public RoleModel Role { get; set; }
    public UserRoleModel MapFromEntity(UserRole entity)
    {
        UserId = entity.UserId;
        RoleId = entity.RoleId;
        Role = entity.Role is not null ? new RoleModel().MapFromEntity(entity.Role) :
            null;
        return this;
    }
}
