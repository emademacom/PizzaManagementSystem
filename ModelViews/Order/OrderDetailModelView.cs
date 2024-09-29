using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace ModelViews
{	

	public class OrderDetailModelView
	{				

		[Required(ErrorMessage = "Pizza: Id field is required")]
		public int pizzaId { get; set; }

		[Required(ErrorMessage = "Pizza: Qty field is required")]
		public int Qty { get; set; }
	}
}
