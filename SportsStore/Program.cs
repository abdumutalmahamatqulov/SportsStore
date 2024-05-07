using Microsoft.AspNetCore.Identity;
using SportsStore.Data.Repositories.EntityFrameworkRepositories;
using SportsStore.Domain.Entities;
using SportsStore.Services.Services;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
/*builder.Services.AddFakeRepositories();*/
/*builder.Services.AddAdoNetRepositories();*/
builder.Services.AddEntityFrameworkRepositories();
builder.Services.AddServices();
builder.Services.AddIdentity<User, IdentityRole<Guid>>(opt =>
{
    opt.Password.RequiredLength = 7;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;

    opt.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication();
builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/Auth/Login";
});

builder.Services.AddAuthorization();


var app = builder.Build();
var hash = app.Services.CreateScope().ServiceProvider.GetRequiredService<UserManager<User>>().PasswordHasher.HashPassword(new User
{
    Id = Guid.Parse("cde79a12-0364-4df7-ac73-9b9fb0a41745"),
    UserName = "Oybek",
    NormalizedUserName = "Oybek".ToUpper(),
    Email = "oybek@gmail.com",
    NormalizedEmail = "oybek@gmail.com".ToUpper(),
    EmailConfirmed = true
}, "Web123$");
Console.WriteLine(hash);
app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();
app.MapControllerRoute("default", "{controller=Product}/{action=Index}");
app.Run();
