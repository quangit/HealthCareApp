using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HealthCare.Phone.Controls
{
    public partial class MyTextBox : UserControl
    {    
        public Visibility SpinnerVisibility
        {
            get { return (Visibility)GetValue(SpinnerVisibilityProperty); }
            set { SetValue(SpinnerVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SpinnerVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpinnerVisibilityProperty =
            DependencyProperty.Register("SpinnerVisibility", typeof(Visibility), typeof(MyTextBox), new PropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback((DependencyObject s, DependencyPropertyChangedEventArgs e) =>
            {
                ((MyTextBox)s).Spinner.Visibility = (Visibility)e.NewValue;
                ((MyTextBox)s).MainTextBox.IsReadOnly = (((MyTextBox)s).Spinner.Visibility == Visibility.Visible);
                
            })));



        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Path.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(string), typeof(MyTextBox), new PropertyMetadata(null));



        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Hint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(MyTextBox), new PropertyMetadata(null, new PropertyChangedCallback((DependencyObject s, DependencyPropertyChangedEventArgs e) =>
            {
                

            })));


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MyTextBox), new PropertyMetadata(null, new PropertyChangedCallback((DependencyObject s, DependencyPropertyChangedEventArgs e) =>
            {
                var control = ((MyTextBox) s);
                control.HintTextBlock.Visibility = (!string.IsNullOrEmpty((string)e.NewValue)) ? Visibility.Collapsed : Visibility.Visible;
                control.MainTextBox.Text = (string) (e.NewValue ?? string.Empty);


            })));





        public InputScope InputScope
        {
            get { return (InputScope)GetValue(InputScopeProperty); }
            set { SetValue(InputScopeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InputScope.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputScopeProperty =
            DependencyProperty.Register("InputScope", typeof(InputScope), typeof(MyTextBox), new PropertyMetadata(null));




        public MyTextBox()
        {
            InitializeComponent();
            
        }

        private void MainTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            HintTextBlock.Visibility = (!string.IsNullOrEmpty(MainTextBox.Text)) ? Visibility.Collapsed : Visibility.Visible;
        }

        private void MainTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            MainTextBox.GotFocus -= MainTextBox_OnGotFocus;
            if (Spinner.Visibility == Visibility.Visible)
            {
            
            //    LoseFocus(sender);
               // this.Focus();
            }
            MainTextBox.GotFocus += MainTextBox_OnGotFocus;
        }
        private void LoseFocus(object sender)
        {
            var control = sender as Control;
            if (control == null)
                return;
            var isTabStop = control.IsTabStop;
            control.IsTabStop = false;
            control.IsEnabled = false;
            control.IsEnabled = true;
            control.IsTabStop = isTabStop;
        }
    }
}
