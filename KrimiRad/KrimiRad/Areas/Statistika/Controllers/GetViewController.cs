using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrimiRad.Areas.Statistika.Controllers
{
    public class GetViewController : Controller
    {
        // GET: Statistika/GetView/PoOpstiniITipuDjela
        public PartialViewResult PoOpstiniITipuDjela()
        {
            return PartialView();
        }
    }
}