using Catalog.API.Persistence;
using Catalog.Grpc.Services;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddInfrastructureLayer();
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CatalogService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Logger.LogInformation("Ensuring database...");
var dbContext = new CatalogContext();
dbContext.Database.EnsureCreated();
var logger = app.Services.GetService<ILogger<CatalogContextSeed>>();
CatalogContextSeed
    .SeedAsync(dbContext, logger)
    .Wait();

app.Logger.LogInformation("Database loaded...");

app.Run();
