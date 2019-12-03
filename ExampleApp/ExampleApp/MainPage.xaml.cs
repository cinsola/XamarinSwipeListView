﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using XamSwipeListView;

namespace ExampleApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new ProjectViewModel();
        }

        private async void RemoveElement(object sender, EventArgs e)
        {
            var itemView = (sender as Button).CommandParameter as ItemClass;
            string tmp = itemView.Name;
            var hasRemoved = (this.BindingContext as ProjectViewModel).ListItems.Remove(itemView);
            if(hasRemoved)
            {
                await DisplayAlert("Confirm", $"{tmp} removed", "ok");
            }
        }

        private void SwipeRightCompleted(object sender, object e)
        {
            var selectedItem = (ItemClass)e;
            selectedItem.SavedConfirmed = true;
            TouchDispatcher.TouchingView = null;
            TouchDispatcher.TouchingView = null;
        }

        private void UndoClick(object sender, EventArgs e)
        {
            var itemView = (sender as Button).CommandParameter as XamSwipeItemView;
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
                new ItemClass("1Angela", "The .NET fullstack Developer", new DateTime(2016, 6, 2)),
                new ItemClass("2Bacco", "A front-end developer", new DateTime(2016, 7, 14)),
                //new ItemClass("3Ciro", "Works with games", new DateTime(2016, 8, 1)),
                //new ItemClass("4Dario", "The fucking CFO", new DateTime(2016, 10, 25)),
                //new ItemClass("5Elmno", "Does nothing at all", new DateTime(2017, 1, 9)),
                //new ItemClass("6Francis", "The good girl", new DateTime(2017, 1, 9)),
                //new ItemClass("7Gus", "The CO-Founder", new DateTime(2016, 6, 2)),
                new ItemClass("8Hope", "Our business-woman", new DateTime(2016, 10, 15))
            };
        }
    }

    public class ItemClass : INotifyPropertyChanged
    {
        bool _leftVisible;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(PropertyName)); }
        }
        public bool SavedConfirmed { get { return _leftVisible; } set { if (value != _leftVisible) { _leftVisible = value; OnPropertyChanged(nameof(SavedConfirmed)); } } }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Join { get; set; }
        public ItemClass(string Name, string Description, DateTime Join)
        {
            this.Name = Name;
            this.Description = Description;
            this.Join = Join;
            this.SavedConfirmed = false;
        }

    }
}
