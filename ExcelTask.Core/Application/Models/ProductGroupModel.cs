namespace ExcelTask.Core.Application.Models
{
    public class ProductGroupModel
    {
        public string GroupName { get; set; } = string.Empty;
        public required List<ProductInGroupModel> Products { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
