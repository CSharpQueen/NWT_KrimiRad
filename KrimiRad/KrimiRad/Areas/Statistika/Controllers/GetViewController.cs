﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrimiRad.Areas.Statistika.Controllers
{
    public class GetViewController : Controller
    {        
        public PartialViewResult BrojDjelaPoOpstinama() {
            return PartialView();
        }

        public PartialViewResult BrojDjelaPoDatumu() {
            return PartialView();
        }
        public PartialViewResult PrijavePoTipovimaZaOpstinu() {
            return PartialView();
        }
        public PartialViewResult BrojDjelaPoTipuDjela() {
            return PartialView();
        }
        public PartialViewResult OmjerRjesenihUPeriodu() {
            return PartialView();
        }
        public PartialViewResult BrojDjelaPoOpstinamaZaTipDjela() {
            return PartialView();
        }

        public PartialViewResult BrojDjelaPoDatumimaZaTipDjela() {
            return PartialView();
        }
        public PartialViewResult OmjerRjesenihPoTipuUPeriodu() {
            return PartialView();
        }
    }
}