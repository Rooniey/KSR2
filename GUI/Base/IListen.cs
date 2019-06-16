namespace GUI.Base
{
    public interface IListen { }
    public interface IListen<T> : IListen
    {
        void Handle(T obj);
    }
}
