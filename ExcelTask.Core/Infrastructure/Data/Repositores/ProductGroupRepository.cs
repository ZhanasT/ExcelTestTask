using ExcelTask.Core.Application.Models;
using ExcelTask.Core.Application.Persistence;
using ExcelTask.Core.Domain;
using ExcelTask.Core.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ExcelTask.Core.Infrastructure.Data.Repositores
{
    public class ProductGroupRepository : GenericRepository<ProductGroup>, IProductGroupRepository
    {
        public ProductGroupRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<int> GetLastIdAsync()
        {
            return await (from pg in _dbSet
                          orderby pg.Id descending
                          select pg.Id).FirstOrDefaultAsync();
        }

        public async Task<List<ProductGroupModel>> GetAllGroupsAsync()
        {
            var productGroups = await _dbContext.ProductGroups
                .Include(pg => pg.Products)
                    .ThenInclude(p => p.UnitOfMeasureType)
                .Select(pg => new ProductGroupModel
                {
                    GroupName = pg.GroupName,
                    Products = pg.Products.Select(p => new ProductInGroupModel
                    {
                        ProductName = p.Name,
                        UnitOfMeasureName = p.UnitOfMeasureType.Name,
                        UnitPrice = p.UnitPrice,
                        QuantityInGroup = _dbContext.ProductGroupRelations
                            .Where(pr => pr.ProductId == p.Id && pr.ProductGroupId == pg.Id)
                            .Select(pr => pr.QuantityInGroup)
                            .FirstOrDefault()
                    }).ToList(),
                    TotalPrice = pg.Products.Sum(p => p.UnitPrice * _dbContext.ProductGroupRelations
                        .Where(pr => pr.ProductId == p.Id && pr.ProductGroupId == pg.Id)
                        .Select(pr => pr.QuantityInGroup)
                        .FirstOrDefault())
                }).ToListAsync();

            return productGroups;
        }

        public async Task<ProductGroupModel?> GetGroupByIdAsync(int groupId)
        {
            var productGroups = await _dbContext.ProductGroups
                .Where(pg => pg.Id == groupId)
                .Include(pg => pg.Products)
                    .ThenInclude(p => p.UnitOfMeasureType)
                .Select(pg => new ProductGroupModel
                {
                    GroupName = pg.GroupName,
                    Products = pg.Products.Select(p => new ProductInGroupModel
                    {
                        ProductName = p.Name,
                        UnitOfMeasureName = p.UnitOfMeasureType.Name,
                        UnitPrice = p.UnitPrice,
                        QuantityInGroup = _dbContext.ProductGroupRelations
                            .Where(pr => pr.ProductId == p.Id && pr.ProductGroupId == pg.Id)
                            .Select(pr => pr.QuantityInGroup)
                            .FirstOrDefault()
                    }).ToList(),
                    TotalPrice = pg.Products.Sum(p => p.UnitPrice * _dbContext.ProductGroupRelations
                        .Where(pr => pr.ProductId == p.Id && pr.ProductGroupId == pg.Id)
                        .Select(pr => pr.QuantityInGroup)
                        .FirstOrDefault())
                }).FirstOrDefaultAsync();

            return productGroups;
        }
    }
}
