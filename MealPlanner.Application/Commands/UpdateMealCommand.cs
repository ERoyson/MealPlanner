using MealPlanner.Application.DTOs;
using MediatR;

namespace MealPlanner.Application.Commands;

public record UpdateMealCommand(MealDTO MealDTO) : IRequest<bool>
{ }
