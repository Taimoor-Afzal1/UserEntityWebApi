using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;

namespace UserEntity.Controllers
{
    public class BaseController : ControllerBase
    {
        public override OkObjectResult Ok(object value)
        {
            return base.Ok(new Response()
            {
                Status = UserEntity.Response.RequestStatus.Success,
                Payload = value?.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(value, null))
            });
        }

        //public OkObjectResult Ok(string message)
        //{
        //    return base.Ok(new Response()
        //    {
        //        Status = UserEntity.Response.RequestStatus.Success,
        //        Message = message
        //    }) ;
        //}

        //public OkObjectResult Ok(object value, string message)
        //{
        //    return base.Ok(new Response()
        //    {
        //        Status = UserEntity.Response.RequestStatus.Success,
        //        Message = message,
        //        Payload = value?.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)?
        //            .ToDictionary(prop => prop.Name, prop => prop.GetValue(value, null))
        //    });
        //}

    }
}
