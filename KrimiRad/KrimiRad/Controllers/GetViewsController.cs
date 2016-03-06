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
        public PartialViewResult GetMap()
        {
            return PartialView();
        }
    }
}