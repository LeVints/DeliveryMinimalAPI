using DeliveryMinimalAPI.Models;
using DeliveryMinimalAPI.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add in-memory data
builder.Services.AddSingleton<List<Customer>>(provider =>
{
    return new List<Customer>
    {
        new Customer { Id = 1, Name = "Vince", Address = "Maastricht", Active = true }
    };
});

builder.Services.AddSingleton<List<Order>>(provider =>
{
    return new List<Order>
    {
        new Order 
        { 
            Id = 1, 
            OrderDate = DateTime.Parse("2025-06-30T00:34:45.9339994Z"), 
            CustomerId = 1 
        }
    };
});

builder.Services.AddSingleton<List<DeliveryState>>(provider =>
{
    return new List<DeliveryState>
    {
        new DeliveryState
        {
            Id = 1,
            State = DeliveryStateEnum.Processing,
            DateTime = DateTime.Parse("2025-06-30T00:35:34.1523066Z"),
            OrderId = 1,
            DeliveryServiceId = 0
        }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
