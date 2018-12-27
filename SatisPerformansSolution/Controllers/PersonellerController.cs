using SatisPeformans.Entities;
using SatisPeformans.Entities.Surrogates;
using SatisPerformans.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace SatisPerformansSolution.Controllers
{
    public class PersonellerController : Controller
    {
        // GET: Personeller 
        SatisPerformansDBEntities db = new SatisPerformansDBEntities();
        public ActionResult PersonelListesi()
        {
            return View();
        }
        public ActionResult YeniPersonel()
        {

            return View();
        }

        public ActionResult PersonelEklePopup()
        {
            return PartialView();

        }
        private Repository<Personeller> repo_personeller = new Repository<Personeller>();
        public JsonResult PostPersoneller (PersonellerSurrogate model)
        {
            try
            {
                Personeller yeniPersonel = new Personeller();
                yeniPersonel.Adi = model.Adi;
                yeniPersonel.Soyadi = model.Soyadi;
                repo_personeller.Insert(yeniPersonel);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadPersoneller([DataSourceRequest] DataSourceRequest request, int? MagazaID)
        {
            List<PersonellerSurrogate> res = null;
            try
            {
                 res = (from a in db.Personeller
                            select new PersonellerSurrogate
                            {
                                PersonelID = a.PersonelID,
                                Adi=a.Adi,
                                Soyadi=a.Soyadi,
                                MagazaID=a.MagazaID.Value
                                
                            }).Where(x=>x.MagazaID== MagazaID).ToList();
               
            }
            catch (Exception ex)
            {
               
            }
            return Json(res.ToDataSourceResult(request));
        }
       
    }
}