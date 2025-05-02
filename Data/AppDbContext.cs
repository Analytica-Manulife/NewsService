using FinanceNewsService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FinanceNewsService.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<NewsArticle> NewsArticles { get; set; }

        private readonly IConfiguration _configuration;
        private readonly ILogger<AppDbContext> _logger;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IConfiguration configuration,
            ILogger<AppDbContext> logger)
            : base(options)
        {
            _configuration = configuration;
            _logger = logger;

            _logger.LogInformation("AppDbContext initialized.");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _logger.LogInformation("Configuring AppDbContext using SQL Authentication...");

            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);
                });

                _logger.LogInformation("SQL Server configured with connection string from configuration.");
            }

            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            _logger.LogInformation("Saving changes to the database...");
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Saving changes to the database (async)...");
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
