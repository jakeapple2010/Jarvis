using System;

namespace Jarvis.Ssh
{
    public class MockSsh : ISsh
    {
        public void ExecuteSshCommand(string ipAddress, string command)
        {
            Console.WriteLine("Sending mock command to client");
        }
    }
}