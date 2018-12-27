using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SatisPeformans.Entities;
using SatisPeformans.Entities.Surrogates;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SatisPerformansSolution.Controllers
{
    public class GenelController : Controller
    {
        SatisPerformansDBEntities Cntxt = new SatisPerformansDBEntities();
        public JsonResult ReadUrunler()
        {
            List<UrunlerSurrogate> res = null;
            try
            {
                res = (from a in Cntxt.Urunler
                       
                       select new UrunlerSurrogate
                       {
                           UrunID = a.UrunID,
                           UrunAdi = a.UrunAdi,

                       }).ToList();
            }
            catch (Exception ex)
            {

            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadPersoneller(int MagazaID)
        {
            List<PersonellerSurrogate> res = null;
            try
            {
                res = (from a in Cntxt.Personeller
                       where a.MagazaID== MagazaID
                       select new PersonellerSurrogate
                       
                       {
                           PersonelID = a.PersonelID,
                           Adi = a.Adi,
                           Soyadi = a.Soyadi

                       }).ToList();
            }
            catch (Exception ex)
            {
               
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ReadHedefAylar([DataSourceRequest] DataSourceRequest request)
        {
            
               var res = Cntxt.HedefAylari.Select(
                o => new
                {
                    HedefAyi = o.HedefAyi,
                    HedefAyID = o.HedefAyID
                }
                ).OrderBy(x => x.HedefAyID).ToList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadIslemKanallari([DataSourceRequest] DataSourceRequest request)
        {

            var res = Cntxt.IslemKanallari.Select(
             o => new
             {
                 IslemKanaliID = o.IslemKanaliID,
                 IslemKanali = o.IslemKanali
             }
             ).ToList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ReadSatisDurumlari([DataSourceRequest] DataSourceRequest request)
        {

            var res = Cntxt.SatisDurumlari.Select(
             o => new
             {
                 SatisDurumID = o.SatisDurumID,
                 SatisDurumu = o.SatisDurumu
             }
             ).OrderBy(x => x.SatisDurumID).ToList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadMagazalar([DataSourceRequest] DataSourceRequest request)
        {

            var res = Cntxt.Magazalar.Select(
             o => new
             {
                 MagazaID = o.MagazaID,
                 MagazaAdi = o.MagazaAdi
             }
             ).OrderBy(x => x.MagazaAdi).ToList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public List<T> ExecuteStoredProcedure<T>(string procedureName, params object[] parameterValues) where T : class
        {
            string query = string.Format("exec {0} ", procedureName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            if(parameterValues != null)
            {
                for (int i = 0; i < parameterValues.Length; i++)
                {
                    query += string.Format("@param{0}", i.ToString());
                    sqlParams.Add(new SqlParameter(string.Format("@param{0}", i.ToString()), (parameterValues[i] ?? DBNull.Value)));
                    if (i < parameterValues.Length - 1)
                        query += " ,";
                }
            }
            var resultSet = Cntxt.Database.SqlQuery<T>(query, sqlParams.ToArray());

            return resultSet.Cast<T>().ToList<T>();
        }

        public ActionResult SetSession(string key, string value)
        {
            Session[key] = value;

            return this.Json(new { success = true });
        }

    }
}