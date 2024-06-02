using ExcelTask.Core.Domain.Base;

namespace ExcelTask.Core.Domain
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public int UnitOfMeasureTypeId { get; set; }
        public UnitOfMeasureType UnitOfMeasureType { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int Quantity {  get; set; }
        public List<ProductGroup> ProductGroups { get; } = [];
    }
}
