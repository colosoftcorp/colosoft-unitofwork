using System;

namespace Colosoft
{
    public abstract class UnitOfWorkProviderBase : IUnitOfWorkProvider
    {
        private readonly IUnitOfWorkRegistry registry;

        protected UnitOfWorkProviderBase(IUnitOfWorkRegistry registry)
        {
            this.registry = registry;
        }

        public virtual IUnitOfWork Create() => this.CreateCore(null);

        protected IUnitOfWork CreateCore(object parameter)
        {
            var uow = this.CreateUnitOfWork(parameter);
            this.registry.RegisterUnitOfWork(uow);
            uow.Disposing += this.OnUnitOfWorkDisposing;
            return uow;
        }

        protected abstract IUnitOfWork CreateUnitOfWork(object parameter);

        private void OnUnitOfWorkDisposing(object sender, EventArgs e)
        {
            var uow = (IUnitOfWork)sender;
            uow.Disposing -= this.OnUnitOfWorkDisposing;

            this.registry.UnregisterUnitOfWork(uow);
        }

        public IUnitOfWork GetCurrent(int ancestorLevel = 0) =>
            this.registry.GetCurrent(ancestorLevel);
    }
}
