namespace ServerTCP.Interfaces
{
    public interface ISignalRClient
    {
        Action<string> ReceivedData { get; set; }
        Task Start();
        Task UpdateStatus(string ip, string name, string status);
    }
}