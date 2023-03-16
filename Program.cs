using Avalonia;
using Avalonia.ReactiveUI;
using System;

namespace SpeechToText;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var appBuilder = BuildAvaloniaApp();
        var app = (App)appBuilder.Instance;
        appBuilder.StartWithClassicDesktopLifetime(args) ;
        app?.host?.Dispose();
    }
    
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
}