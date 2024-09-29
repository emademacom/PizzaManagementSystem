using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ModelViews
{	
	public class OrderModelViewCreate
	{	
		[StringLength(100, ErrorMessage = "Customer name length can't be more than 100.")]
		public string? Customername { get; set; }

		[StringLength(20,ErrorMessage = "The order not contains any pizza.")]
		public string? Customermobile { get; set; }

		//public List<OrderDetailModelView>? details { get; set; }
		public List<OrderDetailModelView>? details { get; set; }

	}
}
