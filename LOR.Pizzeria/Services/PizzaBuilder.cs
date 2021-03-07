using System.Linq;
using LOR.Pizzeria.Models;

namespace LOR.Pizzeria.Services
{
    public interface IPizzaBuilder
    {
        PizzaBuilder With(PizzaType type, IPizzaStore pizzaStore);
        PizzaBuilder Size(Size size);
        PizzaBuilder Topping(Topping topping);
    }

    public class PizzaBuilder : IPizzaBuilder
    {
        private Pizza Product { get; set; }
        public PizzaBuilder (StoreName storeName)
        {
            switch (storeName)
            {
                case StoreName.Brisbane:
                    Product = new Pizza(new BrisbaneStore());
                    break;
                case StoreName.Sydney:
                    Product = new Pizza(new SydneyStore());
                    break;
                //case StoreName.GoldCoast.ToString():
                //    Product = new Pizza(new GoldCoastStore());
                //    break;
                default:
                    break;
            }
        }

        public PizzaBuilder With(PizzaType pizzaType, IPizzaStore pizzaStore)
        {
            //Finds the matching pizza in the selected store
            Product.PizzaRecipe = pizzaStore.PizzaRecipes.FirstOrDefault(x => x.Name == pizzaType);
            if (Product.PizzaRecipe != null)
            {
                Product.Description += pizzaType;
                Product.Price += Product.PizzaRecipe.BasePrice;
            }

            return this;
        }

        public PizzaBuilder Size(Size size)
        {
			Product.PizzaSize = size switch
			{
				Models.Size.Medium => PizzaSize.Medium,
				Models.Size.Large => PizzaSize.Large,
				Models.Size.Jumbo => PizzaSize.Jumbo,
				_ => PizzaSize.Medium,
			};
			Product.Description += $" size {size}";
            Product.Price += Product.PizzaSize.Price;
            return this;
        }


        public PizzaBuilder Topping(Topping topping)
        {
            switch (topping)
            {
                case Models.Topping.ExtraCheese:
                    Product.PizzaRecipe.Toppings.Add(Ingredient.ExtraCheese);
                    Product.Price += Ingredient.ExtraCheese.Price;
                    break;
                case Models.Topping.OliveOil:
                    Product.PizzaRecipe.Toppings.Add(Ingredient.OliveOil);
                    Product.Price += Ingredient.ExtraCheese.Price;
                    break;
                case Models.Topping.Mayo:
                    Product.PizzaRecipe.Toppings.Add(Ingredient.Mayo);
                    Product.Price += Ingredient.ExtraCheese.Price;
                    break;
                default:
                    break;
            }

            return this;
        }

        public Pizza GetPizza => Product;

		public Pizza Cook()
		{
            Product.PizzaRecipe.Prepare();
            Product.PizzaRecipe.Bake();
            Product.PizzaRecipe.Cut();
            Product.PizzaRecipe.Box();
            return Product;
		}
	}



}
