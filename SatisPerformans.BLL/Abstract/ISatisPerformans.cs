using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SatisPerformans.BLL.Abstract
{
    public interface ISatisPerformans
    {
        List<TEntity> GetAll<TEntity>(bool dbcontextRefresh = false, bool useCache = false) where TEntity : class, INotifyPropertyChanged;
      
        IQueryable<TEntity> GetAllAsQueryable<TEntity>(bool useCache = false) where TEntity : class, System.ComponentModel.INotifyPropertyChanged;
      
        TEntity GetEntityByID<TEntity>(int id, bool dbcontextRefresh = false) where TEntity : class, INotifyPropertyChanged;
       
        IQueryable<TEntity> GetQueryableSearchBy<TEntity>(Expression<Func<TEntity, bool>> predicate, bool dbcontextRefresh = false) where TEntity : class, INotifyPropertyChanged;
      
        TEntity GetEntitySearchBy<TEntity>(Expression<Func<TEntity, bool>> predicate, bool dbcontextRefresh = false) where TEntity : class, INotifyPropertyChanged;
        dynamic ExecuteStoredProcedure(string storedProcedureName, List<object> parameters, bool useCache = false);
      
        dynamic ExecuteStoredProcedure(string storedProcedureName, bool useCache = false, params object[] parameters);
        
        void ExecuteQuery(string storedProcedureName, List<object> parameters, bool useCache = false);
       
        Kendo.Mvc.UI.DataSourceResult ExecuteStoredProcedure<TEntity>(string storedProcedureName, List<object> parameters, Kendo.Mvc.UI.DataSourceRequest reg, string SortedColumn, bool useCache = false);
       
        TEntity ExecuteStoredProcedure<TEntity>(string storedProcedureName, bool useCache = false, params object[] parameters);
    }
}
