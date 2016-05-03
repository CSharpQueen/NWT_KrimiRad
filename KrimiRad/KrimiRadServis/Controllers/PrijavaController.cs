using DataAccess;
using DataAccess.Entity;
using KrimiRadServis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace KrimiRadServis.Controllers
{
    //[Authorize]

    [EnableCors("*", "*", "*")]
    public class PrijavaController : ApiController
    {
        private AppDbContext db = new AppDbContext();
        // GET api/prijava
        [ResponseType(typeof(List<Prijava>))]
        public IHttpActionResult GetPrijava()
        {

            var prijave = new List<Prijava>() {
                new Prijava {
                    DatumIVrijemePocinjenjaDjela = DateTime.Now,                    
                    Grad = "Sarajevo",
                    Adresa = "Skenderija 12",
                    Longituda = 18.371985,
                    Latituda = 43.852723,
                    TipDjela = new TipDjela() {
                        Naziv = "Ubistvo",
                        Opis = "LALALAL",
                        StrucniNaziv = "lalalaal",
                        VrstaDjela = "adsfasf"
                    }
                },

                new Prijava {
                    DatumIVrijemePocinjenjaDjela = DateTime.Now,                    
                    Grad = "Sarajevo2",
                    Adresa = "Safeta zajke 13",
                    Longituda = 43.851980,
                    Latituda = 18.345206,
                    TipDjela = new TipDjela() {
                        Naziv = "Ubistvo2",
                        Opis = "LALALAL2",
                        StrucniNaziv = "lalalaal2",
                        VrstaDjela = "adsfasf2"
                    }
                },
            };


            //otkomentarisat kasnije
            //var prijave = db.Prijava.Select(s => new { s.DatumIVrijemePocinjenjaDjela, s.Grad, s.Adresa, s.Latituda, s.Longituda, s.TipDjela }).ToList();
            


            return Json<List<Prijava>>(prijave);
        }

        // GET api/Prijava/5
        [ResponseType(typeof(Prijava))]
        public async Task<IHttpActionResult> GetPrijava(int id)
        {
            Prijava prijava = await db.Prijava.FindAsync(id);
            if (prijava == null)
            {
                return NotFound();
            }

            return Ok(prijava);
        }

        // POST api/prijava
        [ResponseType(typeof(Prijava))]
        public async Task<IHttpActionResult> PostPrijava(PrijavaCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var prijava = new Prijava() {
                //popunit
            };
            db.Prijava.Add(prijava);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = prijava.ID }, prijava);
        }

        // PUT api/prijava/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPrijava(int id, Prijava prijava)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prijava.ID)
            {
                return BadRequest();
            }

            db.Entry(prijava).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrijavaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/prijava/5
        [ResponseType(typeof(Prijava))]
        public async Task<IHttpActionResult> DeletePrijava(int id)
        {
            Prijava prijava = await db.Prijava.FindAsync(id);
            if (prijava == null)
            {
                return NotFound();
            }

            db.Prijava.Remove(prijava);
            await db.SaveChangesAsync();

            return Ok(prijava);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrijavaExists(int id)
        {
            return db.Prijava.Count(e => e.ID == id) > 0;
        }

    }
}