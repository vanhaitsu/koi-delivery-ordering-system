using KoiDeliveryOrderingSystem.Data.Repository;
using KoiDeliveryOrderingSystem.Service;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ShipmentTrackingService>();
builder.Services.AddScoped<ShipmentTrackingRepository>();
builder.Services.AddScoped<ShipperService>();
builder.Services.AddScoped<ShipperRepository>();
builder.Services.AddScoped<HealthCheckService>();
builder.Services.AddScoped<HealCheckRepository>();
builder.Services.AddScoped<PackagingProcessRepository>();
builder.Services.AddScoped<PackagingProcessService>();
builder.Services.AddScoped<PricingPolicyRepository>();
builder.Services.AddScoped<PricingPolicyService>();
builder.Services.AddScoped<ShipmentOrderDetailRepository>();
builder.Services.AddScoped<ShipmentOrderDetailService>();
builder.Services.AddScoped<ShipmentOrderService>();
builder.Services.AddScoped<ShipmentOrderRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AnimalTypeService>();
builder.Services.AddScoped<AnimalTypeRepository>();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("https://localhost:7005")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
