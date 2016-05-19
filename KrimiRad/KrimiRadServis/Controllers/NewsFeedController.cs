using DataAccess;
using DataAccess.Entity;
using KrimiRadServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace KrimiRadServis.Controllers
{
    [EnableCors("*", "*", "*")]
    public class NewsFeedController : ApiController
    {
        private AppDbContext db = new AppDbContext();
        // GET: api/NewsFeed
        [HttpGet]
        public IHttpActionResult GetZadnjePrijave()
        {
            List<NewsFeedViewModel> prijave = db.Prijava.Select(s => new NewsFeedViewModel() { PrijavaId = s.ID, Adresa = s.Adresa, DatumIVrijemePocinjenjaDjela = s.DatumIVrijemePocinjenjaDjela, Opstina = s.Opstina, TipDjela = s.TipDjela.Naziv }).ToList();
                       
            if(prijave.Count==0) return Json(new { poruka = "Mirno stanje u Kantonu" });
            else
            return Json<List<NewsFeedViewModel>>(prijave);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
