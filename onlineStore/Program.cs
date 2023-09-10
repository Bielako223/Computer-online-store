
using Microsoft.EntityFrameworkCore;
using StoreLibrary.Data;
using StoreLibrary.DataAccess;
using StoreLibrary.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StoreDataContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<IitemData, ItemData>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(sp => ShoppingCartModel.GetCart(sp));

builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();
    
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSession();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Items}/{action=Index}/{id?}");

app.Run();
