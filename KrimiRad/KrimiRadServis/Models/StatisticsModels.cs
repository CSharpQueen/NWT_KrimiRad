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
        public int Count { get; set; }
    }

    public class BrojDjelaPoTipuDjelaModel {
        public string TipDjela { get; set; }
        public int Count { get; set; }
    }

    public class OmjerRjesenihUPerioduModel {
        public string TipDjela { get; set; }
        public int BrojRijesenih { get; set; }
        public int BrojNerjesenih { get; set; }
    }

    public class BrojDjelaPoOpstinamaZaTipDjelaModel {
        public string Opstina { get; set; }
        public int Count { get; set; }
    }

    public class BrojDjelaPoDatumimaZaTipDjelaModel {
        public string Datum { get; set; }
        public int Count { get; set; }
    }
}