using System;
using System.Collections.Generic;
using Xamarin.Forms;
namespace XamSwipeList.SwipeList
{
	public class SwipeListView : ListView
	{
		private List<SwipeItemView> TouchedElements { get; set; } = new List<SwipeItemView>();
		public void PreventXamarinBug()
		{
			foreach(var elem in TouchedElements)
			{
				elem.PristineItem();
			}

			TouchedElements = new List<SwipeItemView>();
		}

		public void AppendTouchedElement(SwipeItemView item)
		{
			TouchedElements.Add(item);
		}
	}
}
