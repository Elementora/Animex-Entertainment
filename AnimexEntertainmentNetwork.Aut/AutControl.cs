using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AnimexEntertainmentNetwork.Aut
{
    public class AutControl : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            if (httpContext.Request.Cookies[FormsAuthentication.FormsCookieName] == null)

                return false;
            else
                return true;

        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            filterContext.Result = new RedirectResult("/Home?ReturnUrl=" + filterContext.HttpContext.Request.Url.PathAndQuery);
        }

    }
}
