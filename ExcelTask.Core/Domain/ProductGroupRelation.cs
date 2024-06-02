using ExcelTask.Core.Domain.Base;

namespace ExcelTask.Core.Domain
{
    public class ProductGroupRelation : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public int ProductGroupId { get; set; }
        public int QuantityInGroup { get; set; }
    }
}
