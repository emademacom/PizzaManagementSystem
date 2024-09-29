using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ModelViews
{	
	public class OrderModelViewResponse
	{
		public string? Codice { get; set; }
		public string? Customername { get; set; }
		
		public string? Customermobile { get; set; }

		public string totalQty { get; set; }
		public string totalPrice { get; set; }

		public List<OrderModelViewDetailResponse>? details { get; set; }

	}
	public class OrderModelViewDetailResponse
	{
		public string? Name { get; set; }
		public int? Qty { get; set; }

		public double? Price { get; set; }
	}
}
