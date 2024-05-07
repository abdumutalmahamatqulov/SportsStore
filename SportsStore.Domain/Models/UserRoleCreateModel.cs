using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Models;

public class UserRoleCreateModel
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

}
