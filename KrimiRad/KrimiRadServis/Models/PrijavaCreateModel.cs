using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KrimiRadServis.Models {
    public class PrijavaCreateModel {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Opstina { get; set; }
        public string Grad { get; set; }
        public string Adresa { get; set; }
        public DateTime DatumIVrijemePocinjenjaDjela { get; set; }        
        public int TipDjelaId { get; set; }        
        public int AlbumId { get; set; }        
    }
}