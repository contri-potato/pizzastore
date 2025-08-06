using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzastore.Models
{
    [Table("Toppings")]
    public class Topping
    {
#nullable disable

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<PizzaTopping> PizzaToppings { get; set; } = null;
    }
}
