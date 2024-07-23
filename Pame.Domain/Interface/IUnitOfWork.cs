namespace Pame.Domain;

public interface IUnitOfWork
{
    Task<bool> Commit();
}
