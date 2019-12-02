using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace XamSwipeListView
{
    public class XamSwipeListView : ListView
    {
        public XamSwipeListView()
        {
            this.SelectionMode = ListViewSelectionMode.None;
        }

        protected override void SetupContent(Cell content, int index)
        {
            base.SetupContent(content, index);
        }

        public void RemoveElement()
        {

        }

        //private List<XamSwipeItemView> TouchedElements { get; set; } = new List<XamSwipeItemView>();
        //public void PreventXamarinBug()
        //{
        //    foreach (var elem in TouchedElements)
        //    {
        //        elem.PristineItem();
        //    }

        //    TouchedElements = new List<XamSwipeItemView>();
        //}

        //public void AppendTouchedElement(XamSwipeItemView item)
        //{
        //    TouchedElements.Add(item);
        //}
    }
}
