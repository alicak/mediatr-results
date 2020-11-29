using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
	public abstract class AbstractControllerBase : ControllerBase
	{
		protected ActionResult<T> Ok<T>(T resultValue)
		{
			return base.Ok(resultValue);
		}

		protected ActionResult<T> CreatedAtAction<T>(string actionName, object routeValue, T resultValue)
		{
			return base.CreatedAtAction(actionName, routeValue, resultValue);
		}
	}
}