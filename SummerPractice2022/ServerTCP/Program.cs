using Microsoft.AspNetCore.SignalR.Client;
using ServerTCP;
using SuperSimpleTcp;
using System.Net;
using System.Text;

SignalRClient signalR;

try
{
    signalR = new SignalRClient();
    await signalR.Start();

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
    Console.ReadLine();
}

async void ClientConnected(object sender, ConnectionEventArgs e)
{
	await signalR.UpdateStatus(e.IpPort, "name", "pending");
    Console.WriteLine($"[{e.IpPort}] client connected");
}

async void ClientDisconnected(object sender, ConnectionEventArgs e)
{
	await signalR.UpdateStatus(e.IpPort, "name", "false");
	Console.WriteLine($"[{e.IpPort}] client disconnected: {e.Reason}");
}

async void DataReceived(object sender, DataReceivedEventArgs e)
{
    Console.WriteLine($"[{e.IpPort}]: {Encoding.UTF8.GetString(e.Data)}");
}
