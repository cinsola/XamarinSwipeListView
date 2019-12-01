using Android.Content;
using Android.Views;
using System;
using Xamarin.Forms.Platform.Android;
using XamSwipeListView;

[assembly: Xamarin.Forms.ExportRenderer(typeof(XamSwipeItemView), typeof(XamSwipeList.Droid.CustomRenderer.SwipeItemRenderer))]
namespace XamSwipeList.Droid.CustomRenderer
{
	public class SwipeItemRenderer : ViewRenderer
	{
        public SwipeItemRenderer() : base(Android.App.Application.Context)
        {

        }

        public override bool DispatchTouchEvent(MotionEvent touch)
		{
			if(TouchDispatcher.TouchingView == null && touch.ActionMasked == MotionEventActions.Down && (this.Element as XamSwipeItemView).SwipeCompleted == false)
			{
				TouchDispatcher.TouchingView = this.Element as XamSwipeItemView;
				TouchDispatcher.StartingBiasX = touch.GetX();
				TouchDispatcher.StartingBiasY = touch.GetY();
				TouchDispatcher.InitialTouch = DateTime.Now;
			}

			return base.DispatchTouchEvent(touch);
		}
	}
}