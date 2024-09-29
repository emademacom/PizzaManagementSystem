using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ModelViews
{	
	public class OrderModelViewComplete
	{		
		[Required(ErrorMessage = "orderCompleted field is required")]
		[Range(typeof(bool), "true", "true")]
		public Boolean orderCompleted { get; set; }

	}
}
