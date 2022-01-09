using Catalog.API.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CatalogContext>();

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
var dbContext = new CatalogContext();
dbContext.Database.EnsureCreated();
var logger = app.Services.GetService<ILogger<CatalogContextSeed>>();
CatalogContextSeed
    .SeedAsync(dbContext, logger)
    .Wait();

app.Logger.LogInformation("Database loaded...");

app.Run();
