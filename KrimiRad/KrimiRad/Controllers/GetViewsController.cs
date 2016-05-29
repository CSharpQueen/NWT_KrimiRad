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
        [Authorize(Roles = "NadlezniOrgan")]
        public PartialViewResult GetPrijave() {
            return PartialView();
        }
        [Authorize(Roles = "NadlezniOrgan")]
        public PartialViewResult GetStatistika() {
            return PartialView();
        }

        [Authorize(Roles = "Administrator")]
        public PartialViewResult GetAdministracija() {
            return PartialView();
        }
        [Authorize(Roles = "NadlezniOrgan")]
        public PartialViewResult GetPrijavaDetalji() {
            return PartialView();
        }

    }
}