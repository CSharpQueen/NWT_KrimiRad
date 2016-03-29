using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

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