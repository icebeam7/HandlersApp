using System.Data;

namespace HandlersApp.Controls
{
    public partial class Draw : View, IDraw
    {
        public static readonly BindableProperty DrawColorProperty =
            BindableProperty.Create(
                nameof(DrawColor),
                typeof(Color),
                typeof(Draw),
                Colors.Black);

        public Color DrawColor
        {
            get => (Color)GetValue(DrawColorProperty);
            set { SetValue(DrawColorProperty, value); }
        }

        public event EventHandler ClearRequested;
        public event EventHandler SaveRequested;

        public void Clear()
        {
            ClearRequested?.Invoke(this, EventArgs.Empty);
            Handler?.Invoke(nameof(IDraw.Clear));
        }

        public void Save()
        {
            SaveRequested?.Invoke(this, EventArgs.Empty);
            Handler?.Invoke(nameof(IDraw.Save));
        }
    }
}
