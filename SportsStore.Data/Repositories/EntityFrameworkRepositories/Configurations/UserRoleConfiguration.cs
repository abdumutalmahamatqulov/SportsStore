using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStore.Domain.Entities;

namespace SportsStore.Data.Repositories.EntityFrameworkRepositories.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        var userRole = new UserRole
        {
            Id = Guid.Parse("06affda9-706f-44c1-8e63-0de63801376d"),
            RoleId = Guid.Parse("066ffda9-706f-44c1-8e63-0de63801376d"),
            UserId = Guid.Parse("cde79a12-0364-4df7-ac73-9b9fb0a41745")
        };
        builder.HasData(userRole);
    }
}
