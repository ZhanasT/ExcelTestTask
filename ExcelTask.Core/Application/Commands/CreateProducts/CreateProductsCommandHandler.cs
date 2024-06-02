using ExcelTask.Core.Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExcelTask.Core.Application.Commands.CreateProducts
{
    public class CreateProductsCommandHandler : IRequestHandler<CreateProductsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(CreateProductsCommand createProductsCommand, CancellationToken cancellationToken)
        {
            try
            {
                var products = createProductsCommand.Products;
                await _unitOfWork.ProductRepository.AddRangeAsync(products);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Ошибка при сохранении данных: {ex.Message}");
            }

        }
    }
}
