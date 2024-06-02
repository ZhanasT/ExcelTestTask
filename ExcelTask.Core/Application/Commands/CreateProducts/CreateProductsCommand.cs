using ExcelTask.Core.Domain;
using MediatR;

namespace ExcelTask.Core.Application.Commands.CreateProducts
{
    public class CreateProductsCommand : IRequest
    {
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
