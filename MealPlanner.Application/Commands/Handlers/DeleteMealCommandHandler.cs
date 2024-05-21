using MealPlanner.Domain.Abstractions;
using MediatR;

namespace MealPlanner.Application.Commands.Handlers;

public class DeleteMealCommandHandler : IRequestHandler<DeleteMealCommand, bool>
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteMealCommandHandler(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(DeleteMealCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.Meal.Delete(request.Id, cancellationToken);

            bool isDeleted = await _unitOfWork.Complete() > 0;

            return isDeleted;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
