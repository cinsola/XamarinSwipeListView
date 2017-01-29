using Android.Views;
using System;
using Xamarin.Forms.Platform.Android;
[assembly: Xamarin.Forms.ExportRenderer(typeof(XamSwipeList.SwipeList.SwipeItemView), typeof(XamSwipeList.Droid.CustomRenderer.SwipeItemRenderer))]
namespace XamSwipeList.Droid.CustomRenderer
{
	class SwipeItemRenderer : ViewRenderer
	{
		public override bool DispatchTouchEvent(MotionEvent touch)
		{
			if(TouchDispatcher.TouchingView == null && touch.ActionMasked == MotionEventActions.Down && (this.Element as SwipeList.SwipeItemView).SwipeCompleted == false)
			{
				TouchDispatcher.TouchingView = this.Element as SwipeList.SwipeItemView;
				TouchDispatcher.StartingBiasX = touch.GetX();
				TouchDispatcher.StartingBiasY = touch.GetY();
				TouchDispatcher.InitialTouch = DateTime.Now;
			}

			return base.DispatchTouchEvent(touch);
		}
	}
}