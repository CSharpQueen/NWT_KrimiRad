using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrimiRadServis.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewBag.Title = "Krimi rad API";

            return View();
        }
    }
}
