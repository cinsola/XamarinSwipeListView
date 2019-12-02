using System;
using System.Collections.Generic;
using System.ComponentModel;
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

[assembly: ExportRenderer(typeof(XamSwipeListView.ControlledContentView), typeof(XamSwipeList.Droid.CustomRenderer.ControlledContentRenderer))]
namespace XamSwipeList.Droid.CustomRenderer
{
    public class ControlledContentRenderer: FrameRenderer
    {
        public ControlledContentRenderer(): base(Android.App.Application.Context)
        {
            EventsToggler(false, this);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(XamSwipeListView.ControlledContentView.HasControl))
            {
                EventsToggler((this.Element as XamSwipeListView.ControlledContentView).HasControl, this);
            }
        }

        private void EventsToggler(bool enabled, Android.Views.View view)
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