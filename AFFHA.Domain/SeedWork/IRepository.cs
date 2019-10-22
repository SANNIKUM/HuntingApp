namespace AFFHA.Domain.SeedWork
{
    public interface IRepository<T> where T : IRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
