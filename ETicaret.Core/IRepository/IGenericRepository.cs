using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Core.IRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /*
        Category
        Insert
        Update,Delete
        List
        Find
        GetAll
        GetSingle
        */

        public IQueryable<TEntity> GetAll();
        public IQueryable<TEntity> FindAll(int Id);
        public TEntity FindSingle(int Id);
        public TEntity Insert(TEntity entity);



    }
}
