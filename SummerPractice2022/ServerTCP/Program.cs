using JSONData;
using JSONData.CammeraData;
using JSONData.Core.CommandConverters;
using JSONData.EngineData;
using JSONData.Models;
using ServerTCP;
using SuperSimpleTcp;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

SignalRClient signalR;
SimpleTcpServer server;
CommandBoardProcessor cbp;

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
	var cameraControl = JsonSerializer.Deserialize<CameraMoveControl>(data.Data);
	//var engineControl = JsonSerializer.Deserialize<EngineControl>(data.Data);
	Console.WriteLine(cameraControl.Axis);
	Console.WriteLine(cameraControl.MovDeg);

	//var control = JsonSerializer.Deserialize<EngineControl>(data.Data);
	
	CameraMoveCommand boardCommand = new CameraMoveCommand();
	boardCommand.CameraMoveDir = new List<CameraMoveControl>();
	boardCommand.CameraMoveDir.Add(cameraControl);
	//switch (data.Code)
	//{
	//	case "0x51":
	//		boardCommand = new EngineCommand();
	//		((EngineCommand)boardCommand).Engines = new List<EngineControl>();
	//		((EngineCommand)boardCommand).Engines.Add(engineControl);
	//		break;
	//	case "0x71":
	//		boardCommand = new CameraMoveCommand();
	//		((CameraMoveCommand)boardCommand).CameraMoveDir = new List<CameraMoveControl>();
	//		((CameraMoveCommand)boardCommand).CameraMoveDir.Add(cameraControl);
	//		break;
	//}

	cbp = new CommandBoardProcessor(JsonSerializer.Serialize(boardCommand));

	var cbpData = cbp.GetCommandBinPackage();

	StringBuilder hex = new StringBuilder(cbpData.Length * 2);
	foreach (byte b in cbpData)
		hex.AppendFormat(" {0:x2}", b);

	//Console.WriteLine(hex);
	await server.SendAsync(data.IpPort, cbpData);
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
