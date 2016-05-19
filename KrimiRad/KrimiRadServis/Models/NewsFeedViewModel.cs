using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KrimiRadServis.Models {
    public class NewsFeedViewModel {
        public int PrijavaId { get; set; }
        public string Opstina { get; set; }        
        public string Adresa { get; set; }
        public string TipDjela { get; set; }
        public DateTime DatumIVrijemePocinjenjaDjela { get; set; }
    }
}