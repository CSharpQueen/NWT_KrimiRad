using Newtonsoft.Json;
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
        [StringLength(100, ErrorMessage = "Naziv mora biti između {2} i {1} karaktera.", MinimumLength = 6)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Unesite pravilan tip djela")]
        public string Naziv { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Stručni naziv mora biti između {2} i {1} karaktera.", MinimumLength = 6)]
        public string StrucniNaziv { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Vrsta djela mora biti između {2} i {1} karaktera.", MinimumLength = 6)]
        public string VrstaDjela { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Opis { get; set; }

        [JsonIgnore]
        public virtual List<Prijava> Prijava { get; set; }
    }
}
