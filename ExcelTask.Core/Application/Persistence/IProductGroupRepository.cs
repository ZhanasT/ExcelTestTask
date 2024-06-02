using ExcelTask.Core.Application.Models;
using ExcelTask.Core.Domain;

namespace ExcelTask.Core.Application.Persistence
{
    public interface IProductGroupRepository : IGenericRepository<ProductGroup>
    {
        Task<int> GetLastIdAsync();
        Task<List<ProductGroupModel>> GetAllGroupsAsync();
        Task<ProductGroupModel?> GetGroupByIdAsync(int groupId);
    }
}
