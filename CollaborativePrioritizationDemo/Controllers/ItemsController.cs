using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using CollaborativePrioritizationDemo.Hubs;
using CollaborativePrioritizationDemo.Models;

namespace CollaborativePrioritizationDemo.Controllers
{
    public class ItemsController : ApiController
    {
		private static List<ItemModel> _items = new List<ItemModel>();

		static ItemsController()
		{
			for (int i = 0; i < 10; ++i)
			{
				_items.Add(new ItemModel()
				{
					ItemId = i,
					Name = String.Format("Item {0}", i + 1),
					SortOrder = (i + 1) * 1000
				});
			}
		}

		private IQueryable<ItemModel> SortedItems
		{
			get
			{
				return _items.OrderBy(i => i.SortOrder).AsQueryable();
			}
		}

		public IEnumerable<ItemModel> GetAllItems()
		{
			return SortedItems;
		}

		public ItemModel GetItemById(int id)
		{
			return _items.Where(i => i.ItemId == id).Single();
		}

		public void Put(int id, ReorderItemModel model)
		{
			var item = _items.Where(i => i.ItemId == id).Single();

			var beforeItem = _items.Where(i => i.ItemId == model.BeforeItemId).SingleOrDefault();
			var afterItem = _items.Where(i => i.ItemId == model.AfterItemId).SingleOrDefault();
			

			if (afterItem != null &&
				beforeItem != null)
			{
				item.SortOrder = (afterItem.SortOrder + beforeItem.SortOrder) / 2.0f;
			}
			else if (afterItem != null)
			{
				item.SortOrder = afterItem.SortOrder + 1000;
			}
			else if (beforeItem != null)
			{
				item.SortOrder = beforeItem.SortOrder / 2.0f;
				if (item.SortOrder > 1000)
					item.SortOrder = 1000;
			}

			// Notify about the update
			ItemsHub.UpdateItemSortOrder(id, item.SortOrder);
		}
    }
}
