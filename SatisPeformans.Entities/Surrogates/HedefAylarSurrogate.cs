using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisPeformans.Entities.Surrogates
{
   public class HedefAylarSurrogate
    {
        public int HedefAyID { get; set; }
        public string HedefAyi { get; set; }
        public Nullable<DateTime> HedefBaslangicTarihi { get; set; }
        public Nullable<DateTime> HedefBitisTarihi { get; set; }

    }
}
