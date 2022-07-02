using Microsoft.AspNetCore.SignalR;

namespace WEB.Hubs
{
	public class MainHub : Hub
	{
		public Task SendMessage(string mes)
		{
			return Clients.Others.SendAsync("Send", mes);
		}
		public Task UpdateStatus(string ip, string name, string status)
		{
			return Clients.Others.SendAsync("UpdateStatus", ip, name, status);
		}
	}
}
