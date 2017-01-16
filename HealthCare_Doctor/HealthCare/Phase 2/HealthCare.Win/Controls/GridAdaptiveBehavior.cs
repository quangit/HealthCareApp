using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Microsoft.Xaml.Interactivity;

namespace HealthCare.Win.Controls
{
    public class ShowDialogAction : DependencyObject, IAction
    {
        public ContentDialog Dialog
        {
            get { return (ContentDialog)GetValue(DialogProperty); }
            set { SetValue(DialogProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Dialog.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DialogProperty =
            DependencyProperty.Register("Dialog", typeof(ContentDialog), typeof(ShowDialogAction), new PropertyMetadata(null));


        public object Execute(object sender, object parameter)
        {
            var e = sender as FrameworkElement;
            if (Dialog != null && e != null)
            {
                Dialog.DataContext = e.DataContext;
            }
            Dialog?.ShowAsync();
            return null;
        }
    }

    public class GridAdaptiveBehavior : Behavior<ItemsWrapGrid>
    {
        public double Ratio
        {
            get { return (double)GetValue(RatioProperty); }
            set { SetValue(RatioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Ratio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RatioProperty = DependencyProperty.Register("Ratio", typeof(double), typeof(GridAdaptiveBehavior), new PropertyMetadata(0D));


        public double ItemMinWidth
        {
            get { return (double)GetValue(ItemMinWidthProperty); }
            set { SetValue(ItemMinWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemMinWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemMinWidthProperty = DependencyProperty.Register("ItemMinWidth", typeof(double), typeof(GridAdaptiveBehavior), new PropertyMetadata(300D, MinItemWidthChanged));


        public double ItemMaxWidth
        {
            get { return (double)GetValue(ItemMaxWidthProperty); }
            set { SetValue(ItemMaxWidthProperty, value); }
        }


        // Using a DependencyProperty as the backing store for ItemMaxWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemMaxWidthProperty = DependencyProperty.Register("ItemMaxWidth", typeof(double), typeof(GridAdaptiveBehavior), new PropertyMetadata(-1D, ItemMaxWidthChanged));


        public double ItemMaxHeight
        {
            get { return (double)GetValue(ItemMaxHeightProperty); }
            set { SetValue(ItemMaxHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemMaxHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemMaxHeightProperty = DependencyProperty.Register("ItemMaxHeight", typeof(double), typeof(GridAdaptiveBehavior), new PropertyMetadata(double.MaxValue));


        private static void ItemMaxWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void MinItemWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }


        private async Task UpdateSize()
        {
            var panel = VisualTreeHelper.GetParent(AssociatedObject) as ItemsPresenter;
            if (panel != null)
            {
            }
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (panel != null)
                {
                    var actualWidth = panel.ActualWidth - panel.Padding.Left - panel.Padding.Right;
                    var count = AssociatedObject.Children.Count;
                    if (count == 0)
                        return;
                    var i = count;
                    if (ItemMinWidth > 0)
                    {
                        i = (int)Math.Round(actualWidth / ItemMinWidth);
                    }
                    var size = actualWidth / i;
                    var j = 100;
                    while (true)
                    {
                        if (ItemMinWidth > 0)
                            if (size < ItemMinWidth && i > 1)
                            {
                                i--;
                            }
                            else
                            {
                                break;
                            }
                        else
                        {
                            
                        }
                        size = actualWidth / i;
                    }
                    AssociatedObject.MaximumRowsOrColumns = i;
                    AssociatedObject.ItemWidth = size;
                    if (Ratio > 0)
                    {
                        var height = size / Ratio;
                        AssociatedObject.ItemHeight = height > ItemMaxHeight ? ItemMaxHeight : height;
                    }
                }
            });
        }

        protected override void OnAttached()
        {
            AssociatedObject.LayoutUpdated += OnLayoutUpdated;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.LayoutUpdated -= OnLayoutUpdated;
            base.OnDetaching();
        }

        private async void OnLayoutUpdated(object sender, object e)
        {
            await UpdateSize();
        }
    }
}
