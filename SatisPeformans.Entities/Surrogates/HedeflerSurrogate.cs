using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisPeformans.Entities.Surrogates
{
    public class HedeflerSurrogate
    {
        public int HedefID { get; set; }
        public Nullable<int> PersonelID { get; set; }
        public string PersonelAdi { get; set; }
        public string HedefAyi { get; set; }
        public Nullable<int> UrunID { get; set; }
        public Nullable<int> MagazaID { get; set; }
        public string UrunAdi { get; set; }
        public Nullable<int> HedefAdet { get; set; }
        public Nullable<int> HedefAyID { get; set; }


    }
}
