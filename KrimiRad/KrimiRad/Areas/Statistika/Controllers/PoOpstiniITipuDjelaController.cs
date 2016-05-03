using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrimiRad.Areas.Statistika.Controllers
{
    public class PoOpstiniITipuDjelaController : Controller
    {
        // GET: Statistika/PoOpstiniITipuDjela
        public PartialViewResult Index()
        {
            return PartialView();
        }
    }
}