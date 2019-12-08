using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwipeCollectionView.Platform.Shared
{
    public class SwipeItemView : ContentView
    {
        ContentView innerRightContent;
        ContentView innerLeftContent;
        ContentView mainContent;
        ContentView innerContent;
        ControlledContentView rightContent;
        ControlledContentView leftContent;

        public SwipeItemView()
        {
            innerLeftContent = new ContentView();
            innerRightContent = new ContentView();
            mainContent = new ContentView();
            innerContent = new ContentView();
            Grid mainGrid = new Grid();
            mainGrid.RowDefinitions = new RowDefinitionCollection();
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            mainGrid.ColumnDefinitions = new ColumnDefinitionCollection();
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            rightContent = new ControlledContentView();
            rightContent.SetValue(Grid.ColumnProperty, 0);
            rightContent.SetValue(Grid.RowProperty, 0);
            rightContent.SetBinding(ControlledContentView.IsVisibleProperty, new Binding("IsRightContentVisible"));
            rightContent.SetBinding(ControlledContentView.HasControlProperty, new Binding("HasRightControl"));

            leftContent = new ControlledContentView();
            leftContent.SetValue(Grid.ColumnProperty, 0);
            leftContent.SetValue(Grid.RowProperty, 0);
            leftContent.SetBinding(ControlledContentView.IsVisibleProperty, new Binding("IsLeftContentVisible"));
            leftContent.SetBinding(ControlledContentView.HasControlProperty, new Binding("HasLeftControl"));

            rightContent.Content = innerRightContent;
            leftContent.Content = innerLeftContent;

            mainContent.Content = innerContent;
            mainContent.SetValue(Grid.ColumnProperty, 0);
            mainContent.SetValue(Grid.RowProperty, 0);
            mainContent.BackgroundColor = Color.White;

            mainGrid.Children.Add(rightContent);
            mainGrid.Children.Add(leftContent);
            mainGrid.Children.Add(mainContent);
            Content = mainGrid;
        }

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
        bool _isLeftContentVisible;
        bool _hasRightControl;
        bool _hasLeftControl;
        public bool SwipeCompleted { get; set; }
        public bool IsRightContentVisible
        {
            get { return _isRightContentVisible; }
            set
            {
                if (value != _isRightContentVisible)
                {
                    _isRightContentVisible = value;
                    OnPropertyChanged(nameof(IsRightContentVisible));
                }
            }
        }
        public bool IsLeftContentVisible
        {
            get { return _isLeftContentVisible; }
            set
            {
                if (value != _isLeftContentVisible)
                {
                    _isLeftContentVisible = value;
                    OnPropertyChanged(nameof(IsLeftContentVisible));
                }
            }
        }


        public bool HasRightControl
        {
            get { return _hasRightControl; }
            set
            {
                if (value != _hasRightControl)
                {
                    _hasRightControl = value;
                    OnPropertyChanged(nameof(HasRightControl));
                }
            }
        }

        public bool HasLeftControl
        {
            get { return _hasLeftControl; }
            set
            {
                if (value != _hasLeftControl)
                {
                    _hasLeftControl = value;
                    OnPropertyChanged(nameof(HasLeftControl));
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

        public void PerformTranslation(double quota)
        {
            HasLeftControl = false;
            HasRightControl = false;

            if (quota == 0)
            {
                IsRightContentVisible = false;
                IsLeftContentVisible = false;
            }

            if (quota > 0)
            {
                IsRightContentVisible = true;
                IsLeftContentVisible = false;
                if (ChangeOpacity == true) { mainContent.Opacity = 1 - Math.Abs(quota); }
            }

            if (quota < 0)
            {
                IsRightContentVisible = false;
                IsLeftContentVisible = true;
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
                    IsLeftContentVisible = false;
                    HasRightControl = true;
                    HasLeftControl = false;
                    mainContent.TranslateTo(this.Width + 10, 0, SwipeDuration);
                    if (ChangeOpacity == true) { mainContent.FadeTo(0, SwipeDuration); }
                    if (SwipeRightCompleted != null) { SwipeRightCompleted(this, this.BoundItem); }
                }
                else
                {
                    IsRightContentVisible = false;
                    IsLeftContentVisible = true;
                    HasRightControl = false;
                    HasLeftControl = true;
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
            mainContent.Opacity = 1;
            SwipeCompleted = false;
            IsRightContentVisible = false;
            IsLeftContentVisible = false;
        }

        static SwipeItemView()
        {
            MainContentProperty = BindableProperty.Create(nameof(MainContent), typeof(View), typeof(SwipeItemView), null, propertyChanged: MainContentChanged);
            SwipeLeftContentProperty = BindableProperty.Create(nameof(SwipeLeftContent), typeof(View), typeof(SwipeItemView), null, propertyChanged: SwipeLeftContentChanged);
            SwipeRightContentProperty = BindableProperty.Create(nameof(SwipeRightContent), typeof(View), typeof(SwipeItemView), null, propertyChanged: SwipeRightContentChanged);
            BoundItemProperty = BindableProperty.Create(nameof(BoundItem), typeof(object), typeof(SwipeItemView), null, propertyChanged: BoundItemChanged);
        }

        private static void BoundItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                (bindable as SwipeItemView).innerContent.BindingContext = newValue;
                (bindable as SwipeItemView).innerLeftContent.BindingContext = newValue;
                (bindable as SwipeItemView).innerRightContent.BindingContext = newValue;
                bindable.SetValue(BoundItemProperty, newValue);
            }
        }

        private static void MainContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                (bindable as SwipeItemView).innerContent.Content = (View)newValue;
                bindable.SetValue(MainContentProperty, newValue);
            }
        }
        private static void SwipeLeftContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                (bindable as SwipeItemView).innerLeftContent.Content = (View)newValue;
                bindable.SetValue(SwipeLeftContentProperty, newValue);
            }
        }
        private static void SwipeRightContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                (bindable as SwipeItemView).innerRightContent.Content = (View)newValue;
                bindable.SetValue(SwipeRightContentProperty, newValue);
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext != null)
            {
                mainContent.BindingContext = this;
                leftContent.BindingContext = this;
                rightContent.BindingContext = this;
            }
        }
    }
}