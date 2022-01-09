using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
// var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Register layers
builder.Services.AddInfrastructureLayer();
builder.Services.AddApplicationServices();


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

app.Logger.LogInformation("Ensuring database...");
var dbContext = new OrderContext();
dbContext.Database.EnsureCreated();
app.Logger.LogInformation("Database loaded...");

app.Run();