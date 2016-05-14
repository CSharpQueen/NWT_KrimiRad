using DataAccess;
using DataAccess.Entity;
using KrimiRadServis.Models;
using KrimiRadServis.Providers;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace KrimiRadServis.Controllers {
    //[Authorize]

    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/prijava")]
    public class PrijavaController : ApiController {
        private AppDbContext db = new AppDbContext();
        // GET api/prijava
        [ResponseType(typeof(List<Prijava>))]
        public IHttpActionResult GetPrijava() {

            //var prijave = new List<Prijava>() {
            //    new Prijava {
            //        DatumIVrijemePocinjenjaDjela = DateTime.Now,                    
            //        Grad = "Sarajevo",
            //        Adresa = "Skenderija 12",
            //        Longituda = 18.371985,
            //        Latituda = 43.852723,
            //        TipDjela = new TipDjela() {
            //            Naziv = "Ubistvo",
            //            Opis = "LALALAL",
            //            StrucniNaziv = "lalalaal",
            //            VrstaDjela = "adsfasf"
            //        }
            //    },

            //    new Prijava {
            //        DatumIVrijemePocinjenjaDjela = DateTime.Now,                    
            //        Grad = "Sarajevo2",
            //        Adresa = "Safeta zajke 13",
            //        Longituda = 43.851980,
            //        Latituda = 18.345206,
            //        TipDjela = new TipDjela() {
            //            Naziv = "Ubistvo2",
            //            Opis = "LALALAL2",
            //            StrucniNaziv = "lalalaal2",
            //            VrstaDjela = "adsfasf2"
            //        }
            //    },
            //};


            //otkomentarisat kasnije
            //var prijave = db.Prijava.Select(s => new { s.ID, s.DatumIVrijemePocinjenjaDjela, s.Grad, s.Adresa, s.Latituda, s.Longituda, s.TipDjela.Naziv }).ToList();
            var prijave = (from p in db.Prijava
                           join t in db.TipDjela on p.TipDjelaId equals t.ID
                           select new {
                               ID = p.ID,
                               DatumIVrijemePocinjenjaDjela = p.DatumIVrijemePocinjenjaDjela,
                               DatumIVrijemePrijave = p.DatumIVrijemePrijave,
                               Grad = p.Grad,
                               Adresa = p.Adresa,
                               Longituda = p.Longituda,
                               Latituda = p.Latituda,
                               TipDjelaId = t.ID
                           }).ToList()
                                     .Select(x => new Prijava() {
                                         ID = x.ID,
                                         DatumIVrijemePocinjenjaDjela = x.DatumIVrijemePocinjenjaDjela,
                                         DatumIVrijemePrijave = x.DatumIVrijemePrijave,
                                         Grad = x.Grad,
                                         Adresa = x.Adresa,
                                         Longituda = x.Longituda,
                                         Latituda = x.Latituda,
                                         TipDjelaId = x.ID
                                     });


            return Json<List<Prijava>>(prijave.ToList());
        }

        // GET api/Prijava/5
        [ResponseType(typeof(Prijava))]
        public async Task<IHttpActionResult> GetPrijava(int? id) {
            int no = Convert.ToInt32(id);
            Prijava prijava = await db.Prijava.FindAsync(no);
            if (prijava == null) {
                return NotFound();
            }

            return Ok(prijava);
        }



        //privatna metoda sa snimanje medija
        private async Task SnimiMedij(int albumId, ICollection<HttpContent> contents) {
            foreach (var content in contents) {

                // Pravimo referencu na container unutar kojeg ćemo smještati dokumente.
                // Container nosi naziv 'krimirad'
                CloudBlobContainer container = BlobHelper.GetContainer("krimirad");

                // Obzirom da se u istom containeru ne može nalaziti blob istog imena, promijenićemo ime dokumenta, ali ćemo zadržati ekstenziju.
                //string fileName = string.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(content.Headers.ContentDisposition.FileName)); // ne radi uzimanje ekstenzije

                string fileName = string.Format("{0}{1}", Guid.NewGuid(), ".jpg");

                // Pravimo referencu na blob sa generisanim imenom unutar referenciranog containera.
                CloudBlockBlob blob = container.GetBlockBlobReference(fileName);



                // Radimo upload stream-a koji dobijamo kroz HTTP na Azure blob storage.
                await blob.UploadFromStreamAsync(await content.ReadAsStreamAsync());

                Medij medij = new Medij() {
                    AlbumId = albumId,
                    Url = ConfigurationManager.AppSettings.Get("BlobStorage") + fileName
                };

                db.Medij.Add(medij);
                await db.SaveChangesAsync();
            }
        }

        [Route("PostMedij")]
        public async Task<IHttpActionResult> PostMedij() {

            try {
                if (Request.Content.IsMimeMultipartContent()) {

                    MultipartMemoryStreamProvider provider = await Request.Content.ReadAsMultipartAsync(new MultipartMemoryStreamProvider()).ContinueWith((task) => {
                        return task.Result;
                    });

                    Album album = new Album() {
                        Naziv = DateTime.Now.ToString()
                    };
                    db.Album.Add(album);
                    db.SaveChanges();
                    await SnimiMedij(album.ID, provider.Contents);
                    HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Accepted;
                    return Json(new { albumId = album.ID, poruka = "Slika/Video je spremljen" });
                }                
                return Json(new { poruka = "Nema slike/videa" });
            } catch (Exception ex) {
                HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Conflict;
                return Json(new { poruka = "Problem kod spremanja slike/videa!" });
            }

        }

        // POST api/prijava
        [ResponseType(typeof(PrijavaCreateModel))]
        [HttpPost]
        public async Task<IHttpActionResult> PostPrijava(PrijavaCreateModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
           
            var prijava = new Prijava() {
                DatumIVrijemePrijave = DateTime.Now,
                DatumIVrijemePocinjenjaDjela = model.DatumIVrijemePocinjenjaDjela, 
                TipDjelaId = model.TipDjelaId,
                Adresa = model.Adresa,
                Opstina = model.Opstina,
                Grad = model.Grad,
                Longituda = model.Longitude,
                Latituda = model.Latitude,
                AlbumId = model.AlbumId,
                Komentar = model.Komentar
            };


            try {
                db.Prijava.Add(prijava);
                await db.SaveChangesAsync();                
                return Json(new { poruka = "Uspješna prijava!" });
            } catch (Exception ex) {                
                return Json(new { poruka = ex.Message });
            }

        }

        // PUT api/prijava/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPrijava(int id, Prijava prijava) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != prijava.ID) {
                return BadRequest();
            }

            db.Entry(prijava).State = EntityState.Modified;

            try {
                await db.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!PrijavaExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/prijava/5
        [ResponseType(typeof(Prijava))]
        public async Task<IHttpActionResult> DeletePrijava(int id) {
            Prijava prijava = await db.Prijava.FindAsync(id);
            if (prijava == null) {
                return NotFound();
            }

            db.Prijava.Remove(prijava);
            await db.SaveChangesAsync();

            return Ok(prijava);
        }

        //protected override void Dispose(bool disposing) {
        //    if (disposing) {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool PrijavaExists(int id) {
            return db.Prijava.Count(e => e.ID == id) > 0;
        }

    }
}