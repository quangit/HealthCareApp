using Xamarin.Forms;

namespace HealthCare.Behaviors
{
    /// <summary>
    ///     Updates text while Entry text changes
    /// </summary>
    public class TextChangedBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create<TextChangedBehavior, string>(p => p.Text, null, propertyChanged: OnTextChanged);

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void OnTextChanged(BindableObject bindable, string oldvalue, string newvalue)
        {
            (bindable as TextChangedBehavior).AssociatedObject.Text = newvalue;
        }

        protected override void OnAttach()
        {
            AssociatedObject.TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
        }

        protected override void OnDetach()
        {
            AssociatedObject.TextChanged -= OnTextChanged;
        }
    }
}