using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrimiRad.Controllers
{
    public class GetViewsController : Controller
    {
        // GET: GetViews
        
        public PartialViewResult GetPrijave() {
            return PartialView();
        }

        public PartialViewResult GetStatistika() {
            return PartialView();
        }

        public PartialViewResult GetAdministracija() {
            return PartialView();
        }

        public PartialViewResult GetPrijavaDetalji() {
            return PartialView();
        }

    }
}