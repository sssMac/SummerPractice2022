using Microsoft.AspNetCore.SignalR;
using System.Text.Json.Nodes;

namespace WEB.Hubs
{
	public class MainHub : Hub
	{
		public Task SendData(string jsonData)
		{
			return Clients.Others.SendAsync("SendData", jsonData);
		}
		public Task UpdateStatus(string ip, string name, string status)
		{
			return Clients.Others.SendAsync("UpdateStatus", ip, name, status);
		}
		
	}
}
