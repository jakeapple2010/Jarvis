namespace Jarvis.SpeechRecognition
{
    public interface ISpeechRecognizer
    {
        string ConvertSpeechToText(string waveFilePath);
    }
}