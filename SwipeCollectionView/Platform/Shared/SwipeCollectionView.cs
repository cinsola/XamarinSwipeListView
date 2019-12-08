using Xamarin.Forms;

namespace SwipeCollectionView.Platform.Shared
{
    public class SwipeCollectionView : CollectionView
    {
        public SwipeCollectionView() : base()
        {
            this.SelectionMode = SelectionMode.None;
        }
    }
}
