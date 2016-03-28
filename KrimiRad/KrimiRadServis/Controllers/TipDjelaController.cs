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
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KrimiRadServis.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]  
    [Authorize] 
    public class TipDjelaController : ApiController
    {
        private AppDbContext db = new AppDbContext();

        // GET: api/TipDjela
        [ResponseType(typeof(List<TipDjela>))]
        public IHttpActionResult GetTipDjela()
        {                        
            return Json<List<TipDjela>>(db.TipDjela.ToList());
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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TipDjela
        [ResponseType(typeof(TipDjela))]
        public async Task<IHttpActionResult> PostTipDjela(TipDjela tipDjela)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipDjela.Add(tipDjela);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tipDjela.ID }, tipDjela);
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

            return Ok(tipDjela);
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