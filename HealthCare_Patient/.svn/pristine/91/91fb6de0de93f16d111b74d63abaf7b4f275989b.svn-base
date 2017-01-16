using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class EntryLogin : Entry
    {
        public static readonly BindableProperty HideKeyboardProperty =
            BindableProperty.Create<EntryLogin, bool>(p => p.HideKeyboard, false, BindingMode.TwoWay, propertyChanged:
                (bindable, value, newValue) =>
                {
                    var entry = ((EntryLogin)bindable);
                    entry.Unfocus();
                });

        public Color PlaceHolderColor { get; set; }
        public double FontSize {
            get;
            set;
        }
        public bool IsLogin { get; set; }

        public bool HideKeyboard
        {
            get { return (bool)GetValue(HideKeyboardProperty); }
            set { SetValue(HideKeyboardProperty, value); }
        }
    }
}