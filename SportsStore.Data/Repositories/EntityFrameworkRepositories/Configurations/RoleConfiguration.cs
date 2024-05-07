using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SportsStore.Data.Repositories.EntityFrameworkRepositories.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
{
    readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public RoleConfiguration()
    {
        //_roleManager = roleManager;
    }

    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
    {
        var roles = new List<IdentityRole<Guid>>()
        {
            new IdentityRole<Guid>{
                Id = Guid.Parse("a2764599-ece5-4f15-b221-a5a77e87eb76"),
                Name = "Admin", NormalizedName = "Admin".ToUpper()
            },
            new IdentityRole<Guid>{
                Id = Guid.Parse("066ffda9-706f-44c1-8e63-0de63801376d"),
                Name = "SuperAdmin", NormalizedName = "SuperAdmin".ToUpper()
            }

        };
        builder.HasData(roles);
    }
}
