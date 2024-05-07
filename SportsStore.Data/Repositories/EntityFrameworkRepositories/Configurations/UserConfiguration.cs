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

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    readonly UserManager<User> _userManager;

    public UserConfiguration()
    {
        //_userManager = userManager;
    }

    public void Configure(EntityTypeBuilder<User> builder)
    {
        var user = new User { 
            Id = Guid.Parse("cde79a12-0364-4df7-ac73-9b9fb0a41745"),
            UserName = "Oybek",
            NormalizedUserName = "Oybek".ToUpper(),
            Email = "oybek@gmail.com",
            NormalizedEmail = "oybek@gmail.com".ToUpper(),
            EmailConfirmed = true
        };
        //user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, "Ay#@ere123");
        builder.HasData(user);
    }
}
