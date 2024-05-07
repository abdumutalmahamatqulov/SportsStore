using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SportsStore.Domain.Models;

public class RoleModel
{
    public virtual Guid Id { get; set; }

    public virtual string? Name { get; set; }
    public RoleModel MapFromEntity(IdentityRole<Guid> role)
    {
        Id = role.Id;
        Name = role.Name;
        return this;
    }
}
