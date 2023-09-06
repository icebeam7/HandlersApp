#if IOS || MACCATALYST
using PlatformView = HandlersApp.Controls.DrawViewiOS;
#elif ANDROID
using PlatformView = HandlersApp.Controls.DrawViewAndroid;
#elif WINDOWS
using PlatformView = HandlersApp.Controls.DrawViewWindows;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0 && !IOS && !ANDROID)
using PlatformView = System.Object;
#endif
using IDraw = HandlersApp.Controls.IDraw;

namespace HandlersApp.Handlers
{
    public interface IDrawHandler : IViewHandler
    {
        new IDraw VirtualView { get; }
        new PlatformView PlatformView { get; }
    }
}
