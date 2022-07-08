using SuperSimpleTcp;
using System.Text;
using System.IO.Ports;
using System.Text.Json;

SerialPort port;
SimpleTcpClient client;

try
{
    client = new SimpleTcpClient("127.0.0.1:8000");
    port = new SerialPort();

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
    string[] ports = SerialPort.GetPortNames();

    Console.WriteLine("Выберите порт:");
    for (int i = 0; i < ports.Length; i++)
    {
        Console.WriteLine("[" + i.ToString() + "] " + ports[i].ToString());
    }

    string n = Console.ReadLine();
    int num = int.Parse(n);
    try
    {
        port.PortName = ports[num];
        port.BaudRate = 115200;
        port.DataBits = 8;
        port.Parity = System.IO.Ports.Parity.None;
        port.StopBits = System.IO.Ports.StopBits.One;
        port.ReadTimeout = 1000;
        port.WriteTimeout = 1000;
        //port.PortName = ports[num];
        //port.BaudRate = 1200;
        //port.WriteTimeout = 1000;
        //port.Handshake = Handshake.None;
        //port.ReadTimeout = 1000;
        //port.ReceivedBytesThreshold = 64;
        //port.ReadBufferSize = 64;
        //port.RtsEnable = false;
        //port.WriteBufferSize = 64;
        //port.DataBits = 8;
        port.Open();

    }
    catch (Exception ex)
    {
        Console.WriteLine("ERROR: невозможно открыть порт:" + ex.ToString());
        return;
    }
}

void Disconnected(object sender, ConnectionEventArgs e)
{
    Console.WriteLine($"*** Server {e.IpPort} disconnected");
    port.Close();
}

void DataReceived(object sender, DataReceivedEventArgs e)
{
    Console.WriteLine($"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data)}");
    port.Write(e.Data,0, (e.Data).Length);
}