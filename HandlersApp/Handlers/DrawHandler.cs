#if IOS || MACCATALYST
using PlatformView = HandlersApp.Controls.DrawViewiOS;
#elif ANDROID
using PlatformView = HandlersApp.Controls.DrawViewAndroid;
#elif WINDOWS
//using PlatformView = HandlersApp.Controls.DrawViewWindows;
using PlatformView = System.Object;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0 && !IOS && !ANDROID)
using PlatformView = System.Object;
#endif
using Microsoft.Maui.Handlers;

using IDraw = HandlersApp.Controls.IDraw;
using Microsoft.Maui.Platform;
using HandlersApp.Controls;

namespace HandlersApp.Handlers
{
    public partial class DrawHandler : IDrawHandler
    {
        public static IPropertyMapper<IDraw, IDrawHandler> Mapper =
            new PropertyMapper<IDraw, IDrawHandler>(ViewHandler.ViewMapper)
            {
                [nameof(IDraw.DrawColor)] = DrawColor,
            };

        public static CommandMapper<IDraw, IDrawHandler> CommandMapper = new(ViewCommandMapper)
        {
            [nameof(IDraw.Clear)] = OnClear,
            [nameof(IDraw.Save)] = OnSave
        };

        IDraw IDrawHandler.VirtualView => VirtualView;

        PlatformView IDrawHandler.PlatformView => PlatformView;

        public DrawHandler() : base(Mapper, CommandMapper)
        {
        }

        static void DrawColor(IDrawHandler handler, IDraw draw)
        {
            handler.PlatformView.PaintColor = draw.DrawColor.ToPlatform();
        }

        protected override void ConnectHandler(PlatformView platformView)
        {
            base.ConnectHandler(platformView);
        }

        protected override void DisconnectHandler(PlatformView platformView)
        {
            PlatformView?.Dispose();

            base.DisconnectHandler(platformView);
        }

        public static void OnSave(IDrawHandler handler, IDraw draw, object? args)
        {
            handler.PlatformView?.Save();
        }

        public static void OnClear(IDrawHandler handler, IDraw draw, object? args)
        {
            handler.PlatformView?.Clear();
        }
    }
}