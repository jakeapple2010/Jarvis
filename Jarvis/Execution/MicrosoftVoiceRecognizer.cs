using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using Jarvis.SpeechRecognition;
using SpeechRecognizer = System.Speech.Recognition.SpeechRecognizer;


namespace Jarvis.Execution
{
    class MicrosoftVoiceRecognizer : IVoiceRecognizer
    {
        public SpeechRecognizer recognizer = new SpeechRecognizer();
        public System.Speech.Synthesis.SpeechSynthesizer synth = new System.Speech.Synthesis.SpeechSynthesizer();

        readonly IVoiceCommandRecognizedEventHandler voiceCommandRecognizedEventHandler;

        public MicrosoftVoiceRecognizer(IVoiceCommandRecognizedEventHandler voiceCommandRecognizedEventHandler)
        {
            this.voiceCommandRecognizedEventHandler = voiceCommandRecognizedEventHandler;
        }


        public void BeginLoop()
        {
            
            synth.SetOutputToDefaultAudioDevice();
            Choices commands = new Choices();
            commands.Add("left", "left 45", "right", "right 45", "forward", "reverse", "stop", "turn around");
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(commands);

            Grammar g = new Grammar(grammarBuilder);
            recognizer.LoadGrammar(g);


            synth.Speak("Welcome, You may now begin commanding the robot.");
            while (true) ;
            
        }
    }
}
