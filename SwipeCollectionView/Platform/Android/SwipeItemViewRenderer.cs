using Android.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SwipeCollectionView.Platform.Shared.SwipeItemView), typeof(SwipeCollectionView.Platform.Android.SwipeItemViewRenderer))]
namespace SwipeCollectionView.Platform.Android
{
    public class SwipeItemViewRenderer : ViewRenderer
    {
        public SwipeItemViewRenderer() : base(global::Android.App.Application.Context)
        {

        }

        Platform.Shared.SwipeItemView CellItem { get; set; }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            DispatchTouchEvent(this, ev);
            return base.OnInterceptTouchEvent(ev);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                CellItem = e.NewElement as Platform.Shared.SwipeItemView;
            }
        }

        private void DispatchTouchEvent(object sender, MotionEvent e)
        {
            if (Platform.Shared.TouchDispatcherHelper.TouchingView == null && e.ActionMasked == MotionEventActions.Down && CellItem.SwipeCompleted == false)
            {
                Platform.Shared.TouchDispatcherHelper.TouchingView = CellItem;
                Platform.Shared.TouchDispatcherHelper.StartingBiasX = e.GetX();
                Platform.Shared.TouchDispatcherHelper.StartingBiasY = e.GetY();
                Platform.Shared.TouchDispatcherHelper.InitialTouch = DateTime.Now;
            }
        }
    }
}
