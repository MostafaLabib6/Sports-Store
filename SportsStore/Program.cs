using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Data;
using SportsStore.Data.Repositories;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(S => S.UseSqlServer(builder.Configuration["ConnectionStrings:SportsStoreConnection"]));

builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddServerSideBlazor();

// to save Cart session
builder.Services.AddScoped<Cart>(sp => CartService.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();




app.UseStaticFiles();
app.UseSession();
//app.MapControllerRoute(
//    name: "pagination",
//    pattern: "page{pageNumber}",
//    new { Controller = "Home", action = "Index" }
//    );

//route mapping for index with category and page
app.MapControllerRoute(
    name: "CategoryPage",
    pattern: "{category}/page{pageNumber}",
    new { Controller = "Home", action = "Index" }
    );
app.MapControllerRoute(
    name: "CategoryPage",
    pattern: "{category}",
    new { Controller = "Home", action = "Index" }
    );

app.MapDefaultControllerRoute();

//app.MapGet("/", () => "Hello World!");
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/admin/{*catchall}","/Admin/Index");
SeedDataInitializer.PopulateData(app);

app.Run();
