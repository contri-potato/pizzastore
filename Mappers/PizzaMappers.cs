using pizzastore.Dtos;
using pizzastore.Models;

namespace pizzastore.Mappers
{
    public static class PizzaMappers
    {
        public static ResponsePizzaDto PizzaToResponseMapper(this Pizza pizza)
        {
               return new ResponsePizzaDto()
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Description = pizza.Description,
                Price = pizza.Price,
                Toppings = pizza.PizzaToppings
                .Select(pt => new ToppingDto() { Name = pt.Topping?.Name, Description = pt.Topping?.Description })
                .ToList()
            };
        }

        public static Pizza ToPizzaEntity(this CreatePizzaDto dto)
        {
            return new Pizza
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                PizzaToppings = dto.ToppingIds
                    .Distinct()
                    .Select(tid => new PizzaTopping
                    {
                        ToppingId = tid
                    })
                    .ToList()
            };
        }
    }
}
