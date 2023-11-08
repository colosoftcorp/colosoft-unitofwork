namespace Colosoft
{
    public class UnitOfWorkProvider : UnitOfWorkProviderBase
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        public UnitOfWorkProvider(IUnitOfWorkFactory unitOfWorkFactory, IUnitOfWorkRegistry unitOfWorkRegistry)
            : base(unitOfWorkRegistry)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        protected override IUnitOfWork CreateUnitOfWork(object parameter) =>
            this.unitOfWorkFactory.Create();
    }
}
