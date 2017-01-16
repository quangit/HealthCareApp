using System.Windows;
using System.Windows.Controls;

namespace HealthCare.Phone.Controls
{
    public partial class MyPasswordBox : UserControl
    {
        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Path.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(string), typeof(MyPasswordBox), new PropertyMetadata(null));



        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Hint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(MyPasswordBox), new PropertyMetadata(null));



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MyPasswordBox), new PropertyMetadata(null));






        public MyPasswordBox()
        {
            InitializeComponent();

        }

        private void MainTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MainTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            HintTextBlock.Visibility = (!string.IsNullOrEmpty(MainTextBox.Password)) ? Visibility.Collapsed : Visibility.Visible;

        }
    }
}
