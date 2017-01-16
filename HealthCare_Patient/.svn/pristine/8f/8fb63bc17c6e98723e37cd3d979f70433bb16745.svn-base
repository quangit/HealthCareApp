using System.Windows.Input;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class ListViewCustom : ListView
    {
        public static BindableProperty ItemClickCommandProperty =
            BindableProperty.Create<ListViewCustom, ICommand>(x => x.ItemClickCommand, null);

        public ListViewCustom()
        {
            ItemTapped += OnItemTapped;
            ItemSelected += (s, e) => { SelectedItem = null; };
        }

        public ICommand ItemClickCommand
        {
            get { return (ICommand) GetValue(ItemClickCommandProperty); }
            set { SetValue(ItemClickCommandProperty, value); }
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && ItemClickCommand != null && ItemClickCommand.CanExecute(e))
            {
                ItemClickCommand.Execute(e.Item);
                SelectedItem = null;
            }
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (Common.OS == TargetPlatform.WinPhone && Parent != null)
            {
                dynamic _parent = null;
                if (Parent is Layout)
                    _parent = Parent as Layout;
                else if (Parent is ContentPage)
                    _parent = Parent as ContentPage;

                if (_parent != null)
                {
                    _parent.Padding =
                        new Thickness(_parent.Padding.Left, _parent.Padding.Top, -15, _parent.Padding.Bottom);
                }
            }
        }
    }
}