using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StarSecurityService.Extentions
{
    public class SessionAdminFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var result = context.HttpContext.Session.GetObjectFromJson<UserSession>("UserDetails");
            if (result == null)
            {
                context.Result = null;
            }
            else if (result != null && result.UserRoleId == 1 || result.UserRoleId == 2)
            {
                context.Result = new RedirectResult("~/Admin/");
            }
        }
    }
}
