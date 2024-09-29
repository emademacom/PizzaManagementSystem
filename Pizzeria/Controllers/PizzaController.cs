using Microsoft.AspNetCore.Mvc;
using ModelViews;
using Swashbuckle.AspNetCore.Annotations;

namespace Pizzeria.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PizzaController : ControllerBase
	{		
		private readonly ILogger<PizzaController> _logger;

		public PizzaController(ILogger<PizzaController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "pizze")]		
		[SwaggerOperation(
			Summary="Lista pizza", 
			Description="Restituisce la lista delle pizze con Name,Price (caricate staticamente)")]
		[ProducesResponseType<List<Pizza>>(StatusCodes.Status200OK)]
		public IActionResult getPizze()
		{
			return Ok(PizzaList.Menu.OrderBy(x=>x.id).ToList());
		}
	}
}
