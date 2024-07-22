using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApplication1.EfCore;
using WebApplication1.Hubs;
using WebApplication1.StartComponent;

var builder = WebApplication.CreateBuilder(args);

var mssqlConnectionString = builder.Configuration.GetConnectionString("Mssql");

builder.Services.AddDbContext<PriceListDbContext>(opt => opt.UseSqlServer(mssqlConnectionString));
builder.Services.AddMediatR(m => m.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddSignalR();

//используется blazor server как альтернатива MVC

builder.Services.AddRazorComponents();

var app = builder.Build();
app.UseStaticFiles();

app.MapHub<PriceListHub>("/pricelist");
app.UseAntiforgery();
app.MapRazorComponents<App>();

app.Run();