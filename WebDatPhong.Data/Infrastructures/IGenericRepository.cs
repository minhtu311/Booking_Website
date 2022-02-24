using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDatPhong.Data.Infrastructures
{
    public interface IGenericRepository<TEntity> where TEntity:class
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int Id);
        void Update(TEntity entity);
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int key);
        IEnumerable<TEntity> Find(Func<TEntity, bool> condition);
    }
}
