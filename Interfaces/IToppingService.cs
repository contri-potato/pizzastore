using pizzastore.Dtos;
using pizzastore.Models;

namespace pizzastore.Interfaces
{
    public interface IToppingService
    {
        Task<Topping?> AddAsync(ToppingDto dto);
        Task<Topping?> GetByIdAsync(int id);
        Task<List<Topping>> GetAllAsync();
        Task<Topping?> UpdateAsync(int id, ToppingDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
