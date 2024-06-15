using MealPlanner.Application.Commands;
using MealPlanner.Application.DTOs;
using MealPlanner.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MealPlanner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MealController : ControllerBase
{
    private readonly IMediator _mediator;
    public MealController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getallmeals")]
    public async Task<ActionResult<List<MealDTO>>> GetAllMeals()
    {
        try
        {
            List<MealDTO> meals = await _mediator.Send(new GetAllMealsQuery());
            return Ok(meals);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Add(MealDTO mealDTO)
    {
        try
        {
            Guid id = await _mediator.Send(new AddMealCommand(mealDTO));
            return Ok(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("id")]
    public async Task<ActionResult<MealDTO>> GetMealById(Guid id)
    {
        try
        {
            MealDTO mealDTO = await _mediator.Send(new GetMealByIdQuery(id));

            return Ok(mealDTO);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<bool>> UpdateMeal([FromBody]MealDTO mealDTO)
    {
        try
        {
            bool isUpdated = await _mediator.Send(new UpdateMealCommand(mealDTO));

            return Ok(isUpdated);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("id")]
    public async Task<ActionResult<bool>> DeleteMeal(Guid id)
    {
        try
        {
            bool isDeleted = await _mediator.Send(new DeleteMealCommand(id));

            return Ok(isDeleted);
        }
        catch (Exception ex)
        {

            return BadRequest(ex);
        }
    }
}
