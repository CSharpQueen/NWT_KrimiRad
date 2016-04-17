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
using System.Configuration;

namespace KrimiRadServis.Controllers
{
    [EnableCors("*", "*", "*")]
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
            List<KorisnikViewModel> korisnici = new List<KorisnikViewModel>();
            List<string> ids = new List<string>();
            foreach (var u in db.Users) {
                korisnici.Add(new KorisnikViewModel() {
                    ImeIPrezime = u.ImeIPrezime,
                    Email = u.Email,
                    JMBG = u.JMBG,                    
                    Username = u.UserName
                });
                ids.Add(u.Id);
            }

            for (int i = 0; i < ids.Count; i++) {
                korisnici[i].TipKorisnika = userManager.GetRoles(ids[i]).FirstOrDefault();
            }

            return Json<List<KorisnikViewModel>>(korisnici);
        }

        // GET: api/Korisnik/guidId
        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            ApplicationUser applicationUser = await db.Users.FirstAsync(s => s.Id == id);
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
            user.ImeIPrezime = korisnik.ImeIPrezime;
            user.JMBG = korisnik.JMBG;
            user.Email = korisnik.Email;
            user.UserName = korisnik.Username;

            user.Roles.Remove(user.Roles.FirstOrDefault());            

            var result = await userManager.UpdateAsync(user);


            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new AppDbContext()));



            if (!roleManager.RoleExists(korisnik.TipKorisnika.ToString())) {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = korisnik.TipKorisnika.ToString();
                IdentityResult r = await roleManager.CreateAsync(role);
            }

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

            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new AppDbContext()));


            if (!roleManager.RoleExists(korisnik.TipKorisnika.ToString())) {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = korisnik.TipKorisnika.ToString();
                IdentityResult r = await roleManager.CreateAsync(role);
            }


            var result = await userManager.CreateAsync(user, korisnik.Password);
            if (result.Succeeded) {

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var callbackUrl = ConfigurationManager.AppSettings.Get("AdminSite") + "/Account/ConfirmEmail/userId=" + user.Id + "&code=" + code; 
                await userManager.SendEmailAsync(user.Id, "Potvrda registracije", "Potvrdite registraciju klikom <a href=\"" + callbackUrl + "\">ovdje</a>");

                return Json(new { poruka = "Korisnik je uspješno dodan" });
            } else return Json(new { errors = result.Errors });           
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