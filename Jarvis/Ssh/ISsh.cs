namespace Jarvis.Ssh
{
    public interface ISsh
    {
        void ExecuteSshCommand(string ipAddress, string command);
    }
}
