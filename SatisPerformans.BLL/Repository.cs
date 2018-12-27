using SatisPeformans.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SatisPerformans.BLL
{
    public class Repository<T> where T : class
    {
        SatisPerformansDBEntities db = new SatisPerformansDBEntities();

        public List<T> List()
        {
            return db.Set<T>().ToList();
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public int Insert(T obj)
        {
            db.Set<T>().Add(obj);
            return Save();
        }

        public int Update(T obj)
        {
            var x= db.Entry(obj);
            x.State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }

        public int Delete(T obj)
        {
            db.Set<T>().Remove(obj);
            return Save();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return db.Set<T>().FirstOrDefault(where);
        }
    }

}
