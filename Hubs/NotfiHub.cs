using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace sportServerDotnet.Hubs
{
	public class NotfiHub : Hub
	{
		public void SendMessage(int data)
		{
			Clients.All.SendAsync("68191121-64a6-4597-8608-49b348475885", data);
		}
	}
}