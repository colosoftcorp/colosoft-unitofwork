using System.Collections.Generic;
using System.Threading;

namespace Colosoft
{
    public class AsyncLocalUnitOfWorkRegistry : UnitOfWorkRegistryBase
    {
        private readonly AsyncLocal<Stack<IUnitOfWork>> stack = new AsyncLocal<Stack<IUnitOfWork>>();

        protected override Stack<IUnitOfWork> GetStack()
        {
            if (this.stack.Value == null)
            {
                this.stack.Value = new Stack<IUnitOfWork>();
            }

            return this.stack.Value;
        }
    }
}