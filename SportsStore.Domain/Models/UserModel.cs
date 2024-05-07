using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<UserRoleModel> Roles { get; set; }
    public UserModel MapFromEntity( User entity)
    {
        Id = entity.Id;
        Name = entity.UserName;
        Email = entity.Email;
        Roles = entity.Roles is not null && entity.Roles.Any()?
            entity.Roles.Select(x=> new UserRoleModel().MapFromEntity(x)).ToList() :
            new List<UserRoleModel>();
        return this;
    }
}
