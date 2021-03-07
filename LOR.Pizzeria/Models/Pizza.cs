namespace LOR.Pizzeria.Models
{
	public class Pizza
	{
		public Pizza(IPizzaStore pizzaStore)
		{
			PizzaStore = pizzaStore;
		}

		public string Description { get; set; }//description that includes all details of a pizza
		public decimal Price { get; set; }//final price to be charged/printed
		public PizzaRecipe PizzaRecipe { get; set; }
		public PizzaSize PizzaSize { get; set; }
		public IPizzaStore PizzaStore { get; set; }

	}

}
