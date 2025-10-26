using OrdersManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddKeyedTransient<IOrderService, OrderService>("transient");
builder.Services.AddKeyedScoped<IOrderService, OrderService>("scoped");
builder.Services.AddKeyedSingleton<IOrderService, OrderService>("singleton");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
