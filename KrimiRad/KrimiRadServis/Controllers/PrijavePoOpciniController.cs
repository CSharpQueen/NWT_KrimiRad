using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KrimiRadServis.Controllers
{
    public class PrijavePoOpciniController : ApiController
    {

       
            private AppDbContext db = new AppDbContext();

            [HttpGet]
            public async Task<IHttpActionResult> Get(string opstina)
            {

                List<Prijava> prijave = db.Prijava.Where(p => p.Opstina.Contains(opstina)).ToList();
                if (prijave == null) return Json("Ne postoje prijave za ovu opštinu");
                return Json<List<Prijava>>(prijave);
            }

}
}
