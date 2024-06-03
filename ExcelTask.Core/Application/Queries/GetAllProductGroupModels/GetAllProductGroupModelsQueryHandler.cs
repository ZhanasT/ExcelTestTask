using ExcelTask.Core.Application.Models;
using ExcelTask.Core.Application.Persistence;
using MediatR;

namespace ExcelTask.Core.Application.Queries.GetAllProductGroupModels
{
    public class GetAllProductGroupModelQueryHandler : IRequestHandler<GetAllProductGroupModelQuery, List<ProductGroupModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllProductGroupModelQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<ProductGroupModel>> Handle(GetAllProductGroupModelQuery getAllProductGroupModelsQuery, CancellationToken cancellationToken)
        {
            try
            {
                return await _unitOfWork.ProductGroupRepository.GetAllGroupsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while getting product groups: {ex.Message}");
            }
        }
    }
}
