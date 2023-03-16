using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using SpeechToText.Services;
using SpeechToText.ViewModels;
using SpeechToText.Views;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace SpeechToText;

public partial class App : Application
{
    public IServiceProvider Container { get; private set; }
    public IHost host { get; private set; }
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    public override void OnFrameworkInitializationCompleted()
    {
        host = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                services.UseMicrosoftDependencyResolver();
                var resolver = Locator.CurrentMutable;
                resolver.InitializeSplat();
                resolver.InitializeReactiveUI();
                
                services.AddSingleton<ITranscriptionService, WhisperTranscriptionService>();
                services.AddTransient<MainWindow>();
                services.AddTransient<MainWindowViewModel>();
                
            })
            .Build();
        
        Container = host.Services;
        Container.UseMicrosoftDependencyResolver();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Locator.Current.GetService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
                
    }
}