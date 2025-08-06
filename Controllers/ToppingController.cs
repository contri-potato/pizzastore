using Microsoft.AspNetCore.Mvc;
using pizzastore.Data;
using pizzastore.Dtos;
using pizzastore.Interfaces;
using pizzastore.Models;
using System;

namespace pizzastore.Controllers
{
    [Route("api/topping")]
    [ApiController]
    public class ToppingController(IToppingService toppingService) : ControllerBase
    {
        private readonly IToppingService _toppingService = toppingService;

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ToppingDto dto)
        {
            try
            {
                var topping = await _toppingService.AddAsync(dto);

                return CreatedAtAction(nameof(GetAll), new { id = topping!.Id }, topping);
            }
            catch (Exception ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Topping>> GetById(int id)
        {
            var topping = await _toppingService.GetByIdAsync(id);

            return topping == null ? NotFound(Constants.TOPPING_NOT_FOUND) : Ok(topping);
        }


        [HttpGet]
        public async Task<ActionResult<List<Topping>>> GetAll()
        {
            return Ok(await _toppingService.GetAllAsync());
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] ToppingDto dto)
        {
            try
            {
                var updated = await _toppingService.UpdateAsync(id, dto);

                return updated == null ? NotFound(Constants.TOPPING_NOT_FOUND) : Ok(updated);
            }
            catch (Exception ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var deleted = await _toppingService.DeleteAsync(id);

            return deleted ? NoContent() : NotFound(Constants.TOPPING_NOT_FOUND);
        }

    }
}
