using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Google.Cloud.Speech.V1;
using Jarvis.Execution;
using Jarvis.SpeechRecognition;
using Jarvis.Ssh;
using NAudio.Wave;
using Ninject;

namespace Jarvis
{
    class Program
    {
        public static WaveInEvent WaveSource;
        public static WaveFileWriter WaveFile;
        private static string _waveFilePath;

        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var thing = new AsyncGoogleVoiceRecognizer(kernel.Get<IVoiceCommandRecognizedEventHandler>());
            

            thing.StreamingMicRecognizeAsync(180);

            while (true) ;



            var execute = kernel.Get<IVoiceRecognizer>();

            execute.BeginLoop();

            Console.ReadLine();
        }
    }
}
