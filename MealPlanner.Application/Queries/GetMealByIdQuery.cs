using MealPlanner.Application.DTOs;
using MediatR;

namespace MealPlanner.Application.Queries
{
    public record GetMealByIdQuery(Guid Id) : IRequest<MealDTO>
    { }
}
