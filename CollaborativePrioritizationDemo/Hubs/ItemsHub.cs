using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace CollaborativePrioritizationDemo.Hubs
{
	public class ItemsHub : Hub
	{
		public static void UpdateItemSortOrder(int itemId, float sortOrder)
		{
			// Get the context for the hubs
			var context = GlobalHost.ConnectionManager.GetHubContext<ItemsHub>();
			context.Clients.All.updateItemSortOrder(itemId, sortOrder);
		}

	}
}