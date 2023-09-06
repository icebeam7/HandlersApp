using Microsoft.Maui.Handlers;

using HandlersApp.Controls;
using IDraw = HandlersApp.Controls.IDraw;

namespace HandlersApp.Handlers
{
    public partial class DrawHandler : ViewHandler<IDraw, DrawViewAndroid>
    {
        DrawViewAndroid? _drawView;

        protected override DrawViewAndroid CreatePlatformView()
        {
            if (_drawView == null)
            {
                _drawView = new DrawViewAndroid(Context);
            }

            return _drawView;
        }
    }
}