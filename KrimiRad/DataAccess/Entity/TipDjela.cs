using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity {
    public class TipDjela {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }

        [Required]        
        public string VrstaDjela { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Opis { get; set; }

        public virtual List<Prijava> Prijava { get; set; }
    }
}
