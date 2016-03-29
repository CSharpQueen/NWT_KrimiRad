using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccess;
using DataAccess.Entity;
using System.Web.Http.Cors;
using Newtonsoft.Json;

namespace KrimiRadServis.Controllers
{
    public class PrijavePoTipuDjelaController : ApiController
    {

        private AppDbContext db = new AppDbContext();

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {

            List<Prijava> prijave = db.Prijava.Where(p => p.TipDjelaId.Contains(id)).ToList();
            if (prijave == null) return Json("Ne postoje prijave za ovaj tip djela");
            return Json<List<Prijava>>(prijave);
        }
    }
}