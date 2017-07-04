namespace Jarvis.SpeechSynthesizer
{
    public class SpeechSynthesizer : ISpeechSynthesizer
    {
        private readonly System.Speech.Synthesis.SpeechSynthesizer synthesizer;

        public SpeechSynthesizer(System.Speech.Synthesis.SpeechSynthesizer synthesizer)
        {
            this.synthesizer = synthesizer;
        }

        public void Synthesize(string text)
        {
            synthesizer.Speak(text);
        }
    }
}