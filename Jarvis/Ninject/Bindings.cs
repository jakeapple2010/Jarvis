using Jarvis.Execution;
using Jarvis.SpeechRecognition;
using Jarvis.SpeechSynthesizer;
using Jarvis.Ssh;
using Ninject.Modules;

namespace Jarvis.Ninject
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ISsh>().To<Ssh.Ssh>();
            Bind<ISpeechRecognizer>().To<SpeechRecognizer>();
            Bind<IMicrophoneControl>().To<MicrophoneControl>();
            Bind<IVoiceRecognizer>().To<MicrosoftVoiceRecognizer>();
            Bind<IVoiceCommandRecognizedEventHandler>().To<VoiceCommandRecognizedEventHandler>();
        }
    }
}