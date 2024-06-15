using Mapster;
using MealPlanner.Application.DTOs;
using MealPlanner.Domain.Abstractions;
using MealPlanner.Domain.Entities;
using MediatR;

namespace MealPlanner.Application.Queries.Handlers;

public class GetAllMealsQueryHandler : IRequestHandler<GetAllMealsQuery, List<MealDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetAllMealsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<MealDTO>> Handle(GetAllMealsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            List<Meal> meals = await _unitOfWork.Meal.GetAll(cancellationToken);
            List<MealDTO> mealsDTO = meals.Adapt<List<MealDTO>>();

            return mealsDTO;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
