using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SMARTKID.Models.Repositories
{
    public interface ISchoolRepository<TEntity>
    {
        public TEntity Add(TEntity entity);

        public TEntity Get(int id);

        public TEntity Delete(int id);

        public TEntity Update(int id, TEntity entity);

        public IEnumerable<TEntity> List();
    }
}
