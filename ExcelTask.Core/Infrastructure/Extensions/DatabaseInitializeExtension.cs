using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ExcelTask.Core.Infrastructure.Data.Context;

namespace ExcelTask.Core.Infrastructure.Extensions
{
    public static class DatabaseInitializeExtension
    {
        public static void DatabaseInitialize(this IServiceScope serviceScope)
        {
            var services = serviceScope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DatabaseContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
