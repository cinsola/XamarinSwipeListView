using System;
using Xamarin.Forms;

namespace SwipeCollectionView.Platform.Shared
{
    public class ControlledContentView : Frame
    {
        public static readonly BindableProperty HasControlProperty;
        public event EventHandler OnControlChanged;
        public bool HasControl
        {
            get { return (bool)GetValue(HasControlProperty); }
            set
            {
                SetValue(HasControlProperty, value);
                OnControlChanged?.Invoke(value, new EventArgs());
            }
        }

        static ControlledContentView()
        {
            HasControlProperty = BindableProperty.Create(nameof(HasControl), typeof(bool), typeof(ControlledContentView), false, propertyChanged: HasControlChanged);
        }

        public ControlledContentView()
        {
            Padding = new Thickness(0);
        }

        private static void HasControlChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                bindable.SetValue(HasControlProperty, newValue);
            }
        }
    }
}
