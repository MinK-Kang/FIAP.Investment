namespace FIAP.Investment.Repositories
{
    public interface IRepository<TId, TEntity>
    {
        void Update(TEntity entity);

        void Insert(TEntity entity);

        void Delete(TId entity);

        TEntity Get(TId id);
    }
}