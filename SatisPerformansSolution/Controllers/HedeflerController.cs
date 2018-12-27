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
    public class HedeflerController : Controller
    {
        // GET: Hedefler 
        SatisPerformansDBEntities db = new SatisPerformansDBEntities();
        
        public ActionResult HedefAylar()
        {
            return View();
        }     
        public ActionResult HedeflerPopup()
        {
            return PartialView();
        }
        public JsonResult ReadHedefAylar([DataSourceRequest] DataSourceRequest request)
        {
            List<HedefAylarSurrogate> res = null;
            try
            {
                res = (from a in db.HedefAylari
                       select new HedefAylarSurrogate
                       {
                           HedefAyID = a.HedefAyID,
                           HedefAyi = a.HedefAyi,
                           HedefBaslangicTarihi = a.HedefTarihiBaslangic,
                           HedefBitisTarihi =  a.HedefTarihiBitis,
                       }).ToList();
                
            }
            catch (Exception ex)
            {

            }
            return Json(res.ToDataSourceResult(request));
        }
        public ActionResult Hedefler()
        {
            return View();
        }

        public JsonResult ReadHedefler([DataSourceRequest] DataSourceRequest request,int MagazaID)
        {
            List<HedeflerSurrogate> res = null;
            try
            {
                res = (from hedefler in db.Hedefler
                       join magazalar in db.Magazalar
                       on hedefler.MagazaID equals magazalar.MagazaID
                       join personeller in db.Personeller
                       on hedefler.PersonelID equals personeller.PersonelID
                       join urunler in db.Urunler
                       on hedefler.UrunID equals urunler.UrunID
                       join hedefaylari in db.HedefAylari
                       on hedefler.HedefAyID equals hedefaylari.HedefAyID
                       select new HedeflerSurrogate
                       {
                           PersonelID= hedefler.PersonelID,
                           UrunID= hedefler.UrunID,
                           HedefAdet= hedefler.HedefAdet,
                           HedefID= hedefler.HedefID,
                           HedefAyID= hedefler.HedefAyID,
                           PersonelAdi=personeller.Adi,
                           UrunAdi= urunler.UrunAdi,
                           HedefAyi= hedefaylari.HedefAyi,
                           MagazaID= hedefler.MagazaID
                       }).Where(x=>x.MagazaID== MagazaID).ToList();

            }
            catch (Exception ex)
            {

            }
            return Json(res.ToDataSourceResult(request));
        }

        public static int AktifAyID()
        {
            using (SatisPerformansDBEntities db = new SatisPerformansDBEntities())
            {
                return db.HedefAylari.FirstOrDefault(x => x.HedefTarihiBaslangic <= DateTime.Now && x.HedefTarihiBitis >= DateTime.Now).HedefAyID;
            }
             
        }
        private Repository<Hedefler> repo_hedefler = new Repository<Hedefler>();
        public JsonResult PostHedefler(HedeflerSurrogate surrogate)
        {

            try
            {
                if (surrogate.HedefID > 0)
                {
                    Hedefler guncellenenHedef = db.Hedefler.Where(x => x.HedefID == surrogate.HedefID).FirstOrDefault();
                    guncellenenHedef.UrunID = surrogate.UrunID;
                    guncellenenHedef.PersonelID = surrogate.PersonelID;
                    guncellenenHedef.HedefAdet = surrogate.HedefAdet;
                    guncellenenHedef.HedefAyID = surrogate.HedefAyID;
                    repo_hedefler.Update(guncellenenHedef);
                }
                else
                {
                    Hedefler yeniHedef = new Hedefler();
                    yeniHedef.PersonelID = surrogate.PersonelID;
                    yeniHedef.UrunID = surrogate.UrunID;
                    yeniHedef.HedefAdet = surrogate.HedefAdet;
                    yeniHedef.HedefAyID = surrogate.HedefAyID;
                    yeniHedef.MagazaID = surrogate.MagazaID;
                    repo_hedefler.Insert(yeniHedef);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        private Repository<HedefAylari> repo_hedefaylar = new Repository<HedefAylari>();
        public JsonResult PostHedefAylar(HedefAylarSurrogate surrogate)
        {

            try
            {
                if (surrogate.HedefAyID > 0)
                {
                    HedefAylari guncellenenHedefAy = db.HedefAylari.Where(x => x.HedefAyID == surrogate.HedefAyID).FirstOrDefault();
                    guncellenenHedefAy.HedefAyi = surrogate.HedefAyi;
                    guncellenenHedefAy.HedefTarihiBaslangic = surrogate.HedefBaslangicTarihi;
                    guncellenenHedefAy.HedefTarihiBitis = surrogate.HedefBitisTarihi;
                    repo_hedefaylar.Update(guncellenenHedefAy);
                }
                else
                {
                    HedefAylari yeniHedefAy = new HedefAylari();
                    yeniHedefAy.HedefAyi = surrogate.HedefAyi;
                    yeniHedefAy.HedefTarihiBaslangic = surrogate.HedefBaslangicTarihi;
                    yeniHedefAy.HedefTarihiBitis = surrogate.HedefBitisTarihi;
                    repo_hedefaylar.Insert(yeniHedefAy);
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