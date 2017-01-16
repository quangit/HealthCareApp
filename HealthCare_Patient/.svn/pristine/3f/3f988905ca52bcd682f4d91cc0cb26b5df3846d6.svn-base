using System.Windows.Input;
using HealthCare.Helpers;
using HealthCare.Resx;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public partial class BaseAddCheckupPage : TopBarContentPage
    {
        public BaseAddCheckupPage()
        {
            InitializeComponent();
        }

        public View PageContent
        {
            get { return idContent.Content; }
            set { idContent.Content = value; }
        }

        public string NextText
        {
            get { return btnNext.Text; }
            set { btnNext.Text = value; }
        }

        public string CancelText
        {
            get { return btnCancel.Text; }
            set { btnCancel.Text = value; }
        }

        #region PageTitle

        /// <summary>
        ///     Identifies the <see cref="PageTitle" /> bindable property.
        /// </summary>
        public static readonly BindableProperty PageTitleProperty =
            BindableProperty.Create<BaseAddCheckupPage, string>(p => p.PageTitle, default(string), BindingMode.Default,
                propertyChanged:
                    (bindable, value, newValue) =>
                    {
                        if (!string.IsNullOrWhiteSpace(newValue) && newValue.Equals(AppResources.pick_schedule) &&
                            Common.OS == TargetPlatform.iOS)
                            ((BaseAddCheckupPage) bindable).Title = " ";
                        else
                            ((BaseAddCheckupPage) bindable).Title = newValue;
                    });

        /// <summary>
        ///     Gets or sets the <see cref="PageTitle" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        /// </value>
        public string PageTitle
        {
            get { return (string) GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        #endregion

        #region CancelCommand

        /// <summary>
        ///     Identifies the <see cref="CancelCommand" /> bindable property.
        /// </summary>
        public static readonly BindableProperty CancelCommandProperty =
            BindableProperty.Create<BaseAddCheckupPage, ICommand>(p => p.CancelCommand, default(ICommand),
                BindingMode.Default, propertyChanged:
                    (bindable, value, newValue) => { ((BaseAddCheckupPage) bindable).btnCancel.Command = newValue; });

        /// <summary>
        ///     Gets or sets the <see cref="CancelCommand" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        /// </value>
        public ICommand CancelCommand
        {
            get { return (ICommand) GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        #endregion

        #region NextCommand

        /// <summary>
        ///     Identifies the <see cref="NextCommand" /> bindable property.
        /// </summary>
        public static readonly BindableProperty NextCommandProperty =
            BindableProperty.Create<BaseAddCheckupPage, ICommand>(p => p.NextCommand, default(ICommand),
                BindingMode.Default, propertyChanged:
                    (bindable, value, newValue) => { ((BaseAddCheckupPage) bindable).btnNext.Command = newValue; });

        /// <summary>
        ///     Gets or sets the <see cref="NextCommand" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        /// </value>
        public ICommand NextCommand
        {
            get { return (ICommand) GetValue(NextCommandProperty); }
            set { SetValue(NextCommandProperty, value); }
        }

        #endregion

        #region NextCommandParam

        /// <summary>
        ///     Identifies the <see cref="NextCommandParam" /> bindable property.
        /// </summary>
        public static readonly BindableProperty NextCommandParamProperty =
            BindableProperty.Create<BaseAddCheckupPage, object>(p => p.NextCommandParam, default(object),
                BindingMode.Default, propertyChanged:
                    (bindable, value, newValue) =>
                    {
                        ((BaseAddCheckupPage) bindable).btnNext.CommandParameter = newValue;
                    });

        /// <summary>
        ///     Gets or sets the <see cref="NextCommandParam" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        /// </value>
        public object NextCommandParam
        {
            get { return GetValue(NextCommandParamProperty); }
            set { SetValue(NextCommandParamProperty, value); }
        }

        #endregion
    }
}