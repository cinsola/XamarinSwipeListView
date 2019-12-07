using System;

namespace SwipeCollectionView
{
    public static class TouchDispatcherHelper
    {
        public static SwipeItemView TouchingView { get; set; }
        public static float StartingBiasX { get; set; }
        public static float StartingBiasY { get; set; }
        public static DateTime InitialTouch { get; set; }
        static TouchDispatcherHelper()
        {
            TouchingView = null;
            StartingBiasX = 0;
            StartingBiasY = 0;
        }
    }
}
