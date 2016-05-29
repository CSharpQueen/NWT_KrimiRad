using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrimiRad.Areas.Administracija.Controllers {
    [Authorize(Roles = "Administrator")]
    public class TipDjelaController : Controller {
        // GET: Administracija/TipDjela
        public PartialViewResult Index() {
            return PartialView();
        }       
    }
}