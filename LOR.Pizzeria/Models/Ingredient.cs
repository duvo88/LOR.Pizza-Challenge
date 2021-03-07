using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LOR.Pizzeria.Models
{
        public interface IIngredient
    {
        string Name { get; }
        decimal Price { get; }
    }


    public class Ingredient : IIngredient
	{
        public string Name { get; set; }
        public decimal Price { get; set; }


        //TODO allow to control the quantity
        public static IIngredient Mushrooms = new Ingredient { Name = "Mushrooms", Price = 2.00m };
		public static IIngredient Cheese = new Ingredient { Name = "Cheese", Price = 2.00m };
        public static IIngredient Ham = new Ingredient { Name = "Ham", Price = 2.00m };
        public static IIngredient Mozzarella = new Ingredient { Name = "Mozzarella", Price = 0.50m };
        public static IIngredient Olives = new Ingredient { Name = "Olives", Price = 1.00m };
        public static IIngredient Pastrami = new Ingredient { Name = "Pastrami", Price = 1.00m };
        public static IIngredient Onion = new Ingredient { Name = "Onion", Price = 1.00m };
        public static IIngredient Garlic = new Ingredient { Name = "Garlic", Price = 1.00m };
        public static IIngredient Oregano = new Ingredient { Name = "Oregano", Price = 1.00m };
        public static IIngredient ChiliPeppers = new Ingredient { Name = "Chili Pepers", Price = 1.00m };
        public static IIngredient Chicken = new Ingredient { Name = "Chicken", Price = 1.00m };

        //toppings
        public static IIngredient ExtraCheese = new Ingredient { Name = "Extra Cheese", Price = 2.00m };
        public static IIngredient OliveOil = new Ingredient { Name = "Olive Oil", Price = 1.00m };
        public static IIngredient Mayo = new Ingredient { Name = "Mayo", Price = 1.50m };

    }

    //for the purpose of this challenge, there will be only 3 toppings
    //however, the toppings can be loaded from a database which are also the whole list of ingredients
    //that can give customer more options to create different flavour
    public enum Topping
    {
        [Description("Extra Cheese")]
        ExtraCheese = 1,
        [Description("Olive Oil")]
        OliveOil = 2,
        [Description("Mayo Sauce")]
        Mayo = 3
    }
}
