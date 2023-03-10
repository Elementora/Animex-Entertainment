using AnimexEntertainmentNetwork.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnimexEntertainmentNetwork.Web.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        protected readonly AnimexService _udb;

        public BaseController(AnimexService udb)
        {
            _udb = udb;

        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {

            return base.BeginExecuteCore(callback, state);
        }
	}
}