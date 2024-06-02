namespace ExcelTask.Core.Application.Models
{
    public class ProductInGroupModel
    {
        public string ProductName { get; set; } = string.Empty;
        public string UnitOfMeasureName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int QuantityInGroup { get; set; }
    }
}
