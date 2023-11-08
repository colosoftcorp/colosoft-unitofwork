using System.Collections.Generic;

namespace Colosoft
{
    public class InstanceUnitOfWorkRegistry : UnitOfWorkRegistryBase
    {
        private readonly Stack<IUnitOfWork> stack = new Stack<IUnitOfWork>();

        public InstanceUnitOfWorkRegistry()
        {
        }

        protected override Stack<IUnitOfWork> GetStack() => this.stack;
    }
}
