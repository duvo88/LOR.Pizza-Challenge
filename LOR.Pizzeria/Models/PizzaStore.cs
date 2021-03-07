using System.Collections.Generic;


namespace LOR.Pizzeria.Models
{
	public interface IPizzaStore
	{
		StoreName Name { get; }
		List<PizzaRecipe> PizzaRecipes { get; }
	}

	public class BrisbaneStore : IPizzaStore
	{
		public StoreName Name { get; set; } = StoreName.Brisbane;
		public List<PizzaRecipe> PizzaRecipes { get; set; } = new List<PizzaRecipe>(){
				new PizzaCapriciosa(20.00m),
				new PizzaFlorenza(21.00m) ,
				new PizzaMargherita(22.00m)
			};
	}

	public class SydneyStore : IPizzaStore
	{
		public StoreName Name { get; set; } = StoreName.Sydney;
		public List<PizzaRecipe> PizzaRecipes { get; set; } = new List<PizzaRecipe>(){
				new PizzaCapriciosa(30.00m),
				new PizzaInferno(31.00m) 
			};
	}

	////more stores here, e.g. GoldCoast
	//public class GoldCoastStore : IPizzaStore
	//{
	//	public string Name { get; set; } = StoreName.GoldCoast.ToString();
	//	public List<PizzaType> PizzaTypes { get; set; } = new List<PizzaType>(){
	//			new PizzaCapriciosa(),
	//			new PizzaInferno(),
	//			new PizzaMargherita()
	//		};
	//}


	public enum StoreName
	{
		Brisbane = 1,
		Sydney = 2,
		//GoldCoast = 3
	}
}
