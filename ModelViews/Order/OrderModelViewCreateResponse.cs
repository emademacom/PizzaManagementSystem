using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ModelViews
{	
	public class OrderModelViewCreateResponse
	{	
		//codice ordine
		public string code { get; set; }

		public string totalQty { get; set; }
		public string totalPrice { get; set; }

		public int queueNumber { get; set; }
	}
}
