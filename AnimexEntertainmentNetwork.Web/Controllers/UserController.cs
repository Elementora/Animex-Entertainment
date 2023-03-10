using AnimexEntertainmentNetwork.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnimexEntertainmentNetwork.Web.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        private UserAndOtherEntities db = new UserAndOtherEntities();
        public ActionResult UserList()
        {
            List<USER> ulist = db.USER.ToList();
            return View(ulist);
        }
	}
}