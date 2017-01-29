using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(XamSwipeList.SwipeList.SwipeListView), typeof(XamSwipeList.Droid.CustomRenderer.SwipeListRenderer))]
namespace XamSwipeList.Droid.CustomRenderer
{
	class SwipeListRenderer : ListViewRenderer
	{
		public override bool DispatchTouchEvent(MotionEvent touch)
		{
			if (TouchDispatcher.TouchingView != null)
			{
				double currentQuota = ((touch.GetX() - TouchDispatcher.StartingBiasX) / (double)this.Width);
				float x = touch.GetX();
				float y = touch.GetY();
				SwipeList.SwipeItemView touchedElement = (TouchDispatcher.TouchingView as SwipeList.SwipeItemView);
				switch (touch.ActionMasked)
				{
					case MotionEventActions.Up:
						touchedElement.CompleteTranslation(currentQuota);
						(this.Element as SwipeList.SwipeListView).AppendTouchedElement(touchedElement);
						TouchDispatcher.TouchingView = null;
						TouchDispatcher.StartingBiasX = 0;
						TouchDispatcher.StartingBiasY = 0;
						break;
					case MotionEventActions.Move:
						if (touchedElement.SwipeCompleted == false)
						{
							TouchDispatcher.TouchingView.PerformTranslation(currentQuota);
						}
						break;
				}
			}
			return base.DispatchTouchEvent(touch);
		}
	}
}