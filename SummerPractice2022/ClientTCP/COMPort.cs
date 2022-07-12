using ClientTCP.Interfaces;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTCP
{
    public class COMPort : ICOMport
    {
        private SerialPort _serialPort = new SerialPort();

        public COMPort()
        {}

        public void COMconnect()
        {
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
				_serialPort.PortName = ports[num];
				_serialPort.BaudRate = 115200;
				_serialPort.DataBits = 8;
				_serialPort.Parity = System.IO.Ports.Parity.None;
				_serialPort.StopBits = System.IO.Ports.StopBits.One;
				_serialPort.ReadTimeout = 1000;
				_serialPort.WriteTimeout = 1000;
				_serialPort.Open();

			}
			catch (Exception ex)
			{
				Console.WriteLine("ERROR: невозможно открыть порт:" + ex.ToString());
				return;
			}
		}

		public void COMdisconnect()
		{
			_serialPort.Close();
		}

        public void COMwrite(byte[] data, int offset, int count)
        {
            _serialPort.Write(data, offset, count);
        }
    }
}
