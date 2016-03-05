using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity {
    public class Album {
        public int ID { get; set; }

        public string Naziv { get; set; }

        public virtual List<Medij> Medij { get; set; }
    }
}
