﻿using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Pizzeria.Filters
{
	public class MyFilters : ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			if (actionContext.ModelState.IsValid == false)
			{
				actionContext.Response = actionContext.Request.CreateErrorResponse(
					HttpStatusCode.BadRequest, actionContext.ModelState);
			}
		}
	}
}
