using ExcelTask.Core.Application.Commands.CreateProducts;
using ExcelTask.Core.Application.Persistence;
using ExcelTask.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExcelTask.Core.Application.Commands.TryUpsertUnitOfMeasure
{
    public class TryUpsertUnitOfMeasureCommandHandler : IRequestHandler<TryUpsertUnitOfMeasureCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public TryUpsertUnitOfMeasureCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(TryUpsertUnitOfMeasureCommand createUnitOfMeasureCommand, CancellationToken cancellationToken)
        {
            try
            {
                var existedUnitOfMeasureType = await _unitOfWork.UnitOfMeasureTypeRepository.Get(e => e.Name == createUnitOfMeasureCommand.Name);
                if (existedUnitOfMeasureType.Count > 0)
                {
                    return existedUnitOfMeasureType.FirstOrDefault()!.Id;
                }

                var newUnitOfMeasure = new UnitOfMeasureType
                {
                    Name = createUnitOfMeasureCommand.Name,
                };
                var createdUnitOfMeasure = await _unitOfWork.UnitOfMeasureTypeRepository.Create(newUnitOfMeasure);
                await _unitOfWork.SaveAsync();
                return createdUnitOfMeasure.Id;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Ошибка при сохранении данных: {ex.Message}");
            }

        }
    }
}
