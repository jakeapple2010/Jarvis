using System.Speech.Recognition;

namespace Jarvis.SpeechRecognition
{
    public interface IVoiceCommandRecognizedEventHandler
    {
        void eventSpeechRecognized(VoiceRecognizedEvent e);
    }
}