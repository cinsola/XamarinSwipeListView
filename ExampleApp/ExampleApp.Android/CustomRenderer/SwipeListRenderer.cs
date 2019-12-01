using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamSwipeListView;

[assembly: ExportRenderer(typeof(XamSwipeListView.XamSwipeListView), typeof(XamSwipeList.Droid.CustomRenderer.SwipeListRenderer))]
namespace XamSwipeList.Droid.CustomRenderer
{
    public class SwipeListRenderer : ListViewRenderer
	{
        public SwipeListRenderer() : base(Android.App.Application.Context)
        {

        }

        public override bool DispatchTouchEvent(MotionEvent touch)
		{
			if (TouchDispatcher.TouchingView != null)
			{
				double currentQuota = ((touch.GetX() - TouchDispatcher.StartingBiasX) / (double)this.Width);
				float x = touch.GetX();
				float y = touch.GetY();
				var touchedElement = (TouchDispatcher.TouchingView as XamSwipeItemView);
				switch (touch.ActionMasked)
				{
					case MotionEventActions.Up:
						touchedElement.CompleteTranslation(currentQuota);
						(this.Element as XamSwipeListView.XamSwipeListView).AppendTouchedElement(touchedElement);
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