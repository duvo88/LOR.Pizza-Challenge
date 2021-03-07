using System;
using System.Collections.Generic;
using System.Linq;
using LOR.Pizzeria.Models;
using LOR.Pizzeria.Services;

namespace LOR.Pizzeria
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine($"Welcome to LOR Pizzeria! Please select the store location: \n1 - {StoreName.Brisbane} \n2 - {StoreName.Sydney}");
			var pizzas = new List<Pizza>();
			List<PizzaBuilder> pizzaBuilders = new List<PizzaBuilder>();

			//Start the order by selecting a store
			StoreName storeName = GetEnumValue<StoreName>();
			try
			{
				do
				{
					//construct PizzaBuilder with the store and its pizza list
					var pizzaBuilder = new PizzaBuilder(storeName);
					Console.WriteLine($"Welcome to {storeName}'s MENU");
					IPizzaStore pizzaStore = pizzaBuilder.GetPizza.PizzaStore;

					//Print out menu from selected store
					string availablePizzas = string.Empty;
					int i = 1;
					foreach (var recipe in pizzaStore.PizzaRecipes)
					{
						string description = ($"{recipe.Name} - {string.Join(", ", recipe.Ingredients.Select(x => x.Name))} - {recipe.BasePrice} AUD");
						//adds line break where suitable
						if (recipe == pizzaStore.PizzaRecipes.Last())
							availablePizzas += $"{(int)recipe.Name} - {recipe.Name}";
						else
							availablePizzas += $"{(int)recipe.Name} - {recipe.Name} \n";
						Console.WriteLine(description);
						i++;
					}

					//PizzaType
					Console.WriteLine($"\nWhat can I get you?\n{availablePizzas}");
					PizzaType pizzaType;
					bool isAvailable = false;
					do
					{
						pizzaType = GetEnumValue<PizzaType>();
						pizzaBuilder = pizzaBuilder.With(pizzaType, pizzaStore);
						if (pizzaBuilder.GetPizza.PizzaRecipe == null)
						{
							Console.WriteLine($"'{pizzaType}' is not available at store {storeName}. Please choose again.");
						}
						else
						{
							isAvailable = true;
						}
					} while (!isAvailable);
					

					//Pizza Size
					Console.WriteLine($"\nNow, please select size for '{pizzaType}'. {GetPizzaSizes()}");
					Size size = GetEnumValue<Size>();
					pizzaBuilder = pizzaBuilder.Size(size);

					//Toppings
					Console.WriteLine($"\nAdd some toppings to make your days. Separated by a comma ',' OR press 0 to continue.{GetToppingList()}");
					var toppings = GetEnumValues<Topping>(true);
					foreach (var topping in toppings)
					{
						pizzaBuilder.Topping(topping);
					}

					pizzaBuilders.Add(pizzaBuilder);

					//Summarize the Pizza selection
					var pizza = pizzaBuilder.GetPizza;
					string toppingDesc = string.Empty;
					toppingDesc = string.Join(", ", pizza.PizzaRecipe.Toppings.Select(x => x.Name));
					if (string.IsNullOrEmpty(toppingDesc))
						Console.WriteLine($"\nGreat! You have added pizza: {pizza.Description}; Price: {pizza.Price} AUD.");
					else
						Console.WriteLine($"\nGreat! You have added pizza: {pizza.Description}; Toppings: {toppingDesc}; Price: {pizza.Price} AUD.");

					//Ask for more pizza if any
					Console.WriteLine("Would you like some more pizza? \nPress 'Y' to add more OR any key to finish your order...");
				}
				while (Console.ReadLine().Equals("y", StringComparison.InvariantCultureIgnoreCase));

				Console.WriteLine("\n>>> Now cooking your order...");
				foreach (var pizzaBuilder in pizzaBuilders)
				{
					var pizza = pizzaBuilder.Cook();
					pizzas.Add(pizza);
				}

				//Print total receipt for all pizzas
				PrintReceipt(pizzas);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"There was an error happened while processing your order: {ex}");
			}
			
		}


		#region Private methods

		//Print out receipt with the total amount of all pizzas
		private static void PrintReceipt(List<Pizza> pizzas)
		{
			Console.WriteLine($"\nTotal price: {pizzas.Sum(x => x.Price)} AUD.");
			Console.WriteLine("Your order is ready! Have a nice day!");
		}

		//Handles the input from user for Menu options
		private static TEnum GetEnumValue<TEnum>(bool allowToContinue = false) where TEnum : struct
		{
			TEnum resultEnumType = default(TEnum);
			bool enumParseResult = false;

			while (!enumParseResult)
			{
				Console.Write("You choice: ");
				string userInput = Console.ReadLine();
				enumParseResult = Enum.TryParse(userInput, true, out resultEnumType);
				//asks user to input again if any invalid select
				if (!Enum.IsDefined(typeof(TEnum), resultEnumType))
				{
					Console.WriteLine("The selected value is invalid! Please try again.");
					enumParseResult = false;
				}
			}
			return resultEnumType;
		}

		//Handles multiple inputs from user for Menu options
		private static List<TEnum> GetEnumValues<TEnum>(bool allowToContinue = false) where TEnum : struct
		{
			bool enumParseResult = false;
			List<TEnum> results = null;
			while (!enumParseResult)
			{
				//gets user input
				results = new List<TEnum>();
				Console.Write("You choice: ");
				string userInput = Console.ReadLine();
				//allows to continue with a special option 
				if (userInput == "0" && allowToContinue)
					return results;

				//manipulates multiple select at the same time
				TEnum resultEnumType;
				if (userInput.Contains(","))
				{
					var inputParts = userInput.Split(',');
					foreach (var input in inputParts)
					{
						enumParseResult = Enum.TryParse(input, true, out resultEnumType);
						if (!Enum.IsDefined(typeof(TEnum), resultEnumType))
						{

							enumParseResult = false;
							break;
						}
						results.Add(resultEnumType);
					}
				}
				else// manipulates single select
				{
					enumParseResult = Enum.TryParse(userInput, true, out resultEnumType);
					if (!Enum.IsDefined(typeof(TEnum), resultEnumType))
					{
						enumParseResult = false;
					}
					results.Add(resultEnumType);
				}
				//asks user to input again if any invalid select
				if (!enumParseResult)
				{
					Console.WriteLine("The selected value(s) is invalid! Please try again.");
				}
			}
			return results;
		}

		//Create menu for user to select pizza size
		private static string GetPizzaSizes()
		{
			string menuOptions = $"\n1 - {PizzaSize.Medium.Description}; Price {PizzaSize.Medium.Price}";
			menuOptions += $"\n2 - {PizzaSize.Large.Description}; Price {PizzaSize.Large.Price}";
			menuOptions += $"\n3 - {PizzaSize.Jumbo.Description}; Price {PizzaSize.Jumbo.Price}";
			return menuOptions;
		}

		//Create menu for user to select toppings
		private static string GetToppingList()
		{
			string menuOptions = $"\n1 - {Ingredient.ExtraCheese.Name}; Price {Ingredient.ExtraCheese.Price}";
			menuOptions += $"\n2 - {Ingredient.OliveOil.Name}; Price {Ingredient.OliveOil.Price}";
			menuOptions += $"\n3 - {Ingredient.Mayo.Name}; Price {Ingredient.Mayo.Price}";
			return menuOptions;
		}

		#endregion
	}

}
