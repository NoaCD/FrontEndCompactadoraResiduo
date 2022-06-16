using FrontEndCompactadoraResiduos.Controllers;
using System;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Polly;

namespace FrontEndCompactadoraResiduos.Filters
{
    public class VerificarSession : ActionFilterAttribute
    {
       

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                string oUsuarios = HttpContext.Session.GetString(SessionKeyNombre);
            }
            catch
            {

            }
        }

    }
}
