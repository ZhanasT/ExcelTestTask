using ExcelTask.Core.Application.Models;
using ExcelTask.Core.Application.Persistence;
using MediatR;

namespace ExcelTask.Core.Application.Queries.GetProductGroupModelById
{
    public class GetProductGroupModelByIdQueryHandler : IRequestHandler<GetProductGroupModelByIdQuery, ProductGroupModel?>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetProductGroupModelByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ProductGroupModel?> Handle(GetProductGroupModelByIdQuery getProductGroupModelByIdQuery, CancellationToken cancellationToken)
        {
            try
            {
                return await _unitOfWork.ProductGroupRepository.GetGroupByIdAsync(getProductGroupModelByIdQuery.Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while getting product groups: {ex.Message}");
            }
        }
    }
}
