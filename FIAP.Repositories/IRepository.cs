namespace FIAP.Repositories
{
    public interface IRepository<TId, TEntity>
    {
        void CreateOrUpdate(TEntity entity);

        void Delete(TId entity);

        TEntity Get(TId id);
    }
}