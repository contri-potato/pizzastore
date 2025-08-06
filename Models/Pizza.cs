using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzastore.Models
{
    [Table("Pizzas")]
    public class Pizza
    {
#nullable disable
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<PizzaTopping> PizzaToppings { get; set; }

    }
}
