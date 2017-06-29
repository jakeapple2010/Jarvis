using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech.Synthesis;



namespace Jarvis.Execution
{
    class MicrosoftVoiceExecution : IExecute
    {
        public SpeechRecognizer recognizer = new SpeechRecognizer();
        public SpeechSynthesizer synth = new SpeechSynthesizer();
        public void BeginLoop()
        {
            
            synth.SetOutputToDefaultAudioDevice();
            Choices commands = new Choices();
            commands.Add(new string[] { "left", "right", "forward", "reverse", "stop", "turn around"});
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(commands);

            Grammar g = new Grammar(grammarBuilder);
            recognizer.LoadGrammar(g);

            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(eventSpeechRecognized);

            synth.Speak("Welcome, You may now begin commanding the robot.");
            while (true) ;
            
        }

        private void eventSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Did you say " + e.Result.Text);
            synth.Speak("Did you say " + e.Result.Text);
        }
    }
}
