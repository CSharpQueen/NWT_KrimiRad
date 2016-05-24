﻿using System;
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
using Newtonsoft.Json;
using KrimiRadServis.Models;
using System.Web.Http.Cors;
using KrimiRadServis.Models;
namespace KrimiRadServis.Controllers
{
    [RoutePrefix("api/Statistika")]
    [EnableCors(origins: "*", headers: "*", methods: "PUT, POST, GET, DELETE, OPTIONS")]
    public class StatistikaController : ApiController
    {

        private AppDbContext db = new AppDbContext();


        [HttpGet]
        [Route("BrojDjelaPoOpstinama")]
        public IHttpActionResult BrojDjelaPoOpstinama() {
            var data = db.Prijava;
            var model = new List<BrojDjelaPoOpstinamaModel>();
            
            foreach (var line in data.GroupBy(info => info.Opstina)
                        .Select(group => new {
                            Opstina = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Opstina))
                {

                model.Add(new BrojDjelaPoOpstinamaModel() {
                    Opstina = line.Opstina,
                    Count = line.Count
                });                
            }

            return Json(model);
        }

        [HttpGet]
        [Route("BrojDjelaPoDatumuZaOpstinu")]
        public IHttpActionResult BrojDjelaPoDatumuZaOpstinu() {
            var data = db.Prijava;
            var model = new List<BrojDjelaPoDatumuZaOpstinuModel>();

            foreach (var line in data.GroupBy(info => info.DatumIVrijemePocinjenjaDjela)
                        .Select(group => new {
                            Datum = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Datum)) {

                model.Add(new BrojDjelaPoDatumuZaOpstinuModel() {
                    Datum = line.Datum.ToShortDateString(),
                    Count = line.Count
                });
            }


            return Json(model);
        }


        [HttpGet]
        [Route("PrijavePoTipovimaZaOpstinu")]
        public IHttpActionResult PrijavePoTipovimaZaOpstinu(string opstina) {

            List<Prijava> prijave = db.Prijava.Where(p => p.Opstina.Contains(opstina)).ToList();
            if (prijave == null) return Json("Ne postoje prijave za ovu opštinu");
            return Json<List<Prijava>>(prijave);
        }




        [HttpGet]
        [Route("PrijavePoOpstiniITipuDjela")]
        public async Task<IHttpActionResult> PrijavePoOpstiniITipuDjela(int id, string opstina) {
            List<Prijava> prijave = db.Prijava.Where(p => p.Opstina.Contains(opstina) && p.TipDjelaId.Equals(id)).ToList();
            if (prijave == null) return Json("Ne postoje prijave za ovu opstinu i tip djela");
            return Json<List<Prijava>>(prijave);
        }

        //get: api/Statistika/PrijavePoDatumu?datum=nekidatum
        [HttpGet]
        [Route("PrijavePoDatumu")]
        public async Task<IHttpActionResult> PrijavePoDatumu(DateTime datum) {
            List<Prijava> prijave = db.Prijava.Where(p => p.DatumIVrijemePrijave.Equals(datum)).ToList();
            if (prijave == null) return Json("Ne postoje prijave za ovaj datum.");
            return Json<List<Prijava>>(prijave);
        }

        //get: api/Statistika/PrijavePoDatumu?opstina=nekaopstina
        

        //get: api/Statistika/PrijavePoDatumu/3
        [HttpGet]
        [Route("PrijavePoTipuDjela")]
        public async Task<IHttpActionResult> PrijavePoTipuDjela(int id) {
            List<Prijava> prijave = db.Prijava.Where(p => p.TipDjelaId == id).ToList();
            if (prijave == null) return Json("Ne postoje prijave za ovaj tip djela");
            return Json<List<Prijava>>(prijave);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
