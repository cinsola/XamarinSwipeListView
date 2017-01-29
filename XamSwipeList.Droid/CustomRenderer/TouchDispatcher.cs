using System;
using XamSwipeList.SwipeList;

namespace XamSwipeList.Droid.CustomRenderer
{
	public static class TouchDispatcher
	{
		public static SwipeItemView TouchingView { get; internal set; }
		public static float StartingBiasX { get; internal set; }
		public static float StartingBiasY { get; internal set; }
		public static DateTime InitialTouch { get; internal set; }
		static TouchDispatcher()
		{
			TouchingView = null;
			StartingBiasX = 0;
			StartingBiasY = 0;
		}
	}
}