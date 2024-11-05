using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStoreWeb.Services
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionType = context.HttpContext.Session.GetString("Type");
            if (sessionType != "0")
            {
                context.Result = new RedirectToActionResult("Index","Logins",null); 
              
            }
            base.OnActionExecuting(context);
        }
    }
}
