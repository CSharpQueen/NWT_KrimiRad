using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KrimiRadServis.Models {
    public class PrijavaCreateModel {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string NazivTipaDjela { get; set; }
        public HttpPostedFileBase Slika { get; set; }
        public HttpPostedFileBase Video { get; set; }
        public string Opstina { get; set; }
    }
}