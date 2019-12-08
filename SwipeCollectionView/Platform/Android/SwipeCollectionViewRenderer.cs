using Android.Views;
using SwipeCollectionView.Platform.Shared;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SwipeCollectionView.Platform.Shared.SwipeCollectionView), typeof(SwipeCollectionView.Platform.Android.SwipeCollectionViewRenderer))]
namespace SwipeCollectionView.Platform.Android
{
    public class SwipeCollectionViewRenderer : CollectionViewRenderer
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

        private Status CurrentStatus = Status.Default;
        public SwipeCollectionViewRenderer() : base(global::Android.App.Application.Context)
        {

        }

        public override bool DispatchTouchEvent(MotionEvent touch)
        {
            if (TouchDispatcherHelper.TouchingView != null)
            {
                double currentQuota = ((touch.GetX() - TouchDispatcherHelper.StartingBiasX) / (double)this.Width);
                double currentScrollQuota = ((touch.GetY() - TouchDispatcherHelper.StartingBiasY) / (double)this.Height);
                bool isgoingBack = _lastDistance - Math.Abs(currentQuota) > 0;
                _setScrolling(currentQuota, currentScrollQuota);

                var touchedElement = TouchDispatcherHelper.TouchingView;
                switch (touch.ActionMasked)
                {
                    case MotionEventActions.Up:
                        CurrentStatus = Status.Default;
                        touchedElement.CompleteTranslation(currentQuota);
                        TouchDispatcherHelper.TouchingView = null;
                        TouchDispatcherHelper.StartingBiasX = 0;
                        TouchDispatcherHelper.StartingBiasY = 0;
                        break;
                    case MotionEventActions.Move:
                        if (Math.Abs(currentQuota) > SwipingMinimalStart)
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
                            TouchDispatcherHelper.TouchingView.PerformTranslation(currentQuota);
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
