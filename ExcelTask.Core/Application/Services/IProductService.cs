using Microsoft.AspNetCore.Http;

namespace ExcelTask.Core.Application.Services
{
    public interface IProductService
    {
        public Task CreateProductsFromXlsx(IFormFile XlsxFile);
    }
}
