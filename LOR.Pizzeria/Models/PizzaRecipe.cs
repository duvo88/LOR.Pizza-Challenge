using System;
using System.Collections.Generic;
using System.Linq;

namespace LOR.Pizzeria.Models
{
    public abstract class PizzaRecipe
    {
        public PizzaType Name { get; set; }
        public decimal BasePrice { get; set; }
        public List<IIngredient> Ingredients { get; set; }
        public List<IIngredient> Toppings { get; set; } = new List<IIngredient>();

        //override this method if the preparation process changes for different types of pizza
        public virtual void Prepare()
		{
			Console.WriteLine($"Preparing {this.Name} ...");
            Console.WriteLine($"Adding {string.Join(", ", Ingredients.Select(x => x.Name))}...");
            //print out list of topping if any
            if (Toppings.Any())
                Console.WriteLine($"Adding toppings {string.Join(", ", Toppings.Select(x => x.Name))}...");
		}

        //MUST override this method as the baking process is different for each pizza type
        public abstract void Bake();

        //override this method if the cutting process changes for different types of pizza
        public virtual void Cut()
        {
            Console.WriteLine("Cutting pizza into 8 slices...");
        }

        //override this method if the boxing process changes for different types of pizza
        public virtual void Box()
        {
            Console.WriteLine("Putting pizza into a nice box...");
        }

    }

	public class PizzaCapriciosa : PizzaRecipe
	{
        public PizzaCapriciosa(decimal basePrice)
        {
            Name = PizzaType.Capriciosa;
            Ingredients = new List<IIngredient> { Ingredient.Mushrooms, Ingredient.Cheese, Ingredient.Ham, Ingredient.Mozzarella };
            BasePrice = basePrice;
        }

        public override void Bake()
		{
            Console.WriteLine("Baking pizza " + this.Name +  " for 15 minutes at 200 degrees...");
        }
    }

    public class PizzaFlorenza : PizzaRecipe
    {
        public PizzaFlorenza(decimal basePrice)
        {
            Name = PizzaType.Florenza;
            Ingredients = new List<IIngredient> { Ingredient.Olives, Ingredient.Pastrami, Ingredient.Mozzarella, Ingredient.Onion };
            BasePrice = basePrice;
        }

        public override void Bake()
        {
            Console.WriteLine("Baking pizza " + this.Name + " for 16 minutes at 210 degrees...");
        }

		public override void Cut()
		{
            Console.WriteLine("Cutting pizza into 6 slices with a special knife...");
        }
	}


    public class PizzaMargherita : PizzaRecipe
    {
        public PizzaMargherita(decimal basePrice)
        {
            Name = PizzaType.Margherita;
            Ingredients = new List<IIngredient> { Ingredient.Mozzarella, Ingredient.Onion, Ingredient.Garlic, Ingredient.Oregano };
            BasePrice = basePrice;
        }

        public override void Bake()
        {
            Console.WriteLine("Baking pizza " + this.Name + " for 17 minutes at 200 degrees...");
        }
    }


    public class PizzaInferno : PizzaRecipe
    {
        public PizzaInferno(decimal basePrice)
        {
            Name = PizzaType.Inferno;
            Ingredients = new List<IIngredient> { Ingredient.ChiliPeppers, Ingredient.Mozzarella, Ingredient.Chicken, Ingredient.Cheese };
            BasePrice = basePrice;
        }

        public override void Bake()
        {
            Console.WriteLine("Baking pizza " + this.Name + " for 15 minutes at 230 degrees...");
        }
    }

    public enum PizzaType
    {
        Capriciosa = 1,
        Florenza = 2,
        Margherita = 3,
        Inferno = 4
    }
}
