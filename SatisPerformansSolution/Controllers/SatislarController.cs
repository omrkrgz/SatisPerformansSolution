using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SatisPeformans.Entities;
using SatisPeformans.Entities.Surrogates;
using SatisPerformans.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SatisPerformansSolution.Controllers
{
    public class SatislarController : GenelController
    {
        // GET: Satislar
        SatisPerformansDBEntities db = new SatisPerformansDBEntities();
        public ActionResult SatislarListesi()
        {
            return View();
        }
        public ActionResult SatislarPopup()
        {
            return PartialView();
        }
        public JsonResult ReadSatislar([DataSourceRequest] DataSourceRequest request, int? MagazaID , int? HedefAyID)
        {
            List<proc_SatislarListesi_Result> res = null;
            try
            {
                res = (List<proc_SatislarListesi_Result>)ExecuteStoredProcedure<proc_SatislarListesi_Result>("proc_SatislarListesi", MagazaID, HedefAyID);
            }
            catch (Exception ex)
            {

            }
            return Json(res.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private Repository<Satislar> repo_satislar = new Repository<Satislar>();
        private Repository<SatisDetaylari> repo_satisdetaylari = new Repository<SatisDetaylari>();
        public JsonResult PostSatislar(SatislarSurrogate surrogate)
        {
            
            try
            {
                HedefAylari SatisTarihi = db.HedefAylari.Where(x => x.HedefAyID == surrogate.HedefAyID).FirstOrDefault();
                bool satisTarihiKontrol = surrogate.SatisTarihi >= SatisTarihi.HedefTarihiBaslangic && surrogate.SatisTarihi <= SatisTarihi.HedefTarihiBitis;
                if (satisTarihiKontrol==false)
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

                if (surrogate.SatisID > 0)
                {
                    Satislar guncellenenSatis = db.Satislar.Where(x => x.SatisID == surrogate.SatisID).FirstOrDefault();
                    guncellenenSatis.SatisTarihi = surrogate.SatisTarihi;
                    repo_satislar.Update(guncellenenSatis);
                    SatisDetaylari guncellenenSatisDetay = db.SatisDetaylari.Where(x => x.SatisID == guncellenenSatis.SatisID).FirstOrDefault();
                    guncellenenSatisDetay.PersonelID = surrogate.PersonelID;
                    guncellenenSatisDetay.UrunID = surrogate.UrunID;
                    guncellenenSatisDetay.SatisDurumID = surrogate.SatisDurumID;
                    guncellenenSatisDetay.MagazaID = surrogate.MagazaID;
                    guncellenenSatisDetay.HedefAyID = surrogate.HedefAyID;
                    repo_satisdetaylari.Update(guncellenenSatisDetay);
                }
                else
                {
                    Satislar yeniSatis = new Satislar();
                yeniSatis.SatisTarihi = surrogate.SatisTarihi.Value;              
                repo_satislar.Insert(yeniSatis);
                SatisDetaylari yeniSatisDetay = new SatisDetaylari();
                yeniSatisDetay.SatisID = yeniSatis.SatisID;
                yeniSatisDetay.PersonelID = surrogate.PersonelID;
                yeniSatisDetay.UrunID = surrogate.UrunID;
                //yeniSatisDetay.SatisAdeti = surrogate.SatisAdeti;
                yeniSatisDetay.SatisDurumID = surrogate.SatisDurumID;
                yeniSatisDetay.MagazaID = surrogate.MagazaID;
                yeniSatisDetay.HedefAyID = surrogate.HedefAyID;
                yeniSatisDetay.Aciklama = surrogate.Aciklama;
                yeniSatisDetay.IslemKanaliID = surrogate.IslemKanaliID;
                yeniSatisDetay.MusteriAdiSoyadi = surrogate.MusteriAdiSoyadi;
                yeniSatisDetay.MusteriNo = surrogate.MusteriNo;
                yeniSatisDetay.MusteriTcNo = surrogate.MusteriTcNo;
                yeniSatisDetay.IslemNo = surrogate.IslemNo;
                    yeniSatisDetay.Tarife = surrogate.Tarife;
                yeniSatisDetay.Kimlik = surrogate.Kimlik;
                yeniSatisDetay.Bundle = surrogate.Bundle;
                repo_satisdetaylari.Insert(yeniSatisDetay);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}