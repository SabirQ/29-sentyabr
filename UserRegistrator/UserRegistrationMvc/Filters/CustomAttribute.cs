using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserRegistrationMvc.DataContext;
using UserRegistrationMvc.Models;

namespace UserRegistrationMvc.Filters
{
    public class CustomAttribute: ActionFilterAttribute
    {
        private readonly RoleEnum _role;
        private static  string LOGIN_SESSION_KEY = "login";
        public CustomAttribute(RoleEnum role)
        {
            _role = role;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string role = context.HttpContext.Session.GetString(LOGIN_SESSION_KEY);
            RoleEnum roleEnum;
            bool result = Enum.TryParse(role, out roleEnum);
            if (role==null||result==false)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary{{"area",null },{ "controller", "auth" },{ "action", "register" }});
            }
            if ((int)roleEnum<=(int)_role)
            {
                base.OnActionExecuted(context);
            }
            else
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary{{"area",null },{ "controller", "home" },{ "action", "index" }});
            }
        }
    }
}
