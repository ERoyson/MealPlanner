using Mapster;
using MealPlanner.Application.DTOs;
using MealPlanner.Domain.Abstractions;
using MealPlanner.Domain.Entities;
using MediatR;

namespace MealPlanner.Application.Queries.Handlers
{
    public class GetMealByIdQueryHandler : IRequestHandler<GetMealByIdQuery, MealDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetMealByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MealDTO> Handle(GetMealByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Meal meal = await _unitOfWork.Meal.GetById(request.Id, cancellationToken);

                MealDTO mealDTO = meal.Adapt<MealDTO>();

                return mealDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
