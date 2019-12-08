using System;
using System.ComponentModel;

namespace ExampleApp
{
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
