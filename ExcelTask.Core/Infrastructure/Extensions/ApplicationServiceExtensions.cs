using ExcelTask.Core.Application.Commands.CreateProducts;
using ExcelTask.Core.Application.Commands.TryUpsertUnitOfMeasure;
using ExcelTask.Core.Application.Persistence;
using ExcelTask.Core.Application.Services;
using ExcelTask.Core.Infrastructure.BackgroundServices;
using ExcelTask.Core.Infrastructure.Data.Repositores;
using ExcelTask.Core.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ExcelTask.Core.Infrastructure.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
            services.AddScoped<IUnitOfMeasureTypeRepository,  UnitOfMeasureTypeRepository>();
            services.AddScoped<IProductGroupRelationRepository, ProductGroupRelationRepository>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(CreateProductsCommandHandler))!));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(TryUpsertUnitOfMeasureCommandHandler))!));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductGroupService, ProductGroupService>();

            services.AddHostedService<ProductGroupBackgroundService>();
            return services;
        }
    }
}
