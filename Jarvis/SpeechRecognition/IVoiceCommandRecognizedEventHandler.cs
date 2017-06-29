using System.Speech.Recognition;

namespace Jarvis.SpeechRecognition
{
    internal interface IVoiceCommandRecognizedEventHandler
    {
        void eventSpeechRecognized(object sender, SpeechRecognizedEventArgs e);
    }
}