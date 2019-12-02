using Android.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamSwipeListView;

[assembly: ExportRenderer(typeof(XamSwipeListView.XamSwipeListView), typeof(XamSwipeList.Droid.CustomRenderer.SwipeListRenderer))]
namespace XamSwipeList.Droid.CustomRenderer
{
    public class SwipeListRenderer : ListViewRenderer
	{
        public enum Status
        {
            StartedSwiping,
            Swiping,
            Default
        }

        public bool IsScrolling { get; set; } = false;
        public const double SwipingConfidenceBias = 0.1;
        public const double SwipingMinimalStart = 0.05;
        private double _lastX = 0;
        private double _lastY = 0;
        private double _lastDistance = 0;

        private Status CurrentStatus = SwipeListRenderer.Status.Default;
        public SwipeListRenderer() : base(Android.App.Application.Context)
        {

        }

        public override bool DispatchTouchEvent(MotionEvent touch)
		{
			if (TouchDispatcher.TouchingView != null)
			{
				double currentQuota = ((touch.GetX() - TouchDispatcher.StartingBiasX) / (double)this.Width);
                double currentScrollQuota = ((touch.GetY() - TouchDispatcher.StartingBiasY) / (double)this.Height);
                bool isgoingBack = _lastDistance - Math.Abs(currentQuota) > 0;
                _setScrolling(currentQuota, currentScrollQuota);

                var touchedElement = (TouchDispatcher.TouchingView as XamSwipeItemView);
				switch (touch.ActionMasked)
				{
					case MotionEventActions.Up:
                        CurrentStatus = Status.Default;
                        touchedElement.CompleteTranslation(currentQuota);
						TouchDispatcher.TouchingView = null;
						TouchDispatcher.StartingBiasX = 0;
						TouchDispatcher.StartingBiasY = 0;
						break;
					case MotionEventActions.Move:
                        if(Math.Abs(currentQuota) > SwipingMinimalStart)
                        {
                            CurrentStatus = Status.Swiping;
                        }
                        else
                        {
                            CurrentStatus = Status.StartedSwiping;
                        }

						if (touchedElement.SwipeCompleted == false && 
                            (CurrentStatus != Status.StartedSwiping ||
                            isgoingBack
                            ))
						{
							TouchDispatcher.TouchingView.PerformTranslation(currentQuota);
						}
						break;
				}
            }
            return CurrentStatus == Status.Swiping ? true : base.DispatchTouchEvent(touch);
		}

        private void _setScrolling(double currentQuota, double currentScrollQuota)
        {
            _lastDistance = Math.Abs(currentQuota);
            if (Math.Abs(currentQuota - _lastX) < Math.Abs(currentScrollQuota - _lastY))
            {
                IsScrolling = true;
            }
            else
            {
                IsScrolling = false;
            }
            _lastX = currentQuota;
            _lastY = currentScrollQuota;
        }
    }
}