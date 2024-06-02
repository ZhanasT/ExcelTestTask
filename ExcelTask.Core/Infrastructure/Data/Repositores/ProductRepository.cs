using ExcelTask.Core.Application.Persistence;
using ExcelTask.Core.Domain;
using ExcelTask.Core.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ExcelTask.Core.Infrastructure.Data.Repositores
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
