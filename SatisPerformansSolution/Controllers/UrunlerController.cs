using System;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SatisPeformans.Entities;
using SatisPeformans.Entities.Surrogates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SatisPerformans.BLL;

namespace SatisPerformansSolution.Controllers
{
    public class UrunlerController : Controller
    {
        // GET: Urunler
        SatisPerformansDBEntities db = new SatisPerformansDBEntities();
        public ActionResult UrunlerListesi()
        {
            return View();
        }

        public JsonResult ReadUrunler([DataSourceRequest] DataSourceRequest request, UrunlerSurrogate model)
        {
            List<UrunlerSurrogate> res = null;
            try
            {
                res = (from a in db.Urunler
                       select new UrunlerSurrogate
                       {
                           UrunID = a.UrunID,
                           UrunAdi = a.UrunAdi,
                           
                       }).ToList();
            }
            catch (Exception ex)
            {

            }
            return Json(res.ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
        }
        private Repository<Urunler> repo_urunler = new Repository<Urunler>();
        public JsonResult PostIslemTipleri(UrunlerSurrogate surrogate)
        {

            try
            {
                if (surrogate.UrunID > 0)
                {
                    Urunler guncellenenUrun = db.Urunler.Where(x => x.UrunID == surrogate.UrunID).FirstOrDefault();
                    guncellenenUrun.UrunAdi = surrogate.UrunAdi;
                    repo_urunler.Update(guncellenenUrun);
                }
                else
                {
                    Urunler yeniUrun = new Urunler();
                    yeniUrun.UrunAdi = surrogate.UrunAdi;

                    repo_urunler.Insert(yeniUrun);
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