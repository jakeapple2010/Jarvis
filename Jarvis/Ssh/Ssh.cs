using Renci.SshNet;

namespace Jarvis.Ssh
{
    public class Ssh : ISsh
    {
        public void ExecuteSshCommand(string ipAddress, string command)
        {
            using (var client = new SshClient(ipAddress, "pi", "berry"))
            {
                client.Connect();
                client.RunCommand(command);
                client.Disconnect();
            }
        }
    }
}
