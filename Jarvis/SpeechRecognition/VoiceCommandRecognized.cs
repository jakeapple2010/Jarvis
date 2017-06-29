using System;
using System.Speech.Recognition;
using Jarvis.Ssh;

namespace Jarvis.SpeechRecognition
{
    public class VoiceCommandRecognized : IVoiceCommandRecognizedEventHandler
    {
        private readonly ISsh _sshClient;

        public VoiceCommandRecognized(ISsh sshClient)
        {
            _sshClient = sshClient;
        }

        public void eventSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            var text = e.Result.Text;
            var ipAddress = "192.168.1.160";
            Console.WriteLine("Did you say " + text);
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
            else
            {
                Console.WriteLine("I didn't understand that or it isn't a valid command. Please try again.");
            }
        }
    }
}