using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Entity {
    public class Prijava {
        
        public int ID { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [DatumUnosa]
        [Display(Name = "Datum i vrijeme prijave")]
        public DateTime DatumIVrijemePrijave { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Datum i vrijeme pocinjenja djela")]
        public DateTime DatumIVrijemePocinjenjaDjela { get; set; }

        public double Longituda { get; set; }
        public double Latituda { get; set; }

        [RegularExpression(@"^(?:.*[a-z]){3,}$", ErrorMessage = "Unesite pravilan naziv grada, veći od 3 slova.")]
        public string Grad { get; set; }

        [RegularExpression(@"^(?:.*[a-z]){3,}$", ErrorMessage = "Unesite pravilan naziv opštine, veći od 3 slova.")]
        public string Opstina { get; set; }

        [StringLength(50, ErrorMessage = "Naziv adrese mora sadržati više od {2} karaktera.", MinimumLength = 3)]
        public string Adresa { get; set; }

        public bool Rijesen { get; set; }

        public int? AlbumId { get; set; }
        [ForeignKey("AlbumId")]
        public virtual Album Album { get; set; }

        public virtual int TipDjelaId { get; set; }        
        public virtual TipDjela TipDjela { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser UserLogin { get; set; }
    }
}
