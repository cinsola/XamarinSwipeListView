using System.ComponentModel;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SwipeCollectionView.ControlledContentView), typeof(SwipeCollectionView.Android.CustomRenderer.ControlledContentRenderer))]
namespace SwipeCollectionView.Android.CustomRenderer
{
    public class ControlledContentRenderer: FrameRenderer
    {
        public ControlledContentRenderer(): base(global::Android.App.Application.Context)
        {
            EventsToggler(false, this);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "HasControl")
            {
                EventsToggler((this.Element as global::SwipeCollectionView.ControlledContentView).HasControl, this);
            }
        }

        private void EventsToggler(bool enabled, global::Android.Views.View view)
        {
            view.Enabled = enabled;
            if(view is ViewGroup)
            {
                for (int idx = 0; idx < (view as ViewGroup).ChildCount; idx++)
                {
                    EventsToggler(enabled, (view as ViewGroup).GetChildAt(idx));
                }
            }
        }
    }
}