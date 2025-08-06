using Microsoft.EntityFrameworkCore;
using pizzastore.Data;
using pizzastore.Dtos;
using pizzastore.Interfaces;
using pizzastore.Mappers;
using pizzastore.Models;

namespace pizzastore.ServiceManagers
{
    public class PizzaService(ApplicationDbContext context) : IPizzaService
    {
        public readonly ApplicationDbContext _context = context;

        public async Task<ResponsePizzaDto> CreateAsync(CreatePizzaDto dto)
        {

            if (HasDuplicateToppings(dto.ToppingIds))
                throw new Exception(Constants.PIZZA_TOPPING_DUPLICATE);

            if (await HasInvalidToppings(dto.ToppingIds) )
                throw new Exception(Constants.TOPPING_NOT_FOUND);

            if (await _context.Pizzas.AnyAsync(p => p.Name.Equals(dto.Name, StringComparison.CurrentCultureIgnoreCase)))
                throw new Exception(Constants.PIZZA_ALREADY_EXISTS);

            var pizza = dto.ToPizzaEntity();

            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();


            return pizza.PizzaToResponseMapper();
                                                                        
        }

        public async Task<List<ResponsePizzaDto>> GetAllAsync()
        {
           var pizzas =  await _context.Pizzas
                .Include(p => p.PizzaToppings)
                .ThenInclude(pt => pt.Topping)
                .ToListAsync();

            return pizzas.Select(p => p.PizzaToResponseMapper()).ToList();
        }

        public async Task<ResponsePizzaDto?> GetByIdAsync(int id)
        {
            var pizza = await _context.Pizzas
                .Include(p => p.PizzaToppings)
                .ThenInclude(pt => pt.Topping)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (pizza == null) return null;

            return pizza.PizzaToResponseMapper();
          
        }

        public async Task<ResponsePizzaDto?> UpdateAsync(int id, CreatePizzaDto dto)
        {
            var pizza = await _context.Pizzas.FindAsync(id);

            if (pizza == null) return null;

            if (HasDuplicateToppings(dto.ToppingIds))
                throw new Exception(Constants.PIZZA_TOPPING_DUPLICATE);

            if (await HasInvalidToppings(dto.ToppingIds))
                throw new Exception(Constants.TOPPING_NOT_FOUND);

            if (await _context.Pizzas.AnyAsync(t => t.Name.Equals(dto.Name, StringComparison.CurrentCultureIgnoreCase) && t.Id != id))
                throw new Exception(Constants.PIZZA_ALREADY_EXISTS);

            var existingToppings =  _context.PizzaToppings.Where(pt => pt.PizzaId == id);
            _context.PizzaToppings.RemoveRange(existingToppings);


            pizza.Name = dto.Name;
            pizza.Description = dto.Description;
            pizza.Price = dto.Price;

            pizza.PizzaToppings = dto.ToppingIds
                .Distinct()
                .Select(tid => new PizzaTopping { ToppingId = tid, PizzaId = pizza.Id })
                .ToList();



            await _context.SaveChangesAsync();

            var responsePizza = await _context.Pizzas
                .Include(p => p.PizzaToppings)
                .ThenInclude(pt => pt.Topping)
                .FirstOrDefaultAsync(p => p.Id == id);

            return responsePizza?.PizzaToResponseMapper();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null) return false;

            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();
            return true;
        }


        #region Private
        private static bool HasDuplicateToppings(List<int> toppingIds) 
        {
            var distinct = toppingIds.Distinct().ToList();
            return distinct.Count != toppingIds.Count;
        }

        private async Task<bool> HasInvalidToppings(List<int> toppingIds)
        {
            var toppings = await _context.Toppings
                .Where(t => toppingIds.Contains(t.Id))
                .ToListAsync();

            return toppings.Count != toppingIds.Count;
        }


        #endregion
    }
}
