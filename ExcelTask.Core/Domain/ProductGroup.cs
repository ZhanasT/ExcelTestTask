using ExcelTask.Core.Domain.Base;

namespace ExcelTask.Core.Domain
{
    public class ProductGroup : BaseEntity<int>
    {
        public string GroupName { get; set; } = string.Empty;
        public List<Product> Products { get; } = [];
    }
}
