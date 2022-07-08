﻿using JSONData;
using ServerTCP;
using SuperSimpleTcp;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

SignalRClient signalR;
SimpleTcpServer server;

Console.ForegroundColor = ConsoleColor.White;

Console.WriteLine(@" ____     ____     ____     __  __   ____     ____       	");
Console.WriteLine(@"/\  _`\  /\  _`\  /\  _`\  /\ \/\ \ /\  _`\  /\  _`\     	");
Console.WriteLine(@"\ \,\L\_\\ \ \L\_\\ \ \L\ \\ \ \ \ \\ \ \L\_\\ \ \L\ \   	");
Console.WriteLine(@" \/_\__ \ \ \  _\L \ \ ,  / \ \ \ \ \\ \  _\L \ \ ,  /   	");
Console.WriteLine(@"   /\ \L\ \\ \ \L\ \\ \ \\ \ \ \ \_/ \\ \ \L\ \\ \ \\ \  	");
Console.WriteLine(@"   \ `\____\\ \____/ \ \_\ \_\\ `\___/ \ \____/ \ \_\ \_\	");
Console.WriteLine(@"    \/_____/ \/___/   \/_/\/ / `\/__/   \/___/   \/_/\/ /	");


try
{
    signalR = new SignalRClient();
    server = new SimpleTcpServer("*", 8000);

	string command = "";

	signalR.ReceivedData += ReceivedData;
	server.Events.ClientConnected += ClientConnected;
	server.Events.ClientDisconnected += ClientDisconnected;
	server.Events.DataReceived += DataReceived;

	await signalR.Start();
	server.Start();

	while (!command.Equals("exit"))
	{
		Console.Write("[Server]");
		command = Console.ReadLine();
		
	}
	Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.ReadLine();
}

async void ReceivedData(string jsonData)
{
	var data = JsonSerializer.Deserialize<BaseModel>(jsonData);
	await server.SendAsync(data.IpPort, data.Data);
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
