using Microsoft.AspNetCore.SignalR.Client;
using ServerTCP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ServerTCP
{
    internal class SignalRClient : ISignalRClient
	{
		public Action<string> ReceivedData { get; set; }

		private HubConnection connection;
		public async Task Start()
		{
			connection = new HubConnectionBuilder()
						.WithUrl("https://localhost:7240/mainhub")
						.Build();
			
			connection.On<string>(
				"SendData",
				data => ReceivedData?.Invoke(data)
			);
			
			await connection.StartAsync();
		}
		public async Task UpdateStatus(string ip, string name, string status)
		{
			await connection.SendAsync("UpdateStatus", ip, name, status);
		}

	}
}
