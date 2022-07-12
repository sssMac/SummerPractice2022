using SuperSimpleTcp;
using System.Text;
using System.IO.Ports;
using System.Text.Json;
using ClientTCP;
using ClientTCP.Interfaces;

ICOMport port;
SimpleTcpClient client;

try
{
    client = new SimpleTcpClient("127.0.0.1:8000");
    port = new COMPort();

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
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


void Connected(object sender, ConnectionEventArgs e)
{
    Console.WriteLine($"*** Server {e.IpPort} connected");
    port.COMconnect();
}

void Disconnected(object sender, ConnectionEventArgs e)
{
    Console.WriteLine($"*** Server {e.IpPort} disconnected");
    port.COMdisconnect();
}

void DataReceived(object sender, DataReceivedEventArgs e)
{
    Console.WriteLine($"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data)}");
    port.COMwrite(e.Data,0, (e.Data).Length);
}