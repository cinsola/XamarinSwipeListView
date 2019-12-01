using System;
using System.Collections.Generic;
using System.Text;

namespace XamSwipeListView
{
    public static class TouchDispatcher
    {
        public static XamSwipeItemView TouchingView { get; set; }
        public static float StartingBiasX { get; set; }
        public static float StartingBiasY { get; set; }
        public static DateTime InitialTouch { get; set; }
        static TouchDispatcher()
        {
            TouchingView = null;
            StartingBiasX = 0;
            StartingBiasY = 0;
        }
    }
}
