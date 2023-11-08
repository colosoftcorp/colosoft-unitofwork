using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        private readonly List<Action> afterCommitActions = new List<Action>();
        private bool isDisposed;

        public event EventHandler Disposing;

        public virtual void Commit()
        {
            this.CommitCore();

            this.RunAfterCommitActions();
        }

        public virtual async Task CommitAsync(CancellationToken cancellationToken)
        {
            await this.CommitAsyncCore(cancellationToken);
            this.RunAfterCommitActions();
        }

        public virtual void Rollback()
        {
            this.RollbackCore();
        }

        public virtual async Task RollbackAsync(CancellationToken cancellationToken)
        {
            await this.RollbackAsyncCore(cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                this.isDisposed = true;

                try
                {
                    this.OnDisposing();
                }
                catch
                {
                    if (disposing)
                    {
                        throw;
                    }
                }

                this.DisposeCore();
            }
        }

        public void RegisterAfterCommitAction(Action action)
        {
            this.afterCommitActions.Add(action);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void RunAfterCommitActions()
        {
            foreach (var action in this.afterCommitActions)
            {
                action();
            }

            this.afterCommitActions.Clear();
        }

        protected abstract void CommitCore();

        protected abstract Task CommitAsyncCore(CancellationToken cancellationToken);

        protected abstract void RollbackCore();

        protected abstract Task RollbackAsyncCore(CancellationToken cancellationToken);

        protected abstract void DisposeCore();

        protected virtual void OnDisposing()
        {
            this.Disposing?.Invoke(this, EventArgs.Empty);
        }
    }
}
