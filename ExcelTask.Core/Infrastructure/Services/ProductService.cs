using ExcelTask.Core.Application.Commands.CreateProducts;
using ExcelTask.Core.Application.Commands.TryUpsertUnitOfMeasure;
using ExcelTask.Core.Application.Services;
using ExcelTask.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace ExcelTask.Core.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly ISender _mediator;

        public ProductService(ISender mediator)
        {
            _mediator = mediator;
        }

        public async Task CreateProductsFromXlsx(IFormFile XlsxFile)
        {
            var products = new List<Product>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await XlsxFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        string name = worksheet.Cells[row, 1].Value.ToString() == null
                            ? throw new InvalidDataException($"Error while parsing data from file: row {row} column 1")
                            : worksheet.Cells[row, 1].Value.ToString()!;

                        string unitOfMeasureName = worksheet.Cells[row, 2].Value.ToString() == null
                            ? throw new InvalidDataException($"Error while parsing data from file: row {row} column 2")
                            : worksheet.Cells[row, 2].Value.ToString()!;

                        int unitOfMeasureTypeId = 0;

                        var newUnitOfMeasureCommand = new TryUpsertUnitOfMeasureCommand
                        {
                            Name = unitOfMeasureName
                        };

                        unitOfMeasureTypeId = await _mediator.Send(newUnitOfMeasureCommand);

                        decimal unitPrice = Convert.ToDecimal(worksheet.Cells[row, 3].Value);

                        int quantity = Convert.ToInt32(worksheet.Cells[row, 4].Value);

                        var product = new Product
                        {
                            Name = name,
                            UnitOfMeasureTypeId = unitOfMeasureTypeId,
                            UnitPrice = unitPrice,
                            Quantity = quantity
                        };

                        products.Add(product);
                    }
                }
            }
            await _mediator.Send(new CreateProductsCommand
            {
                Products = products,
            });
        }
    }
}
