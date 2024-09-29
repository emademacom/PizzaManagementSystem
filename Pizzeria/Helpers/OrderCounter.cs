using System.Diagnostics.Metrics;

namespace Pizzeria.Helpers
{
	public class OrderCounter
	{
		private DateTime dataCorrente;
		private int contatoreOrdini;
		private readonly object lockObject = new object(); // Oggetto per il lock

		public OrderCounter()
		{
			dataCorrente = DateTime.Today;
			contatoreOrdini = 0;
		}

		// Metodo per ricevere un nuovo ordine e generare il codice
		
	}

}
