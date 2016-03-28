using DataAccess;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace KrimiRadServis.Controllers
{
    public class MedijController : ApiController
    {
        private AppDbContext db = new AppDbContext();

        // GET: api/Medij
        [ResponseType(typeof(List<Medij>))]
        public IHttpActionResult GetMedij()
        {
            return Json<List<Medij>>(db.Medij.ToList());
        }

        // GET: api/Medij/5
        [ResponseType(typeof(Medij))]
        public async Task<IHttpActionResult> GetMedij(int id)
        {
            Medij medij = await db.Medij.FindAsync(id);
            if (medij == null)
            {
                return NotFound();
            }

            return Ok(medij);
        }

        // PUT: api/Medij/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMedij(int id, Medij medij)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medij.ID)
            {
                return BadRequest();
            }

            db.Entry(medij).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedijExists(id))
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

        // POST: api/Medij
        [ResponseType(typeof(Medij))]
        public async Task<IHttpActionResult> PostMedij(Medij medij)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Medij.Add(medij);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = medij.ID }, medij);
        }

        // DELETE: api/Medij/5
        [ResponseType(typeof(Medij))]
        public async Task<IHttpActionResult> DeleteMedij(int id)
        {
            Medij medij = await db.Medij.FindAsync(id);
            if (medij == null)
            {
                return NotFound();
            }

            db.Medij.Remove(medij);
            await db.SaveChangesAsync();

            return Ok(medij);
        }


        private bool MedijExists(int id)
        {
            return db.Medij.Count(e => e.ID == id) > 0;
        }
    }
}
