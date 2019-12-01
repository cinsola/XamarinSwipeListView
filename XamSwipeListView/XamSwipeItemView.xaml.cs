using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamSwipeListView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class XamSwipeItemView : ContentView, INotifyPropertyChanged
    {
        public event EventHandler<object> SwipeLeftCompleted;
        public event EventHandler<object> SwipeRightCompleted;
        public event EventHandler<object> DismissSwipe;
        public double DismissSwipeBefore { get; set; } = 0.5;
        public uint SwipeDuration { get; set; } = 200;
        public bool ChangeOpacity { get; set; } = false;

        public static readonly BindableProperty SwipeLeftContentProperty;
        public static readonly BindableProperty SwipeRightContentProperty;
        public static readonly BindableProperty MainContentProperty;
        public static readonly BindableProperty BoundItemProperty;
        bool _isRightContentVisible;
        public bool SwipeCompleted { get; set; }
        public bool IsRightContentVisible
        {
            get { return _isRightContentVisible; }
            set
            {
                if (value != _isRightContentVisible)
                {
                    OnPropertyChanged(nameof(IsRightContentVisible)); _isRightContentVisible = value;
                }
            }
        }
        public View MainContent
        {
            get { return (View)GetValue(MainContentProperty); }
            set
            {
                SetValue(MainContentProperty, value);
            }
        }
        public View SwipeLeftContent
        {
            get { return (View)GetValue(SwipeLeftContentProperty); }
            set
            {
                SetValue(SwipeLeftContentProperty, value);
            }
        }
        public View SwipeRightContent
        {
            get { return (View)GetValue(SwipeRightContentProperty); }
            set
            {
                SetValue(SwipeRightContentProperty, value);
            }
        }

        public object _boundItem;
        public object BoundItem
        {
            get
            {
                return GetValue(BoundItemProperty);
            }
            set
            {
                SetValue(BoundItemProperty, value);
            }
        }
        public XamSwipeItemView()
        {
            InitializeComponent();
            mainContent.BindingContext = this;
            leftContent.BindingContext = this;
            rightContent.BindingContext = this;
        }

        public void PerformTranslation(double quota)
        {
            if (quota > 0)
            {
                IsRightContentVisible = true;
                if (ChangeOpacity == true) { mainContent.Opacity = 1 - Math.Abs(quota); }
            }
            else
            {
                IsRightContentVisible = false;
                if (ChangeOpacity == true) { mainContent.Opacity = 1 - Math.Abs(quota); }
            }
            mainContent.TranslationX = quota * this.Width;
        }

        public void CompleteTranslation(double quota)
        {

            if (Math.Abs(quota) >= DismissSwipeBefore)
            {
                if (quota > 0)
                {
                    IsRightContentVisible = true;
                    mainContent.TranslateTo(this.Width + 10, 0, SwipeDuration);
                    if (ChangeOpacity == true) { mainContent.FadeTo(0, SwipeDuration); }
                    if (SwipeRightCompleted != null) { SwipeRightCompleted(this, this.BoundItem); }
                }
                else
                {
                    IsRightContentVisible = false;
                    mainContent.TranslateTo(-this.Width - 10, 0, SwipeDuration);
                    if (ChangeOpacity == true) { mainContent.FadeTo(0, SwipeDuration); }
                    if (SwipeLeftCompleted != null) { SwipeLeftCompleted(this, this.BoundItem); }
                }
                SwipeCompleted = true;
            }
            else
            {
                SwipeCompleted = false;
                if (DismissSwipe != null) { DismissSwipe(this, this.BoundItem); }
                mainContent.TranslateTo(0, 0, SwipeDuration);
                if (ChangeOpacity == true) { mainContent.FadeTo(1, SwipeDuration); }
            }
        }

        public void PristineItem()
        {
            PerformTranslation(0);
            mainContent.TranslationX = 0;
            mainContent.TranslationX = 1;
            mainContent.TranslationX = 0;
            SetValue(TranslationXProperty, 0);
            this.SwipeCompleted = false;
            IsRightContentVisible = true;
        }

        static XamSwipeItemView()
        {
            MainContentProperty = BindableProperty.Create(nameof(MainContent), typeof(View), typeof(XamSwipeItemView), null, propertyChanged: MainContentChanged);
            SwipeLeftContentProperty = BindableProperty.Create(nameof(SwipeLeftContent), typeof(View), typeof(XamSwipeItemView), null, propertyChanged: SwipeLeftContentChanged);
            SwipeRightContentProperty = BindableProperty.Create(nameof(SwipeRightContent), typeof(View), typeof(XamSwipeItemView), null, propertyChanged: SwipeRightContentChanged);
            BoundItemProperty = BindableProperty.Create(nameof(BoundItem), typeof(object), typeof(XamSwipeItemView), null, propertyChanged: BoundItemChanged);
        }

        private static void BoundItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                (bindable as XamSwipeItemView).innerContent.BindingContext = newValue;
                (bindable as XamSwipeItemView).innerLeftContent.BindingContext = newValue;
                (bindable as XamSwipeItemView).innerRightContent.BindingContext = newValue;
                bindable.SetValue(BoundItemProperty, newValue);

            }
        }

        private static void MainContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                (bindable as XamSwipeItemView).innerContent.Content = (View)newValue;
                bindable.SetValue(MainContentProperty, newValue);
            }
        }
        private static void SwipeLeftContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                (bindable as XamSwipeItemView).innerLeftContent.Content = (View)newValue;
                bindable.SetValue(SwipeLeftContentProperty, newValue);
            }
        }
        private static void SwipeRightContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                (bindable as XamSwipeItemView).innerRightContent.Content = (View)newValue;
                bindable.SetValue(SwipeRightContentProperty, newValue);
            }
        }
    }
}