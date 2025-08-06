using Microsoft.AspNetCore.Mvc;
using pizzastore.Dtos;
using pizzastore.Interfaces;
using pizzastore.Models;
using pizzastore.ServiceManagers;

namespace pizzastore.Controllers
{
    [Route("api/pizza")]
    [ApiController]
    public class PizzaController(IPizzaService pizzaService) : ControllerBase
    {
        private readonly IPizzaService _pizzaService = pizzaService;

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreatePizzaDto createPizzaDto)
        {
            try
            {
                var responsePizza = await _pizzaService.CreateAsync(createPizzaDto);

                return CreatedAtAction(nameof(GetById), new { id = responsePizza.Id }, responsePizza);

            }
            catch (Exception ex)
            {

                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ResponsePizzaDto>>> GetAll()
        {
            var pizzas = await _pizzaService.GetAllAsync();
            return Ok(pizzas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponsePizzaDto>> GetById([FromRoute]int id)
        {
            var pizza = await _pizzaService.GetByIdAsync(id);
            return (pizza == null) ? NotFound(Constants.PIZZA_NOT_FOUND) : Ok(pizza);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CreatePizzaDto dto)
        {
            try
            {
                var updated = await _pizzaService.UpdateAsync(id, dto);

                return updated == null ? NotFound(Constants.PIZZA_NOT_FOUND) : Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var deleted = await _pizzaService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound(Constants.PIZZA_NOT_FOUND);
        }
    }
}
