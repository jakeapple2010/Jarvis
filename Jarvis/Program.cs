using System;
using System.IO;
using System.Linq;
using Google.Cloud.Speech.V1;
using NAudio.Wave;
using Renci.SshNet;

namespace Jarvis
{
    class Program
    {
        public static WaveInEvent WaveSource;
        public static WaveFileWriter WaveFile;
        private static string _waveFilePath;

        static void Main(string[] args)
        {
            var text = ConvertSpeechToText("Say a command like Mark 1 to control Mark 1.");

            if (text.ToLower().Contains("mark 1"))
            {
                Console.WriteLine("Enter in the last set of Mark 1's IP");
                var ipAddress = $"192.168.1.{Console.ReadLine()}";

                while (true)
                {
                    var command = ConvertSpeechToText("What command would you like to execute?");

                    Console.WriteLine(command);

                    if (command.Contains("exit"))
                    {
                        break;
                    }

                    if (command.ToLower() == "left")
                    {
                        ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/left90.py");
                    }

                    if (command.ToLower() == "left 45")
                    {
                        ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/left45.py");
                    }

                    if (command.ToLower() == "right")
                    {
                        ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/right90.py");
                    }

                    if (command.ToLower() == "right 45")
                    {
                        ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/right45.py");
                    }

                    if (command.ToLower() == "forward")
                    {
                        ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/forward.py");
                    }

                    if (command.ToLower() == "stop")
                    {
                        ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/stop.py");
                    }

                    if (command.ToLower() == "reverse")
                    {
                        ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/reverse.py");
                    }
                }
            }

            Console.WriteLine(text.ToLower());

            Console.ReadLine();
        }

        private static void ExecuteSshCommand(string ipAddress, string command)
        {
            using (var client = new SshClient(ipAddress, "pi", "berry"))
            {
                client.Connect();
                client.RunCommand(command);
                client.Disconnect();
            }
        }

        private static void Record(string message)
        {
            var thisApp = new Program();
            thisApp.StartRecording();
            Console.WriteLine(message + " Press enter to stop.");

            Console.ReadLine();
            thisApp.StopRecording();
        }


        private static string ConvertSpeechToText(string message)
        {
            Record(message);

            var speech = SpeechClient.Create();
            var response = speech.Recognize(new RecognitionConfig
            {
                SampleRateHertz = 44100,
                LanguageCode = "en"
            }, RecognitionAudio.FromFile(_waveFilePath));

            while (!response.Results.Any())
            {
                Console.WriteLine("I didn't understand that. Try again!");
                Record(message);
            }
            var speechRecognitionAlternative = response.Results.First().Alternatives.OrderBy(x => x.Confidence).FirstOrDefault();
            return speechRecognitionAlternative != null ? speechRecognitionAlternative.Transcript : string.Empty;
        }

        private void StartRecording()
        {
            WaveSource = new WaveInEvent {WaveFormat = new WaveFormat(44100, 1)};

            WaveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            WaveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            _waveFilePath = $@"{Directory.GetCurrentDirectory()}\Test0001.wav";

            if (!File.Exists(_waveFilePath))
            {
                File.Create(_waveFilePath).Close();
            }
            WaveFile = new WaveFileWriter(_waveFilePath, WaveSource.WaveFormat);

            WaveSource.StartRecording();
        }

        private void StopRecording()
        {
            WaveSource.StopRecording();
            WaveFile.Close();
        }

        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (WaveFile != null)
            {
                WaveFile.Write(e.Buffer, 0, e.BytesRecorded);
                WaveFile.Flush();
            }
        }

        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (WaveSource != null)
            {
                WaveSource.Dispose();
                WaveSource = null;
            }

            if (WaveFile != null)
            {
                WaveFile.Dispose();
                WaveFile = null;
            }
        }
    }
}
