using System;
using System.Threading.Tasks;

namespace SpeechToText.Services;

public interface ITranscriptionService
{
    Task TranscribeAsync(IProgress<int> progress);
}