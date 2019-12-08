using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using SwipeCollectionView;

namespace ExampleApp
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new ProjectViewModel();
        }

        /// <summary>
        /// Example item removal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RemoveElement(object sender, EventArgs e)
        {
            var itemView = (sender as Button).CommandParameter as ItemClass;
            string removedItem = itemView.Name;
            var hasRemoved = (this.BindingContext as ProjectViewModel).ListItems.Remove(itemView);
            if(hasRemoved)
            {
                await DisplayAlert("Confirm", $"{removedItem} removed", "ok");
            }
        }

        /// <summary>
        /// Example sub-action (change the presentation when swipe is completed)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwipeRightCompleted(object sender, object e)
        {
            var selectedItem = (ItemClass)e;
            selectedItem.SavedConfirmed = true;
        }

        /// <summary>
        /// Example undo sub-action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UndoClick(object sender, EventArgs e)
        {
            var itemView = (sender as Button).CommandParameter as SwipeCollectionView.Platform.Shared.SwipeItemView;
            (itemView.BoundItem as ItemClass).SavedConfirmed = false;
            itemView.PristineItem();
        }
    }

    public class ProjectViewModel
    {
        public ObservableCollection<ItemClass> ListItems { get; set; }
        public ProjectViewModel()
        {
            ListItems = new ObservableCollection<ItemClass> {
                new ItemClass("Bacco", "A front-end developer", new DateTime(2016, 7, 14)),
                new ItemClass("Lucie", "The good girl", new DateTime(2017, 1, 9)),
                new ItemClass("Ciro", "Works too much", new DateTime(2016, 8, 1)),
                new ItemClass("Dario", "Our new CFO", new DateTime(2016, 10, 25)),
                new ItemClass("John", "Does nothing at all", new DateTime(2017, 1, 9)),
                new ItemClass("Gus", "The CO-Founder", new DateTime(2016, 6, 2)),
                new ItemClass("Angela", "The .NET fullstack Developer", new DateTime(2016, 6, 2)),
                new ItemClass("Hope", "Our business man", new DateTime(2016, 10, 15))
            };
        }
    }
}
