using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KrimiRadServis.Controllers
{
    public class AlbumController : ApiController
    {
        // GET api/album
        [ResponseType(typeof(List<Album>))]
        public IHttpActionResult GetAlbum()
        {
            return Json<List<Album>>(db.Album.ToList());
        }


        // GET api/Album/5
        [ResponseType(typeof(Album))]
        public async Task<IHttpActionResult> GetAlbum(int id)
        {
            Album album = await db.Album.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }

        // POST api/album
        [ResponseType(typeof(Album))]
        public async Task<IHttpActionResult> PostAlbum(Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Album.Add(album);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = album.ID }, album);
        }


        // PUT api/album/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAlbum(int id, Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != album.ID)
            {
                return BadRequest();
            }

            db.Entry(album).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(id))
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


        // DELETE api/album/5
        [ResponseType(typeof(Album))]
        public async Task<IHttpActionResult> DeleteAlbum(int id)
        {
            Album album = await db.Album.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            db.Album.Remove(album);
            await db.SaveChangesAsync();

            return Ok(album);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlbumExists(int id)
        {
            return db.Album.Count(e => e.ID == id) > 0;
        }

    }
}
}