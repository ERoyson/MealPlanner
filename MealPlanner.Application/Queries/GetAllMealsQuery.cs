using MealPlanner.Application.DTOs;
using MediatR;

namespace MealPlanner.Application.Queries;

public record GetAllMealsQuery : IRequest<List<MealDTO>>
{ }
