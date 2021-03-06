using EventBus.Messages;
using MassTransit;
using Ordering.API.EventBusConsumer;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Register layers
builder.Services.AddInfrastructureLayer();
builder.Services.AddApplicationServices();
builder.Services.AddAutoMapper(typeof(Program));

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config => {
    config.AddConsumer<CheckoutConsumer>();
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(Constants.CHECKOUT_QUEUE, c =>
        {
            c.ConfigureConsumer<CheckoutConsumer>(ctx);
        });
    });
});
builder.Services.AddMassTransitHostedService();

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