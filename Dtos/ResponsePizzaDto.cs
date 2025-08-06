using pizzastore.Models;

namespace pizzastore.Dtos
{
    public class ResponsePizzaDto
    {
#nullable disable
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<ToppingDto> Toppings { get; set; }
    }
}