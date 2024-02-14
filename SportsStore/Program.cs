using SportsStore.Data.Repositories.AdoNetRepositories;
using SportsStore.Data.Repositories.FakeRepositories;
using SportsStore.Services.Services;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
/*builder.Services.AddFakeRepositories();*/
builder.Services.AddAdoNetRepositories();
builder.Services.AddServices();




var app = builder.Build();



app.UseStaticFiles();
app.MapControllerRoute("default", "{controller=Product}/{action=Index}");
app.Run();
