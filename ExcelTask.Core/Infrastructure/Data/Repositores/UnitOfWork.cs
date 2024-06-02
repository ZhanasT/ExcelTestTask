using ExcelTask.Core.Application.Persistence;
using ExcelTask.Core.Infrastructure.Data.Context;

namespace ExcelTask.Core.Infrastructure.Data.Repositores
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        public UnitOfWork(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private ProductRepository _productRepository;
        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);

        private ProductGroupRepository _productGroupRepository;
        public IProductGroupRepository ProductGroupRepository => _productGroupRepository ??= new ProductGroupRepository(_context);

        private UnitOfMeasureTypeRepository _unitOfMeasureRepository;
        public IUnitOfMeasureTypeRepository UnitOfMeasureTypeRepository => _unitOfMeasureRepository ??= new UnitOfMeasureTypeRepository(_context);

        private ProductGroupRelationRepository _productGroupRelationRepository;
        public IProductGroupRelationRepository ProductGroupRelationRepository => _productGroupRelationRepository ??= new ProductGroupRelationRepository(_context);

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose() { _context.Dispose(); GC.SuppressFinalize(this); }

    }
}
