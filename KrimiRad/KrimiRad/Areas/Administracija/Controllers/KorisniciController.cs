using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrimiRad.Areas.Administracija.Controllers
{
    public class KorisniciController : Controller
    {
        // GET: Administracija/Korisnici
        public ActionResult Index()
        {
            return View();
        }
    }
}