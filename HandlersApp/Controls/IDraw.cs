namespace HandlersApp.Controls
{
    public interface IDraw : IView
    {
        Color DrawColor { get; }

        void Clear();

        void Save();
    }
}