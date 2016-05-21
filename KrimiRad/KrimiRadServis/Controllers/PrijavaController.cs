using DataAccess;
using DataAccess.Entity;
using KrimiRadServis.Models;
using KrimiRadServis.Providers;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
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

    [EnableCors(origins: "*", headers: "*", methods: "PUT, POST, GET, DELETE, OPTIONS")]
    [RoutePrefix("api/prijava")]
    public class PrijavaController : ApiController {
        private AppDbContext db = new AppDbContext();
        // GET api/prijava
        [ResponseType(typeof(List<PrijavaViewModel>))]
        public IHttpActionResult GetPrijava() {
            var prijave = (from p in db.Prijava
                          select new PrijavaViewModel() {
                              ID = p.ID,
                              Adresa = p.Adresa,
                              DatumIVrijemePocinjenjaDjela = p.DatumIVrijemePocinjenjaDjela,
                              Grad = p.Grad,
                              Komentar = p.Komentar,
                              Opstina = p.Opstina,
                              TipDjelaNaziv = p.TipDjela.Naziv,
                              Latituda = p.Latituda,
                              Longituda = p.Longituda,
                              Rijesen = p.Rijesen
                          }).ToList();

            return Json<List<PrijavaViewModel>>(prijave);
        }

        // GET api/Prijava/5
        [ResponseType(typeof(Prijava))]
        [HttpGet]
        public async Task<IHttpActionResult> GetPrijava(int? id) {
            int no = Convert.ToInt32(id);
            Prijava prijava = await db.Prijava.FindAsync(no);
            if (prijava == null) {
                return NotFound();
            }

            PrijavaViewModel model = new PrijavaViewModel() {
                Adresa = prijava.Adresa,
                DatumIVrijemePocinjenjaDjela = prijava.DatumIVrijemePocinjenjaDjela,
                ID = prijava.ID,
                Grad = prijava.Grad,
                Komentar = prijava.Komentar,
                Latituda = prijava.Latituda,
                Longituda = prijava.Longituda,
                Opstina = prijava.Opstina,
                Rijesen = prijava.Rijesen,
                TipDjelaNaziv = prijava.TipDjela.Naziv,
                ImgUrls = prijava.Album.Medij.Select(s => s.Url).ToList()
            };

            return Ok(model);
        }



        //privatna metoda sa snimanje medija
        private async Task SnimiMedij(int albumId, ICollection<HttpContent> contents) {

                // Obzirom da se u istom containeru ne može nalaziti blob istog imena, promijenićemo ime dokumenta, ali ćemo zadržati ekstenziju.
            foreach (var content in contents) {

                // Pravimo referencu na container unutar kojeg ćemo smještati dokumente.
                // Container nosi naziv 'krimirad'
                CloudBlobContainer container = BlobHelper.GetContainer("krimirad");
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
                Komentar = model.Komentar,
                Username = User != null ? User.Identity != null ? User.Identity.Name : null : null               
            };


            try {
                db.Prijava.Add(prijava);
                await db.SaveChangesAsync();                
                return Json(new { poruka = "Uspješna prijava!" });
            } catch (Exception ex) {                
                return Json(new { poruka = ex.Message });
            }

        }


        [Route("Rijesi")]                
        public async Task<IHttpActionResult> Rijesi(int id) {
            var prijava = db.Prijava.Find(id);
            if (prijava == null) {
                return NotFound();
            }
            prijava.Rijesen = true;
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

            return Json(new { poruka = "Slučaj je rješen!" });
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