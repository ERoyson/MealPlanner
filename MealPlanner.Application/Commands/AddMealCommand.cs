using MealPlanner.Application.DTOs;
using MediatR;

namespace MealPlanner.Application.Commands
{
    public record AddMealCommand(MealDTO meal) : IRequest<Guid>
    { }
}
