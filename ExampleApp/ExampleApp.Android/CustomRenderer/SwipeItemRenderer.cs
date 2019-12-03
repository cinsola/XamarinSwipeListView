using Android.Content;
using Android.Views;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamSwipeListView;

[assembly: Xamarin.Forms.ExportRenderer(typeof(XamSwipeItemView), typeof(XamSwipeList.Droid.CustomRenderer.SwipeItemRenderer))]
namespace XamSwipeList.Droid.CustomRenderer
{
	public class SwipeItemRenderer : ViewCellRenderer
	{
        XamSwipeItemView CellItem { get; set; }
        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            CellItem = item as XamSwipeItemView;
            Android.Views.View _view = convertView;
            if(convertView == null)
            {
                _view = base.GetCellCore(item, convertView, parent, context);
            }
            else
            {

                _view.Touch -= DispatchTouchEvent;
            }

            _view.Touch += DispatchTouchEvent;
            return _view;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCellPropertyChanged(sender, e);
        }

        private void DispatchTouchEvent(object sender, Android.Views.View.TouchEventArgs e)
        {
            if (TouchDispatcher.TouchingView == null && e.Event.ActionMasked == MotionEventActions.Down && CellItem.SwipeCompleted == false)
            {
                TouchDispatcher.TouchingView = CellItem;
                TouchDispatcher.StartingBiasX = e.Event.GetX();
                TouchDispatcher.StartingBiasY = e.Event.GetY();
                TouchDispatcher.InitialTouch = DateTime.Now;
            }
        }
    }
}