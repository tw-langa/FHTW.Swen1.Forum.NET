namespace FHTW.Swen1.Forum;



public interface IAtom
{
    public void BeginEdit(Session session);

    public void Save();

    public void Delete();

    public void Refresh();
}
