using System;
using System.Threading.Tasks;

namespace SpeechToText.Services;

public class WhisperTranscriptionService : ITranscriptionService
{
    public async Task TranscribeAsync(IProgress<int> progress)
    {
        for (int i = 0; i <= 100; i++)
        {
            await Task.Delay(100);
            progress.Report(i);
        }  
    }
}