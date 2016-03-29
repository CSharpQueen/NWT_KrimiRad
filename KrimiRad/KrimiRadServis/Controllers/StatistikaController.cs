using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccess;
using DataAccess.Entity;
using System.Web.Http.Cors;
using Newtonsoft.Json;

namespace KrimiRadServis.Controllers
{
    public class StatistikaController : ApiController
    {

        private AppDbContext db = new AppDbContext();


        // GET: api/StatistikaLokacija
         [HttpGet]
        public async Task<IHttpActionResult> GetStatisikaLokacija(string opstina)
        {
            List <Prijava> nova= new List<Prijava>();
            List<Prijava> prijave = db.Prijava.ToList();
            foreach (Prijava p in prijave)
            {
                if (p.Opstina == opstina)
                    nova.Add(p);
            }

            return Json<List<Prijava>>(nova);
        }

        // GET: api/StatistikaTipDjela
        [HttpGet]
        public async Task<IHttpActionResult> GetStatisikaTipDjela(int tipDjela)
        {
            List<Prijava> nova = new List<Prijava>();
            List<Prijava> prijave = db.Prijava.ToList();
            foreach (Prijava p in prijave)
            {
                if (p.TipDjelaId==tipDjela)
                    nova.Add(p);
            }

            return Json<List<Prijava>>(nova);
        }


        // GET: api/StatistikaGrad
        [HttpGet]
        public async Task<IHttpActionResult> GetStatisikaGrad(string grad)
        {
            List<Prijava> nova = new List<Prijava>();
            List<Prijava> prijave = db.Prijava.ToList();
            foreach (Prijava p in prijave)
            {
                if (p.Grad== grad)
                    nova.Add(p);

                
            }
            
            return Json<List<Prijava>>(nova);
        }

        // GET: api/StatistikaAdresa
        [HttpGet]
        public async Task<IHttpActionResult> GetStatisikaAdresa(string adresa)
        {
              List<Prijava> nova = new List<Prijava>();
               List<Prijava> prijave = db.Prijava.ToList();
               foreach (Prijava p in prijave)
               {
                   if (p.Adresa == adresa)
                       nova.Add(p);


               }
           // List<Prijava> pr = db.Prijava.ToList().Where(p => p.Adresa == adresa);
          
            
            return Json<List<Prijava>>(nova);
        }




    }
}
