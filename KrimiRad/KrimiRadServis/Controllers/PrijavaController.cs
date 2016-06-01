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
                TipDjelaNaziv = prijava.TipDjela.Naziv            
            };

            if(prijava.Album != null && prijava.Album.Medij != null) {
                model.Medij = prijava.Album.Medij.Select(s => new MedijModel() { TipMedija = s.TipMedija, Url = s.Url }).ToList();
            }

            return Ok(model);
        }




        private bool IsImgFile(string ext) {
            string[] imgExtensions = {
            ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF"
            };
            return -1 != Array.IndexOf(imgExtensions, ext.ToUpperInvariant());
        }

       

        private bool IsVideoFile(string ext) {
            string[] videoExtensions = {            
            ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".OGG", ".RMA", //etc
            ".AVI", ".MP4", ".DIVX", ".WMV", //etc
            };
            return -1 != Array.IndexOf(videoExtensions, ext.ToUpperInvariant());
        }

        //privatna metoda sa snimanje medija
        private async Task SnimiMedij(int albumId, ICollection<HttpContent> contents) {

                // Obzirom da se u istom containeru ne može nalaziti blob istog imena, promijenićemo ime dokumenta, ali ćemo zadržati ekstenziju.
            foreach (var content in contents) {

                // Pravimo referencu na container unutar kojeg ćemo smještati dokumente.
                // Container nosi naziv 'krimirad'
                CloudBlobContainer container = BlobHelper.GetContainer("krimirad");
                var fileName = content.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);

                var ext = Path.GetExtension(fileName);

                TipMedija t;

                if (IsImgFile(ext)) t = TipMedija.Image;
                else if (IsVideoFile(ext)) t = TipMedija.Video;
                else throw new Exception("Nije podržan format slike/videa");

                fileName = string.Format("{0}{1}", Guid.NewGuid(), ext); // ne radi uzimanje ekstenzije

                //string fileName = string.Format("{0}{1}", Guid.NewGuid(), ".jpg");

                // Pravimo referencu na blob sa generisanim imenom unutar referenciranog containera.
                CloudBlockBlob blob = container.GetBlockBlobReference(fileName);



                // Radimo upload stream-a koji dobijamo kroz HTTP na Azure blob storage.
                await blob.UploadFromStreamAsync(await content.ReadAsStreamAsync());

                Medij medij = new Medij() {
                    AlbumId = albumId,
                    TipMedija = t,
                    Url = ConfigurationManager.AppSettings.Get("BlobStorage") + fileName
                };

                db.Medij.Add(medij);
                await db.SaveChangesAsync();
            }
        }

        [Route("PostMedij")]        
        public async Task<IHttpActionResult> PostMedij() {

            using (var transaction = db.Database.BeginTransaction()) {
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
                        transaction.Commit();
                        return Json(new { albumId = album.ID, poruka = "Slika/Video je spremljen" });
                    }
                    return Json(new { poruka = "Nema slike/videa" });
                } catch (Exception ex) {
                    transaction.Rollback();
                    HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    return Json(new { poruka = "Problem kod spremanja slike/videa!" });
                }
            }

        }

        // POST api/prijava
        [ResponseType(typeof(PrijavaCreateModel))]
        [HttpPost]
        public async Task<IHttpActionResult> PostPrijava(PrijavaCreateModel model) {            
            string secret = ConfigurationManager.AppSettings.Get("captchaSecret").ToString();
            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, model.Captcha));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);            

            if(!captchaResponse.Success) {
                return Json(new { poruka = "Captcha nije ispravna!" });
            }

            if (!ModelState.IsValid) {               
                return BadRequest(ModelState);
            }

            if (model.AlbumId == 0) model.AlbumId = null;

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
                return Json(new { poruka = "Prijava nije uspjela, pokušajte ponovo!" });
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
        public async Task<IHttpActionResult> DeletePrijava(int id) {
            Prijava prijava = await db.Prijava.FindAsync(id);
            if (prijava == null) {
                return NotFound();
            }

            db.Prijava.Remove(prijava);
            await db.SaveChangesAsync();

            return Json(new { poruka = "Prijava je obrisana!" });
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



    public class CaptchaResponse {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}