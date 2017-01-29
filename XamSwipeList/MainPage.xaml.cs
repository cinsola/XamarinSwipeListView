using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamSwipeList.SwipeList;

namespace XamSwipeList
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			this.BindingContext = new ProjectViewModel();
		}

		private void RemoveElement(object sender, EventArgs e)
		{
			SwipeItemView itemView = (sender as Button).CommandParameter as SwipeItemView;
			swipeListView.PreventXamarinBug();
			(this.BindingContext as ProjectViewModel).ListItems.Remove((ItemClass)(itemView.BoundItem));
		}

		private void SwipeRightCompleted(object sender, object e)
		{
			var selectedItem = (ItemClass)e;
			selectedItem.LeftVisible = true;
		}

		private void UndoClick(object sender, EventArgs e)
		{
			SwipeItemView itemView = (sender as Button).CommandParameter as SwipeItemView;
			itemView.PristineItem();
		}
	}

	public class ProjectViewModel
	{
		public ObservableCollection<ItemClass> ListItems { get; set; }
		public ProjectViewModel()
		{
			ListItems = new ObservableCollection<ItemClass> {
				new ItemClass("Cristiano", "The .NET fullstack Developer", new DateTime(2016, 6, 2)),
				new ItemClass("John", "A front-end developer", new DateTime(2016, 7, 14)),
				new ItemClass("Angela", "Works with games", new DateTime(2016, 8, 1)),
				new ItemClass("Mike", "The fucking CFO", new DateTime(2016, 10, 25)),
				new ItemClass("Luke", "Does nothing at all", new DateTime(2017, 1, 9)),
				new ItemClass("Lisa", "The good girl", new DateTime(2017, 1, 9)),
				new ItemClass("Tim", "The CO-Founder", new DateTime(2016, 6, 2)),
				new ItemClass("Fiona", "Our business-woman", new DateTime(2016, 10, 15))
			};
		}
	}

	public class ItemClass : INotifyPropertyChanged
	{
		bool _leftVisible;

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string PropertyName)
		{
			if(PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(PropertyName)); }
		}
		public bool LeftVisible { get { return _leftVisible; } set { if (value != _leftVisible) { _leftVisible = value; OnPropertyChanged(nameof(LeftVisible)); } } }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime Join { get; set; }
		public ItemClass(string Name, string Description, DateTime Join)
		{
			this.Name = Name;
			this.Description = Description;
			this.Join = Join;
			this.LeftVisible = false;
		}
		
	}
}
