using System.Threading.Tasks;
using Renci.SshNet;

namespace Jarvis.Ssh
{
    public class Ssh : ISsh
    {
        public void ExecuteSshCommand(string ipAddress, string command)
        {
            new Task(() =>
                {
                    using (var client = new SshClient(ipAddress, "pi", "berry"))
                    {
                        client.Connect();
                        client.RunCommand(command);
                        client.Disconnect();
                    }
                }
            ).Start();
        }
    }
}
