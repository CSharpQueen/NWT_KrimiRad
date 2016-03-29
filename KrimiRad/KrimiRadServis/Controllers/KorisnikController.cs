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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using KrimiRadServis.Models;
using System.Web.Http.Cors;

namespace KrimiRadServis.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class KorisnikController : ApiController
    {
        private AppDbContext db = new AppDbContext();

        private UserManager<ApplicationUser> userManager;

        public KorisnikController() {            
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: api/Korisnik
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json<List<ApplicationUser>>(db.Users.ToList());
        }

        // GET: api/Korisnik/guidId
        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser);
        }


        // PUT: api/Korisnik/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(string id, KorisnikBindingModel korisnik)
        {            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.FindByIdAsync(id);
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded) return Json(new { poruka = "Korisnik je uspješno izmjenjen" });
            else return Json(new { errors = result.Errors });
        }

        // POST: api/Korisnik
        [HttpPost]
        public async Task<IHttpActionResult> Post(KorisnikBindingModel korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() {
                ImeIPrezime = korisnik.ImeIPrezime,
                JMBG = korisnik.JMBG,
                Email = korisnik.Email,
                UserName = korisnik.Username
            };

            var result = await userManager.CreateAsync(user, korisnik.Password);
            if (result.Succeeded) return Json(new { poruka = "Korisnik je uspješno dodan" });
            else return Json(new { errors = result.Errors });           
        }

        // DELETE: api/Korisnik/guidid
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded) return Json(new { poruka = "Korisnik je uspješno obrisan" });
            else return Json(new { errors = result.Errors });            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationUserExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}