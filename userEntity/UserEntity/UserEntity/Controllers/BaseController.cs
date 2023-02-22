using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;
using UserEntity.Model;

namespace UserEntity.Controllers
{
    public class BaseController : ControllerBase
    {
        public override OkObjectResult Ok(object value)
        {
            return base.Ok(new Response()
            {
                Status = Model.Response.RequestStatus.Success,
                Payload = value?.GetType().GetProperties()
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(value, null))
            });
        }

        [NonAction]
        public 
            OkObjectResult Ok(string message)
        {
            return base.Ok(new Response()
            {
                Status = Model.Response.RequestStatus.Success,
                Message = message
            });
        }

        [NonAction]
        public  OkObjectResult Ok(object value, string message)
        {
            return base.Ok(new Response()
            {
                Status = Model.Response.RequestStatus.Success,
                Message = message,
                Payload = value?.GetType().GetProperties()
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(value, null))
            });
        }

        [NonAction]
        public override BadRequestObjectResult BadRequest(object value)
        {
            return base.BadRequest(new Response()
            {
                Status = Model.Response.RequestStatus.Error,
                Message = value.ToString(),
                Error = null
            });
        }

    }
}
























