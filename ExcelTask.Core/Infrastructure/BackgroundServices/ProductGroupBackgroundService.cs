using ExcelTask.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ExcelTask.Core.Infrastructure.BackgroundServices
{
    public class ProductGroupBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProductGroupBackgroundService> _logger;

        public ProductGroupBackgroundService(IServiceProvider serviceProvider, ILogger<ProductGroupBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Grouping product background service running.");

            await DoWork();

            using PeriodicTimer timer = new(TimeSpan.FromMinutes(5));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await DoWork();
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Grouping product background service is stopping.");
            }
        }

        private async Task DoWork()
        {
            _logger.LogInformation("Grouping product background service is working.");

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var productGroupService = scope.ServiceProvider.GetRequiredService<IProductGroupService>();
                await productGroupService.CreateGroupProducts();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while grouping products: {ex}");
            }
        }
    }
}
