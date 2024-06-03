using ExcelTask.Core.Application.Models;
using MediatR;

namespace ExcelTask.Core.Application.Queries.GetAllProductGroupModels
{
    public class GetAllProductGroupModelQuery : IRequest<List<ProductGroupModel>>
    {
    }
}
