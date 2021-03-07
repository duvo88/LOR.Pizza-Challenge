namespace LOR.Pizzeria.Models
{
    public class PizzaSize
    {
        public string Description { get; set; }
        public decimal Price { get; set; }

        public static PizzaSize Medium = new PizzaSize { Description = "Medium", Price = 0.00m };
        public static PizzaSize Large = new PizzaSize { Description = "Large", Price = 2.50m };
        public static PizzaSize Jumbo = new PizzaSize { Description = "Jumbo", Price = 3.50m };

    }

    public enum Size
    {
        Medium = 1,
        Large = 2,
        Jumbo = 3
    }
}
