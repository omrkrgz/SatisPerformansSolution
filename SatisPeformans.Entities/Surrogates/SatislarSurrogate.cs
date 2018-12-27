using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisPeformans.Entities.Surrogates
{
   public class SatislarSurrogate
    {
        public Nullable<int> SatisID { get; set; }
        public Nullable<int> PersonelID { get; set; }
        public string PersonelAdi { get; set; }
        public Nullable<int> UrunID { get; set; }
        public Nullable<int> MagazaID { get; set; }
        public Nullable<int> HedefAyID { get; set; }
        public string UrunAdi { get; set; }
        public string SatisDurumu { get; set; }
        public Nullable<int> SatisAdeti { get; set; }
        public Nullable<int> SatisDurumID { get; set; }
        public Nullable<bool> SatisGerceklestiMi { get; set; }
        public Nullable<System.DateTime> SatisTarihi { get; set; }
        public Nullable<int> IslemKanaliID { get; set; }
        public string MusteriAdiSoyadi { get; set; }
        public string MusteriNo { get; set; }
        public string MusteriTcNo { get; set; }
        public string IslemNo { get; set; }
        public string Kimlik { get; set; }
        public string Tarife { get; set; }
        public string Bundle { get; set; }
        public string Aciklama { get; set; }
    }
}
