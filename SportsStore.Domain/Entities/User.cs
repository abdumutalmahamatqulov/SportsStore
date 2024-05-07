using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SportsStore.Domain.Entities;

public class User: IdentityUser<Guid>
{
    public List<UserRole> Roles { get; set; }

}
