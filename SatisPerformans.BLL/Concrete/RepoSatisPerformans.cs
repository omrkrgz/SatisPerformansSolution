using SatisPerformans.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using System.Linq.Expressions;
using System.Reflection;
using SatisPeformans.Entities;

namespace SatisPerformans.BLL.Concrete
{
    public class RepoSatisPerformans : ISatisPerformans
    {
        private static ISatisPerformans _satisperformans;
        public static ISatisPerformans satisperformans
        {
            get
            {
                if (_satisperformans == null)
                    throw new ApplicationException("BaseController.CommunitySvr: CommunitySvr is null.");
                return _satisperformans;
            }
            set { _satisperformans = value; }
        }
        public void ExecuteQuery(string storedProcedureName, List<object> parameters, bool useCache = false)
        {
            throw new NotImplementedException();
        }

        public dynamic ExecuteStoredProcedure(string storedProcedureName, List<object> parameters, bool useCache = false)
        {

            string s = "Senkron.Business.Entities." + storedProcedureName + "_Result";
            //Type myClassType = Type.GetType(s);
            //object a = Activator.CreateInstance(myClassType);
            using (SatisPerformansDBEntities context = new SatisPerformansDBEntities())
            {
                Type myType = Assembly.Load("Senkron.EntityFramework").GetType("Senkron.Business.Entities." + "AciOkulEntities");
                ConstructorInfo[] ci = myType.GetConstructors();
                object myClass = ci[0].Invoke(null);
                dynamic list = myClass.GetType().GetMethod(storedProcedureName).Invoke(myClass, parameters == null ? null : parameters.ToArray());
                if (list != null)
                {
                    if (list is sbyte
                    || list is byte
                    || list is short
                    || list is ushort
                    || list is int
                    || list is uint
                    || list is long
                    || list is ulong
                    || list is float
                    || list is double
                    || list is decimal
                    || list is Int32 || list is string)
                    {
                        return list;
                    }
                    return Enumerable.ToList(list);
                }
                return null;
            }
        }

        public dynamic ExecuteStoredProcedure(string storedProcedureName, bool useCache = false, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public DataSourceResult ExecuteStoredProcedure<TEntity>(string storedProcedureName, List<object> parameters, DataSourceRequest reg, string SortedColumn, bool useCache = false)
        {
            throw new NotImplementedException();
        }

        public TEntity ExecuteStoredProcedure<TEntity>(string storedProcedureName, bool useCache = false, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        List<TEntity> ISatisPerformans.GetAll<TEntity>(bool dbcontextRefresh, bool useCache)
        {
            throw new NotImplementedException();
        }

        IQueryable<TEntity> ISatisPerformans.GetAllAsQueryable<TEntity>(bool useCache)
        {
            throw new NotImplementedException();
        }

        TEntity ISatisPerformans.GetEntityByID<TEntity>(int id, bool dbcontextRefresh)
        {
            throw new NotImplementedException();
        }

        TEntity ISatisPerformans.GetEntitySearchBy<TEntity>(Expression<Func<TEntity, bool>> predicate, bool dbcontextRefresh)
        {
            throw new NotImplementedException();
        }

        IQueryable<TEntity> ISatisPerformans.GetQueryableSearchBy<TEntity>(Expression<Func<TEntity, bool>> predicate, bool dbcontextRefresh)
        {
            throw new NotImplementedException();
        }
    }
}
