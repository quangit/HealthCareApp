using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Microsoft.Xaml.Interactivity;

namespace HealthCare.Win.Controls
{
    public class OpenMenuFlyoutAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            FrameworkElement senderElement = sender as FrameworkElement;
            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);

            flyoutBase.ShowAt(senderElement);

            return null;
        }
    }
}