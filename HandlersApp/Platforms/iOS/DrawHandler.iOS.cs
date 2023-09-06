using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

using HandlersApp.Controls;
using IDraw = HandlersApp.Controls.IDraw;

namespace HandlersApp.Handlers
{
    public partial class DrawHandler : ViewHandler<IDraw, DrawViewiOS>
    {
        DrawViewiOS? _drawView;

        protected override DrawViewiOS CreatePlatformView()
        {
            if (_drawView == null)
            {
                _drawView = new DrawViewiOS();
            }

            return _drawView;
        }
    }
}