using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SatisPeformans.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SatisPerformansSolution.Controllers.RaporlarController
{
    public class SatisRaporlariController : GenelController
    {
        // GET: SatisRaporlari
        SatisPerformansDBEntities Cntxt = new SatisPerformansDBEntities();
        public ActionResult SatisRaporu()
        {
            return View();
        }
        public JsonResult ReadSatisRaporlari([DataSourceRequest] DataSourceRequest request,int HedefAyID,int MagazaID)
        {
            List<proc_SatisRaporu_Result> res = null;
            try
            {
                res= (List<proc_SatisRaporu_Result>)ExecuteStoredProcedure<proc_SatisRaporu_Result>("proc_SatisRaporu", HedefAyID, MagazaID);
            }
            catch (Exception ex)
            {

            }
            return Json(res.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}