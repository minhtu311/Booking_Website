using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDatPhong.Data.Infrastructures
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly WebDatPhongDbContext context;
        private DbSet<TEntity> dbSet;

        public GenericRepository(WebDatPhongDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public virtual void Add(TEntity entity)
        {
            this.dbSet.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }

        public void Delete(int Id)
        {
            dbSet.Remove(dbSet.Find(Id));
        }

        public virtual IEnumerable<TEntity> Find(Func<TEntity, bool> condition)
        {
            return this.dbSet.Where(condition);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return this.dbSet.ToList();
        }

        public virtual TEntity GetById(int keys)
        {
            return this.dbSet.Find(keys);
        }

        public virtual void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
