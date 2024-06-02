using ExcelTask.Core.Domain.Base;

namespace ExcelTask.Core.Domain
{
    public class UnitOfMeasureType : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}
