namespace FHTW.Swen1.Forum.Repositories;



public interface IRepository<T>
{
    public T? Get(object id, Session? session = null);

    public IEnumerable<T> GetAll(Session? session = null);

    public void Refresh(T obj, Session? session = null);

    public void Save(T obj, Session? session = null);

    public void Delete(T obj, Session? session = null);
}
