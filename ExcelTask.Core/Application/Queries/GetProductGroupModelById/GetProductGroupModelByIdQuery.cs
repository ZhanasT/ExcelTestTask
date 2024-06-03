using ExcelTask.Core.Application.Models;
using MediatR;

namespace ExcelTask.Core.Application.Queries.GetProductGroupModelById
{
    public class GetProductGroupModelByIdQuery : IRequest<ProductGroupModel?>
    {
        public int Id { get; set; }
    }
}
