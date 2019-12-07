using Android.Views;
using System;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(SwipeCollectionView.SwipeItemView), typeof(SwipeCollectionView.Android.CustomRenderer.SwipeItemRenderer))]
namespace SwipeCollectionView.Android.CustomRenderer
{
    public class SwipeItemRenderer : ViewRenderer
    {
        public SwipeItemRenderer() : base(global::Android.App.Application.Context)
        {

        }
        
        SwipeItemView CellItem { get; set; }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            DispatchTouchEvent(this, ev);
            return base.OnInterceptTouchEvent(ev);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            if(e.NewElement != null)
            {
                CellItem = e.NewElement as SwipeItemView;
            }
        }

        private void DispatchTouchEvent(object sender, MotionEvent e)
        {
            if (TouchDispatcherHelper.TouchingView == null && e.ActionMasked == MotionEventActions.Down && CellItem.SwipeCompleted == false)
            {
                TouchDispatcherHelper.TouchingView = CellItem;
                TouchDispatcherHelper.StartingBiasX = e.GetX();
                TouchDispatcherHelper.StartingBiasY = e.GetY();
                TouchDispatcherHelper.InitialTouch = DateTime.Now;
            }
        }
    }
}