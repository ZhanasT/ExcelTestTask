using ExcelTask.Core.Application.Models;
using ExcelTask.Core.Application.Persistence;
using ExcelTask.Core.Application.Queries.GetAllProductGroupModels;
using ExcelTask.Core.Application.Queries.GetProductGroupModelById;
using ExcelTask.Core.Application.Services;
using ExcelTask.Core.Domain;
using MediatR;

namespace ExcelTask.Core.Infrastructure.Services
{
    public class ProductGroupService : IProductGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISender _mediator;
        private const decimal MaxGroupPrice = 200m;

        public ProductGroupService(IUnitOfWork unitOfWork, ISender mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<int> CreateGroupProducts()
        {
            var unprocessedProducts = await GetUnprocessedProducts();
            var newProductGroups = new List<ProductGroup>();
            var newProductGroupRelations = new List<ProductGroupRelation>();
            decimal currentGroupPrice = 0;
            int newProductGroupIndex = await _unitOfWork.ProductGroupRepository.GetLastIdAsync() + 1;
            var currentGroup = new ProductGroup
            {
                Id = newProductGroupIndex,
                GroupName = $"Group {newProductGroupIndex}"
            };

            if (unprocessedProducts.Count == 0)
            {
                return 0;
            }

            foreach (var unprocessedProduct in unprocessedProducts)
            {
                while (unprocessedProduct.Quantity > 0)
                {
                    int quantityToAdd = Math.Min(unprocessedProduct.Quantity, (int)((MaxGroupPrice - currentGroupPrice) / unprocessedProduct.UnitPrice));

                    if (quantityToAdd <= 0)
                    {
                        newProductGroups.Add(currentGroup);
                        currentGroupPrice = 0;
                        newProductGroupIndex++;
                        currentGroup = new ProductGroup
                        {
                            Id = newProductGroupIndex,
                            GroupName = $"Group {newProductGroupIndex}"
                        };
                        continue;
                    }

                    var newProductGroupRelation = new ProductGroupRelation
                    {
                        ProductId = unprocessedProduct.Id,
                        ProductGroupId = currentGroup.Id,
                        QuantityInGroup = quantityToAdd
                    };
                    newProductGroupRelations.Add(newProductGroupRelation);

                    currentGroupPrice += quantityToAdd * unprocessedProduct.UnitPrice;
                    unprocessedProduct.Quantity -= quantityToAdd;
                }
            }

            if (currentGroupPrice > 0)
            {
                newProductGroups.Add(currentGroup);
            }

            await _unitOfWork.ProductGroupRepository.AddRangeAsync(newProductGroups);
            await _unitOfWork.ProductGroupRelationRepository.AddRangeAsync(newProductGroupRelations);
            await _unitOfWork.SaveAsync();

            return newProductGroups.Count;
        }

        public async Task<List<ProductGroupModel>> GetProductGroupModels()
        {
            try
            {
                return await _mediator.Send(new GetAllProductGroupModelQuery());
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error while getting all product groups: {ex}");
            }
        }

        public async Task<ProductGroupModel?> GetProductGroupModelById(int id)
        {
            try
            {
                return await _mediator.Send(new GetProductGroupModelByIdQuery { Id = id });
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error while getting all product groups: {ex}");
            }
        }

        private async Task<List<Product>> GetUnprocessedProducts()
        {
            var existedProductGroupRelations = await _unitOfWork.ProductGroupRelationRepository.Get();

            if (existedProductGroupRelations.Count == 0)
            {
                return await _unitOfWork.ProductRepository.Get();
            }
            else
            {
                var productIdsFromRelations = existedProductGroupRelations.Select(x => x.ProductId).ToList();

                var products = await _unitOfWork.ProductRepository.Get(e => productIdsFromRelations.Contains(e.Id));

                foreach (var product in products)
                {
                    int quantityInRelation = existedProductGroupRelations
                        .Where(pifr => pifr.ProductId == product.Id)
                        .Sum(pifr => pifr.QuantityInGroup);

                    product.Quantity -= quantityInRelation;
                }

                return products;
            }
        }
    }
}
