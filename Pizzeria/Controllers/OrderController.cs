using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using ModelDto.Models;
using ModelViews;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrderController : ControllerBase
	{		
		private readonly ILogger<OrderController> _logger;
		private MyContext context;
		public OrderController(ILogger<OrderController> logger, MyContext context)
		{
			_logger = logger;
			this.context = context;
		}

		[HttpPost]
		[Route("order")]
		[ProducesResponseType<OrderModelViewCreateResponse>(StatusCodes.Status200OK)]
		[SwaggerOperation(
			Summary = "Crea nuovo ordine",
			Description = "Restituisce la lista delle pizze con Name,Price (caricate staticamente)" +
			"  1) Diavola, price 6.50   " +
			"  2) Bufalina, price 7   " +
			"  3) Margherita, price 5 " +
			"  4) Ortolana, price 6" +
			"")]
		
		public IActionResult createOrder([FromBody] OrderModelViewCreate _order)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				if(_order.details == null || _order.details.Count == 0)
					return BadRequest("Pizza not selected");
				
				Order order = new Order();
				order.Id = Utilities.GenerateGuid(); //con SQL Server non necessario Guid incluso
				order.Codice = Utilities.GenerateUniqueId().ToUpper();
				if (String.IsNullOrEmpty(_order.Customername))
					order.Customername = _order.Customername;
				if (String.IsNullOrEmpty(_order.Customername))
					order.Customermobile = _order.Customermobile;
				order.CreatedAt = DateTime.Now;
				order.UpdatedAt = DateTime.Now;

				context.Orders.Add(order);
				
				//context.SaveChanges();
				List<OrderDetail> details = new List<OrderDetail>();
				double totQty = 0;
				double totPrice = 0;
				foreach (var item in _order.details)
				{
					Pizza pizza = PizzaList.Menu.Where(x=>x.id == item.pizzaId).FirstOrDefault();
					if (pizza == null) continue;

					OrderDetail detail = new OrderDetail();
					detail.Id = Utilities.GenerateGuid();
					detail.OrderId = order.Id;
					detail.Qty = item.Qty;
					detail.Name = pizza.name;
					detail.Price = pizza.price;
					detail.CreatedAt = DateTime.Now;
					detail.UpdatedAt = DateTime.Now;
					totQty += item.Qty;
					totPrice += (item.Qty*pizza.price);
					details.Add(detail);
				}
				if(details.Count == 0) return BadRequest("Pizze details not valid");

				context.OrderDetails.AddRange(details);
				context.SaveChanges();

				OrderModelViewCreateResponse result = new OrderModelViewCreateResponse();
				result.totalPrice = totPrice.ToString("0.00");
				result.totalQty = totQty.ToString();
				result.code = order.Codice;
				result.queueNumber = context.Orders
					.Where(x => (x.IsCompleted == null || x.IsCompleted == 0)
						&& x.Codice != order.Codice
					)
					.Count();
				return Ok(result);
			}
			catch (Exception e) {
				return BadRequest("Saving issue");
			}
		}

		[HttpPut]
		[Route("order/{orderCode}")]
		[ProducesResponseType<int>(StatusCodes.Status200OK)]
		[SwaggerOperation(
			Summary = "Viene utilizzata per dichiarare l'ordine completo",
			Description = "Ritorna il numero di ordini ancora in coda")
		]
		public IActionResult completeOrder(
			[FromRoute] String orderCode,
			[FromBody] OrderModelViewComplete _order
			)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				Order order = context.Orders.Where(x => x.Codice.ToUpper() == orderCode.ToUpper()).FirstOrDefault();
				if (order == null)
					return BadRequest("Order not found");

				order.IsCompleted = _order.orderCompleted?(ulong)1: (ulong)0;
				order.UpdatedAt = DateTime.Now;
				context.SaveChanges();
				int result = context.Orders
					.Where(x => (x.IsCompleted == null || x.IsCompleted == 0))
					.Count();

				return Ok(result);
			}
			catch (Exception e)
			{
				return BadRequest("Saving issue");
			}
		}

		[HttpGet]
		[Route("order_to_prepare")]
		[ProducesResponseType<OrderModelViewResponse>(StatusCodes.Status200OK)]
		[SwaggerOperation(
			Summary = "Endpoint che restituisce l'ordine da preparare",
			Description = "Ritorna l'ordine e i suoi dettagli")
		]
		public IActionResult orderToPrepare()
		{
			Order _order = context.Orders
				.Where(x => (x.IsCompleted == null || x.IsCompleted == 0))
				.OrderBy(x => x.CreatedAt)
				.FirstOrDefault();
			if (_order == null)
				return Ok();

			OrderModelViewResponse result = new OrderModelViewResponse();
			result.Codice = _order.Codice;
			result.Customername = _order.Customername;
			result.Customermobile = _order.Customermobile;
			result.details = new List<OrderModelViewDetailResponse>();
			double totQty = 0;
			double totPrice = 0;
			foreach (OrderDetail detail in _order.OrderDetails) {
				OrderModelViewDetailResponse det = new OrderModelViewDetailResponse();
				det.Name = detail.Name;
				det.Qty = detail.Qty;
				det.Price = detail.Price;				
				totQty += detail.Qty??1;
				totPrice += ((detail.Qty??1) * detail.Price??0);				
			}
			result.totalPrice = totPrice.ToString("0.00");
			result.totalQty = totQty.ToString();
			return Ok(result);
		}
	}
}
