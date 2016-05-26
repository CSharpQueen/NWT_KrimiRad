using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KrimiRadServis.Models {
    public class MedijModel {
        public TipMedija TipMedija { get; set; }
        public string Url { get; set; }
    }
}