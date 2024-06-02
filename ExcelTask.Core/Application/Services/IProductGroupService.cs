using ExcelTask.Core.Application.Models;

namespace ExcelTask.Core.Application.Services
{
    public interface IProductGroupService
    {
        Task<int> CreateGroupProducts();
        Task<List<ProductGroupModel>> GetProductGroupModels();
        Task<ProductGroupModel?> GetProductGroupModelById(int id);
    }
}
