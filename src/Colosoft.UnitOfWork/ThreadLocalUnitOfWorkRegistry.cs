using System.Collections.Generic;
using System.Threading;

namespace Colosoft
{
#pragma warning disable CA1001 // Types that own disposable fields should be disposable
    public class ThreadLocalUnitOfWorkRegistry : UnitOfWorkRegistryBase
#pragma warning restore CA1001 // Types that own disposable fields should be disposable
    {
        private readonly ThreadLocal<Stack<IUnitOfWork>> stack
            = new ThreadLocal<Stack<IUnitOfWork>>(() => new Stack<IUnitOfWork>());

        protected override Stack<IUnitOfWork> GetStack()
        {
            return this.stack.Value;
        }
    }
}
