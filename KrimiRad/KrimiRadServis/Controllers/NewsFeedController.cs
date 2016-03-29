using DataAccess;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KrimiRadServis.Controllers
{
    public class NewsFeedController : ApiController
    {
        private AppDbContext db = new AppDbContext();
        // GET: api/NewsFeed
        [HttpGet]
        public IHttpActionResult GetZadnjePrijave()
        {
            List<Prijava> prijave = db.Prijava.ToList();
            List<Prijava> lista = new List<Prijava>();

            foreach (Prijava p in prijave)
            {
                if (p.DatumIVrijemePocinjenjaDjela.Date == DateTime.Now.AddDays(-1) || p.DatumIVrijemePocinjenjaDjela.Date == DateTime.Now.Date)
                    lista.Add(p);

            }
            if(lista.Count==0) return Json(new { poruka = "Mirno stanje u Kantonu" });
            else
            return Json<List<Prijava>>(lista);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
