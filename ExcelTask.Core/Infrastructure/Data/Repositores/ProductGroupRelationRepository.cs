using ExcelTask.Core.Application.Persistence;
using ExcelTask.Core.Domain;
using ExcelTask.Core.Infrastructure.Data.Context;

namespace ExcelTask.Core.Infrastructure.Data.Repositores
{
    public class ProductGroupRelationRepository : GenericRepository<ProductGroupRelation>, IProductGroupRelationRepository
    {
        public ProductGroupRelationRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
