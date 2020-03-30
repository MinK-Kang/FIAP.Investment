﻿using System.Collections.Generic;
using System.Data;

namespace FIAP.Investment.Repositories
{
    public interface IRepository<TId, TEntity>
    {
        void Update(TEntity entity);

        void Insert(TEntity entity);

        void Delete(TId entity);

        DataTable Get(TId id);
    }
}