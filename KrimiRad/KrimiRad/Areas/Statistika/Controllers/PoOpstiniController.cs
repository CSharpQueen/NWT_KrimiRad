using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrimiRad.Areas.Statistika.Controllers
{
    public class PoOpstiniController : Controller
    {
        // GET: Statistika/PoOpstini
        public PartialViewResult Index()
        {
            return PartialView();
        }
    }
}