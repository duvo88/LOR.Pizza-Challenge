using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using LOR.Pizzeria.Models;
using LOR.Pizzeria.Services;
using Newtonsoft.Json;

namespace LOR.PizzaTest
{
	[TestClass]
	public class PizzaServiceTests
	{

		[TestMethod]
		public void BrisbaneStore_HasFlorenza()
		{
			var brisbaneStore = new BrisbaneStore();
			var pizzaBuilder = new PizzaBuilder(brisbaneStore.Name);
			pizzaBuilder = pizzaBuilder.With(PizzaType.Florenza, brisbaneStore);
			var result = pizzaBuilder.GetPizza.PizzaRecipe;
			var florenzaPizza = new PizzaFlorenza(21.00m);

			Assert.AreEqual(JsonConvert.SerializeObject(florenzaPizza), JsonConvert.SerializeObject(result));
		}

		[TestMethod]
		public void SydneyStore_HasNoFlorenza()
		{
			var sydneyStore = new SydneyStore();
			var pizzaBuilder = new PizzaBuilder(sydneyStore.Name);
			pizzaBuilder = pizzaBuilder.With(PizzaType.Florenza, sydneyStore);
			var result = pizzaBuilder.GetPizza.PizzaRecipe;

			Assert.AreEqual(null, result);
		}

		[TestMethod]
		public void BrisbaneLargeCapriciosa_ValuesAt_22_5AUD()
		{
			var brisbaneStore = new BrisbaneStore();
			var pizzaCapriciosa = brisbaneStore.PizzaRecipes.Where(x => x.Name == PizzaType.Capriciosa).First();
			var result = pizzaCapriciosa.BasePrice + PizzaSize.Large.Price;

			Assert.AreEqual(22.50m, result);
		}
				

		[TestMethod]
		public void BrisbaneLargeCapriciosa_WithExtraCheese_ValuesAt_24_5AUD()
		{
			var brisbaneStore = new BrisbaneStore();
			var pizzaCapriciosa = brisbaneStore.PizzaRecipes.Where(x => x.Name == PizzaType.Capriciosa).First();
			var result = pizzaCapriciosa.BasePrice + PizzaSize.Large.Price + Ingredient.ExtraCheese.Price;

			Assert.AreEqual(24.50m, result);
		}

		[TestMethod]
		public void SydneyMediumInferno_ValuesAt_31AUD()
		{
			var sydneyStore = new SydneyStore();
			var pizzaInferno = sydneyStore.PizzaRecipes.Where(x => x.Name == PizzaType.Inferno).First();
			var result = pizzaInferno.BasePrice + PizzaSize.Medium.Price;

			Assert.AreEqual(31.00m, result);
		}

		[TestMethod]
		public void SydneyMediumInferno_WithOliveOil_ValuesAt_32AUD()
		{
			var sydneyStore = new SydneyStore();
			var pizzaInferno = sydneyStore.PizzaRecipes.Where(x => x.Name == PizzaType.Inferno).First();
			var result = pizzaInferno.BasePrice + PizzaSize.Medium.Price + Ingredient.OliveOil.Price;

			Assert.AreEqual(32.00m, result);
		}

	}
}
