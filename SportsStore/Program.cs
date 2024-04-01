using SportsStore.Data.Repositories.EntityFrameworkRepositories;
using SportsStore.Services.Services;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
/*builder.Services.AddFakeRepositories();*/
/*builder.Services.AddAdoNetRepositories();*/
builder.Services.AddEntityFrameworkRepositories();
builder.Services.AddServices();




var app = builder.Build();



app.UseStaticFiles();
app.MapControllerRoute("default", "{controller=Product}/{action=Index}");
app.Run();
