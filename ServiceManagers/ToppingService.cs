using Microsoft.EntityFrameworkCore;
using pizzastore.Data;
using pizzastore.Dtos;
using pizzastore.Interfaces;
using pizzastore.Models;

namespace pizzastore.ServiceManagers
{
    public class ToppingService(ApplicationDbContext context) : IToppingService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Topping?> AddAsync(ToppingDto dto)
        {                                                                                                                                                           
            if (await _context.Toppings.AnyAsync(t => t.Name.Equals(dto.Name, StringComparison.CurrentCultureIgnoreCase)))
                throw new Exception(Constants.TOPPING_ALREADY_EXISTS);

            var topping = new Topping
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Toppings.Add(topping);
            await _context.SaveChangesAsync();

            return topping;
        }

        public async Task<Topping?> GetByIdAsync(int id)
        {
            return await _context.Toppings.FindAsync(id);
        }

        public async Task<List<Topping>> GetAllAsync()
        {
            return await _context.Toppings.ToListAsync();
        }

        public async Task<Topping?> UpdateAsync(int id, ToppingDto dto)
        {
            var topping = await _context.Toppings.FindAsync(id);

            if (topping == null) return null;

            if (await _context.Toppings.AnyAsync(t => t.Name.Equals(dto.Name, StringComparison.CurrentCultureIgnoreCase) && t.Id != id))
                throw new Exception(Constants.TOPPING_ALREADY_EXISTS);

            topping.Name = dto.Name;
            topping.Description = dto.Description;

            await _context.SaveChangesAsync();

            return topping;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var topping = await _context.Toppings.FindAsync(id);
            if (topping == null) return false;

            _context.Toppings.Remove(topping);
            await _context.SaveChangesAsync();
            return true;
        }

     
    }
}
