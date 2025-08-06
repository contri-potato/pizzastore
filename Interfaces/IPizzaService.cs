using pizzastore.Models;
using pizzastore.Dtos;
namespace pizzastore.Interfaces
{
    public interface IPizzaService
    {
        Task<List<ResponsePizzaDto>> GetAllAsync();
        Task<ResponsePizzaDto?> GetByIdAsync(int id);
        Task<ResponsePizzaDto> CreateAsync(CreatePizzaDto dto);
        Task<ResponsePizzaDto?> UpdateAsync(int id, CreatePizzaDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
