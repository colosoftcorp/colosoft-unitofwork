using System;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft
{
    public interface IUnitOfWork : IDisposable
    {
        event EventHandler Disposing;

        void Commit();

        Task CommitAsync(CancellationToken cancellationToken);

        void Rollback();

        Task RollbackAsync(CancellationToken cancellationToken);

        void RegisterAfterCommitAction(Action action);
    }
}
