using MediatR;

namespace MealPlanner.Application.Commands;

public record DeleteMealCommand(Guid Id) : IRequest<bool>
{ }
