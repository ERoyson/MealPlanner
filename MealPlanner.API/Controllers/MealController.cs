﻿using MealPlanner.Application.Commands;
using MealPlanner.Application.DTOs;
using MealPlanner.Application.Queries;
using MealPlanner.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MealPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MealController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}