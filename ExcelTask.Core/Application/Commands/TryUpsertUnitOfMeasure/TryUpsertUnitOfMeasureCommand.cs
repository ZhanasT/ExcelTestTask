using MediatR;

namespace ExcelTask.Core.Application.Commands.TryUpsertUnitOfMeasure
{
    public class TryUpsertUnitOfMeasureCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}
