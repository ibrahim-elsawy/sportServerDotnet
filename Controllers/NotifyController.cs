using System;
using Microsoft.AspNetCore.Mvc;
using sportServerDotnet.Hubs;

namespace sportServerDotnet.Controllers
{
	[ApiController]
	[Route("notify")]
	public class NotifyController : ControllerBase
	{
		private readonly NotfiHub _notifyUser;
		public NotifyController(NotfiHub notfiHub)
		{
			_notifyUser = notfiHub;;
		}

		[HttpPost]
		public void notifyMsg(int id)
		{
			_notifyUser.SendMessage(id);
		}
	}
}