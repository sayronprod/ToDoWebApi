using Microsoft.AspNetCore.Mvc;

namespace ToDo.WebApi.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected void SetStatusCode(int statusCode)
        {
            this.HttpContext.Response.StatusCode = statusCode;
        }
    }
}
