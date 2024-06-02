using ExcelTask.Core.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExcelTask.Core.Infrastructure.Extensions
{
    public static class DatabaseExtension
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection("DatabaseSettings");

            string connectionString = configSection["ConnectionString"] == null
                ? throw new InvalidOperationException("Invalid Database connection string")
                : configSection["ConnectionString"]!;

            services.AddDbContext<DatabaseContext>(opt => opt.UseNpgsql(connectionString), ServiceLifetime.Transient);
        }
    }
}
