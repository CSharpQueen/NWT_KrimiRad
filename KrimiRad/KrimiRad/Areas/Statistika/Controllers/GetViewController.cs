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

        // GET: Statistika/GetView/PoOpstini
        public PartialViewResult PoOpstini()
        {
            return PartialView();
        }

        // GET: Statistika/GetView/PoTipuDjela
        public PartialViewResult PoTipuDjela()
        {
            return PartialView();
        }

        // GET: Statistika/GetView/PoDatumu
        public PartialViewResult PoDatumu()
        {
            return PartialView();
        }

        public PartialViewResult BrojDjelaPoOpstinama() {
            return PartialView();
        }
    }
}