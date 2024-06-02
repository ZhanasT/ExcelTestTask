namespace ExcelTask.Core.Application.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductRepository ProductRepository { get; }
        public IProductGroupRepository ProductGroupRepository { get; }
        public IUnitOfMeasureTypeRepository UnitOfMeasureTypeRepository { get; }
        public IProductGroupRelationRepository ProductGroupRelationRepository { get; }
        Task SaveAsync();
    }
}
