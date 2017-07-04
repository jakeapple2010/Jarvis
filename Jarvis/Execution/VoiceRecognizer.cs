using System;
using Jarvis.SpeechRecognition;
using Jarvis.Ssh;

namespace Jarvis.Execution
{
    public class VoiceRecognizer : IVoiceRecognizer
    {
        private readonly IMicrophoneControl _microphone;
        private readonly ISpeechRecognizer _speechRecognizer;
        private readonly ISsh _sshClient;

        public VoiceRecognizer(IMicrophoneControl microphone, ISpeechRecognizer speechRecognizer, ISsh sshClient)
        {
            this._microphone = microphone;
            this._speechRecognizer = speechRecognizer;
            this._sshClient = sshClient;
        }

        public void BeginLoop()
        {
            while (true)
            {
                var waveFile = _microphone.Record("What command would you like to execute?");
                var text = _speechRecognizer.ConvertSpeechToText(waveFile);

                if (waveFile.Contains("exit"))
                {
                    break;
                }

                if (!text.ToLower().Contains("mark 1"))
                {
                    Console.WriteLine("I didn't understandd that or it isn't a valid command. Please try again.");
                    continue;
                }
                Console.WriteLine("Enter in the last set of Mark 1's IP");
                var ipAddress = $"192.168.1.{Console.ReadLine()}";

                while (true)
                {
                    waveFile = _microphone.Record("What command would you like to execute?");
                    text = _speechRecognizer.ConvertSpeechToText(waveFile);

                    Console.WriteLine($"Attempting to recognize command: `{text}`");

                    if (text.ToLower() == "left")
                    {
                        _sshClient.ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/left90.py");
                    }
                    else if (text.ToLower() == "left 45")
                    {
                        _sshClient.ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/left45.py");
                    }
                    else if (text.ToLower() == "right")
                    {
                        _sshClient.ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/right90.py");
                    }
                    else if (text.ToLower() == "right 45")
                    {
                        _sshClient.ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/right45.py");
                    }
                    else if (text.ToLower() == "forward")
                    {
                        _sshClient.ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/forward.py");
                    }
                    else if (text.ToLower() == "stop")
                    {
                        _sshClient.ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/stop.py");
                    }
                    else if (text.ToLower() == "reverse")
                    {
                        _sshClient.ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/reverse.py");
                    }
                    else if (text.Contains("exit"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("I didn't understand that or it isn't a valid command. Please try again.");
                    }
                }
            }
        }
    }
}