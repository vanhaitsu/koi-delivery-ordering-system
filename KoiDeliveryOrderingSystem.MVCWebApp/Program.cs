using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Data.Repository;
using KoiDeliveryOrderingSystem.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dependencies
//builder.Services.AddScoped<FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ShipmentTrackingService>();
builder.Services.AddScoped<ShipmentTrackingRepository>();

//builder.Services.AddScoped<ShipmentOrderDetailService>();
builder.Services.AddScoped<ShipmentTrackingRepository>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
