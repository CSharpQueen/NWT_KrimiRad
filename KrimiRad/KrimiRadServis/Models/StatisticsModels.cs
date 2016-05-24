using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KrimiRadServis.Models {

    public class BrojDjelaPoOpstinamaModel {
        public string Opstina { get; set; }
        public int Count { get; set; }
    }


    public class BrojDjelaPoDatumuZaOpstinuModel {
        public string Datum { get; set; }
        public int Count { get; set; }
    }

    public class PrijavePoTipovimaZaOpstinuModel {
        public string TipDjela { get; set; }
        public string Count { get; set; }
    }
}