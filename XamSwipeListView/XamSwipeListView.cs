using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace XamSwipeListView
{
    public class XamSwipeListView : ListView
    {
        public XamSwipeListView() : base()
        {
            this.SelectionMode = ListViewSelectionMode.None;
        }
    }
}
