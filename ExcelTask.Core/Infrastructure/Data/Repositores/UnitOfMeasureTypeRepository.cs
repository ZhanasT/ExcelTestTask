using ExcelTask.Core.Application.Persistence;
using ExcelTask.Core.Domain;
using ExcelTask.Core.Infrastructure.Data.Context;

namespace ExcelTask.Core.Infrastructure.Data.Repositores
{
    public class UnitOfMeasureTypeRepository : GenericRepository<UnitOfMeasureType>, IUnitOfMeasureTypeRepository
    {
        public UnitOfMeasureTypeRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
