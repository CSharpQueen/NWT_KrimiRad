using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KrimiRadServis.Models {
    public class KorisnikViewModel {
        public string ImeIPrezime { get; set; }
        [Required]
        public string JMBG { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }   
        public string TipKorisnika { get; set; }
    }
}