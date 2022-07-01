using SuperSimpleTcp;
using System.Text;

try
{
    SimpleTcpClient client = new SimpleTcpClient("127.0.0.1:8000");

    string command = "";

    client.Events.Connected += Connected;
    client.Events.Disconnected += Disconnected;
    client.Events.DataReceived += DataReceived;

    client.Connect();

    while (!command.Equals("exit"))
    {
        command = Console.ReadLine();
        client.Send(command);
    }

}
catch (Exception ex) {
    Console.WriteLine(ex.Message);
}


static void Connected(object sender, ConnectionEventArgs e)
{
    Console.WriteLine($"*** Server {e.IpPort} connected");
}

static void Disconnected(object sender, ConnectionEventArgs e)
{
    Console.WriteLine($"*** Server {e.IpPort} disconnected");
}

static void DataReceived(object sender, DataReceivedEventArgs e)
{
    Console.WriteLine($"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data)}");
}