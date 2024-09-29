using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace ModelViews
{
	public static class PizzaList
	{
		public static List<Pizza> Menu = new List<Pizza>
		{						
			new Pizza(){id=1, name = "Diavola" , price = 6.50, currency = "EUR" },
			new Pizza(){id=2, name = "Bufalina" , price = 7, currency = "EUR" },
			new Pizza(){id=3, name = "Margherita" , price = 5, currency = "EUR" },
			new Pizza(){id=4, name = "Ortolana" , price = 6, currency = "EUR" },
		};
	}

	public class Pizza
	{
		public int id { get; set; }
		public string name { get; set; }
		public double price { get; set; }
		public string currency { get; set; }

	}
}
