using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KrimiRadServis.Models {
    public class PrijavaViewModel {
        public int ID { get; set; }
        public string Opstina { get; set; }
        public string Grad { get; set; }
        public string Adresa { get; set; }
        public string Komentar { get; set; }
        public DateTime DatumIVrijemePocinjenjaDjela { get; set; }
        public string TipDjelaNaziv { get; set; }
        public double Latituda { get; set; }
        public double Longituda { get; set; }
        public bool Rijesen { get;  set; }
        public List<string> ImgUrls { get; set; }
    }
}