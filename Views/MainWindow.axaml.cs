using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SpeechToText.ViewModels;
using Splat;

namespace SpeechToText.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = Locator.Current.GetService<MainWindowViewModel>();
#if DEBUG
        this.AttachDevTools();
#endif
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}