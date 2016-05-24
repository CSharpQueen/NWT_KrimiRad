using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccess;
using DataAccess.Entity;
using Newtonsoft.Json;
using System.Web.Http.Cors;
using PagedList;

namespace KrimiRadServis.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "PUT, POST, GET, DELETE, OPTIONS")]
    public class TipDjelaController : ApiController
    {
        private AppDbContext db = new AppDbContext();


        public IHttpActionResult Get() {
            return Json(db.TipDjela);
        }

        // GET: api/TipDjela        
        public IHttpActionResult Get(int page)
        {
            var model = db.TipDjela;
            if (model == null) return null;
            return Json(new { count = model.Count(), tipoviDjela = model.ToList().ToPagedList(page, 7).ToList() });
        }

        // GET: api/TipDjela/5
        [ResponseType(typeof(TipDjela))]
        public async Task<IHttpActionResult> GetTipDjela(int id)
        {
            TipDjela tipDjela = await db.TipDjela.FindAsync(id);
            if (tipDjela == null)
            {
                return NotFound();
            }

            return Ok(tipDjela);
        }

        // PUT: api/TipDjela/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTipDjela(int id, TipDjela tipDjela)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipDjela.ID)
            {
                return BadRequest();
            }

            db.Entry(tipDjela).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipDjelaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Json(new { poruka = "Tip djela je uredjen!", tipDjela = tipDjela });
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TipDjela
        [ResponseType(typeof(TipDjela))]
        public async Task<IHttpActionResult> PostTipDjela(TipDjela tipDjela) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            db.TipDjela.Add(tipDjela);
            await db.SaveChangesAsync();

            return Json(new { poruka = "Tip djela je kreiran!", tipDjela = tipDjela });
        }

        // DELETE: api/TipDjela/5
        [ResponseType(typeof(TipDjela))]
        public async Task<IHttpActionResult> DeleteTipDjela(int id)
        {
            TipDjela tipDjela = await db.TipDjela.FindAsync(id);
            if (tipDjela == null)
            {
                return NotFound();
            }

            db.TipDjela.Remove(tipDjela);
            await db.SaveChangesAsync();

            return Json(new { poruka = "Tip djela je obrisan!", tipDjela = tipDjela });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipDjelaExists(int id)
        {
            return db.TipDjela.Count(e => e.ID == id) > 0;
        }
    }
}