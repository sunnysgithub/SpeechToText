using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using ReactiveUI;
using SpeechToText.Services;

namespace SpeechToText.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ITranscriptionService _transcriptionService;

    private int _progress;
    private bool _isFileSelected;
    private bool _isBusy;
    private string _selectedFile = string.Empty;

    public MainWindowViewModel(ITranscriptionService transcriptionService)
    {
        _transcriptionService = transcriptionService;
    }

    public bool IsFileSelected
    {
        get => _isFileSelected;
        set => this.RaiseAndSetIfChanged(ref _isFileSelected, value);
        
    }

    public bool IsBusy
    {
        get => _isBusy;
        set => this.RaiseAndSetIfChanged(ref _isBusy, value);
        
    }

    public int Progress
    {
        get => _progress;
        set => this.RaiseAndSetIfChanged(ref _progress, value);
    }

    public string SelectedFile
    {
        get => _selectedFile;
        set => this.RaiseAndSetIfChanged(ref _selectedFile, value);
    }

    public async Task OpenFileAsync()
    {
        if (IsBusy) return;
        
        OpenFileDialog openFileDialog = new OpenFileDialog()
        {
            AllowMultiple = false,
            Title = "Wählen Sie eine Datei"
        };
        
        openFileDialog.Filters?.Add(new FileDialogFilter() { Name = "WAV", Extensions = { "wav" }});
        
        var result = await openFileDialog.ShowAsync(new Window());

        if (result is null || result.Length == 0)
        {
            IsFileSelected = false;
            return ;
        }

        SelectedFile = result[0];
        IsFileSelected = true;
    }

    public async Task TranscribeAsync()
    {
        if (!IsFileSelected || IsBusy) return;
        
        IsBusy = true;
        var progress = new Progress<int>(value => Progress = value);
        await _transcriptionService.TranscribeAsync(progress);
        IsBusy = false;
    }
}