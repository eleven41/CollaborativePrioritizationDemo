using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CollaborativePrioritizationDemo.Models
{
	[DataContract]
	public class ItemModel
	{
		[DataMember(Name = "id")]
		public int ItemId { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "sortOrder")]
		public float SortOrder { get; set; }
	}

	[DataContract]
	public class ReorderItemModel
	{
		[DataMember(Name = "before")]
		public int BeforeItemId { get; set; }

		[DataMember(Name = "after")]
		public int AfterItemId { get; set; }
	}
}