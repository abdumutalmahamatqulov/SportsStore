using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SportsStore.Domain.Entities;

public class UserRole:IdentityUserRole<Guid>
{
    public Guid Id { get; set; }
    public IdentityRole<Guid> Role { get; set; }
}
