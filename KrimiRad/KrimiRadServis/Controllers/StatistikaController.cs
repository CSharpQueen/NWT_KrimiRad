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
        [Route("BrojDjelaPoDatumu")]
        public IHttpActionResult BrojDjelaPoDatumu() {
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
        [Route("BrojDjelaPoTipuDjela")]
        public IHttpActionResult BrojDjelaPoTipuDjela() {
            var data = db.Prijava;
            var model = new List<BrojDjelaPoTipuDjelaModel>();

            foreach (var line in data.GroupBy(info => info.TipDjela.Naziv)
                        .Select(group => new {
                            TipDjela = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.TipDjela)) {

                model.Add(new BrojDjelaPoTipuDjelaModel() {
                    TipDjela = line.TipDjela,
                    Count = line.Count
                });
            }

            return Json(model);
        }


        [HttpGet]
        [Route("PrijavePoTipovimaZaOpstinu")]
        public IHttpActionResult PrijavePoTipovimaZaOpstinu(string opstina) {

            var data = db.Prijava.Where(p => p.Opstina.Contains(opstina));
            var model = new List<PrijavePoTipovimaZaOpstinuModel>();

            foreach (var line in data.GroupBy(info => info.TipDjela.Naziv)
                        .Select(group => new {
                            TipDjela = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.TipDjela)) {

                model.Add(new PrijavePoTipovimaZaOpstinuModel() {
                    TipDjela = line.TipDjela,
                    Count = line.Count
                });
            }


            return Json(model);
        }

        [HttpGet]
        [Route("OmjerRjesenihUPeriodu")]
        public IHttpActionResult OmjerRjesenihUPeriodu(DateTime datumOd, DateTime datumDo) {

            var data = db.Prijava.Where(p => p.DatumIVrijemePocinjenjaDjela >= datumOd && p.DatumIVrijemePocinjenjaDjela <= datumDo).ToList();
            var model = new List<OmjerRjesenihUPerioduModel>();

            foreach (var line in data.GroupBy(info => info.TipDjela.Naziv)
                        .Select(group => new {
                            TipDjela = group.Key,
                            BrojRijesenih = group.Where(p => p.Rijesen).Count(),
                            BrojNerjesenih = group.Where(p => !p.Rijesen).Count()
                        })
                        .OrderBy(x => x.TipDjela)) {

                model.Add(new OmjerRjesenihUPerioduModel() {
                    TipDjela = line.TipDjela,
                    BrojRijesenih = line.BrojRijesenih,
                    BrojNerjesenih = line.BrojNerjesenih
                });
            }


            return Json(model);
        }

        [HttpGet]
        [Route("BrojDjelaPoOpstinamaZaTipDjela")]
        public IHttpActionResult BrojDjelaPoOpstinamaZaTipDjela(int tipDjelaId) {
            var data = db.Prijava.Where(p => p.TipDjelaId == tipDjelaId);
            var model = new List<BrojDjelaPoOpstinamaZaTipDjelaModel>();

            foreach (var line in data.GroupBy(info => info.Opstina)
                        .Select(group => new {
                            Opstina = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Opstina)) {

                model.Add(new BrojDjelaPoOpstinamaZaTipDjelaModel() {
                    Opstina = line.Opstina,
                    Count = line.Count
                });
            }

            return Json(model);
        }


        [HttpGet]
        [Route("BrojDjelaPoDatumimaZaTipDjela")]
        public IHttpActionResult BrojDjelaPoDatumimaZaTipDjela(int tipDjelaId) {
            var data = db.Prijava.Where(p => p.TipDjelaId == tipDjelaId);
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

      




        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
