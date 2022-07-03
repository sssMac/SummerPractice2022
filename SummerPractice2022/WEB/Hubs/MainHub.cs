using Microsoft.AspNetCore.SignalR;
using System.Text.Json.Nodes;

namespace WEB.Hubs
{
	public class MainHub : Hub
	{
		public Task SendMessage(string ipPort, string mes)
		{
			return Clients.Others.SendAsync("SendMessage", ipPort, mes);
		}
		public Task UpdateStatus(string ip, string name, string status)
		{
			return Clients.Others.SendAsync("UpdateStatus", ip, name, status);
		}
		
	}
}
