using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.Parser.MoneyMaker.EntityFramework.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        public GenericRepository(IDbContext context)
        {
            this._context = context;
        }

        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public virtual void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Entities.Add(entity);
            this._context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this._context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Entities.Add(entity);
            this._context.SaveChanges();
        }
    }
}
