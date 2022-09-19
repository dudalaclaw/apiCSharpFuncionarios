using System;
using System.Collections.Generic;

namespace DesafioTriWebApi.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
    }
}
