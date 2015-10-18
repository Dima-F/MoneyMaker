namespace HandHistories.Parser.MoneyMaker.EntityFramework.Repositories
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
