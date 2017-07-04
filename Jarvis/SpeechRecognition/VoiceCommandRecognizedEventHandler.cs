using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Logging;
using Jarvis.Ssh;

namespace Jarvis.SpeechRecognition
{
    public class VoiceCommandRecognizedEventHandler : IVoiceCommandRecognizedEventHandler
    {
        private readonly ISsh _sshClient;
        private DateTimeOffset lastCommandExecutedAt;

        public VoiceCommandRecognizedEventHandler(ISsh sshClient)
        {
            _sshClient = sshClient;
            this.lastCommandExecutedAt = DateTimeOffset.UtcNow;
        }

        public void eventSpeechRecognized(VoiceRecognizedEvent e)
        {
            var ipAddress = "192.168.1.196";
            //Console.WriteLine("Did you say " + text);
            //Console.WriteLine($"Attempting to recognize command: `{text}`");

            if (lastCommandExecutedAt.AddSeconds(1.5) > DateTimeOffset.UtcNow)
            {
                Console.WriteLine($"Skipping duplicate command {e.Transcripts.FirstOrDefault()}");
                return;
            }

            if (e.Transcripts.Any(t => t.Contains("left 45")))
            {
                Console.WriteLine($"Executing command: left 45");
                _sshClient.ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/left45.py");
            }
            else if (e.Transcripts.Any(t => t.Contains("left")))
            {
                Console.WriteLine($"Executing command: left");
                _sshClient.ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/left90.py");
            }
            else if (e.Transcripts.Any(t => t.Contains("right 45")))
            {
                Console.WriteLine($"Executing command: right 45");
                _sshClient.ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/right45.py");
            }
            else if (e.Transcripts.Any(t => t.Contains("right")))
            {
                Console.WriteLine($"Executing command: right");
                _sshClient.ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/right90.py");
            }
            else if (e.Transcripts.Any(t => t.Contains("forward")))
            {
                Console.WriteLine($"Executing command: forward");
                _sshClient.ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/forward.py");
            }
            else if (e.Transcripts.Any(t => t.Contains("stop")))
            {
                Console.WriteLine($"Executing command: stop");
                _sshClient.ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/stop.py");
            }
            else if (e.Transcripts.Any(t => t.Contains("reverse")))
            {
                Console.WriteLine($"Executing command: reverse");
                _sshClient.ExecuteSshCommand(ipAddress, "python pythonRobot/voiceDirections/reverse.py");
            }
            else
            {
                Console.WriteLine($"Invalid command: {e.Transcripts.FirstOrDefault()}");
                return;
            }
            lastCommandExecutedAt = DateTimeOffset.UtcNow;
        }
    }
}