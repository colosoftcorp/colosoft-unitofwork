namespace Colosoft
{
    public interface ITransactedUnitOfWork : IUnitOfWork
    {
        void BeginTransaction();
    }
}
