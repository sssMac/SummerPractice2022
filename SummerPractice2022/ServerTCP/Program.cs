using SuperSimpleTcp;
using System.Net;
using System.Text;

try
{
    SimpleTcpServer server = new SimpleTcpServer("*", 8000);

    string command = "";

    server.Events.ClientConnected += ClientConnected;
    server.Events.ClientDisconnected += ClientDisconnected;
    server.Events.DataReceived += DataReceived;

    server.Start();

    while (!command.Equals("exit"))
    {
        command = Console.ReadLine();
        server.Send("[ClientIp:Port]", command);

    }

    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

static void ClientConnected(object sender, ConnectionEventArgs e)
{
    Console.WriteLine($"[{e.IpPort}] client connected");
}

static void ClientDisconnected(object sender, ConnectionEventArgs e)
{
    Console.WriteLine($"[{e.IpPort}] client disconnected: {e.Reason}");
}

static void DataReceived(object sender, DataReceivedEventArgs e)
{
    Console.WriteLine($"[{e.IpPort}]: {Encoding.UTF8.GetString(e.Data)}");
}