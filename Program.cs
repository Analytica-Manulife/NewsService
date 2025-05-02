using FinanceNewsService.Data;
using FinanceNewsService.INewsService;
using FinanceNewsService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<INewsService, NewsService>();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application is starting up.");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    logger.LogInformation("Swagger is enabled in development mode.");
}

app.MapControllers();

try
{
    app.Run();
    logger.LogInformation("Application has started successfully.");
}
catch (Exception ex)
{
    logger.LogCritical(ex, "Application failed to start due to an unhandled exception.");
    throw;
}