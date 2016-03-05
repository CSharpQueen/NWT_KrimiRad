using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity {
    public class Medij {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string Url { get; set; }

        public virtual int AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}
