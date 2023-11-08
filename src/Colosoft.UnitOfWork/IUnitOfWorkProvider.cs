namespace Colosoft
{
    public interface IUnitOfWorkProvider
    {
        IUnitOfWork Create();

        IUnitOfWork GetCurrent(int ancestorLevel = 0);
    }
}
