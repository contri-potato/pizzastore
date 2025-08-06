namespace pizzastore.Dtos
{
    public class CreatePizzaDto
    {
#nullable disable
        public string Name { get; set; }
        public List<int> ToppingIds { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
