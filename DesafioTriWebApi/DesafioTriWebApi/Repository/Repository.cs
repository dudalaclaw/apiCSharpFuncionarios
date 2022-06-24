using System;
using System.Collections.Generic;

namespace DesafioTriWebApi.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public virtual IEnumerable<TEntity> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
