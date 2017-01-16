using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthCare.Helpers;
using HealthCare.Resx;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class HcListView<T> : StackLayout
    {
        private Label _idLabel;
        private ListViewCustom _idListView;

        public HcListView()
        {
            InitView();
            _idListView.BindingContext = this;
            _idListView.SetBinding<HcListView<T>>(ListViewCustom.ItemsSourceProperty, b => b.ItemsSource);
            _idListView.SetBinding<HcListView<T>>(ListViewCustom.ItemClickCommandProperty, b => b.ItemClickCommand);
            _idListView.SetBinding<HcListView<T>>(ListView.RefreshCommandProperty, b => b.RefreshCommand);
        }

        Cell GetViewCell()
        {
            var content = new ViewCell();
            content.BindingContextChanged += OnBindingContextChanged;
            return content;
        }

        public static readonly BindableProperty TemplateSelectorProperty = BindableProperty.Create<HcListView<T>, IDataTemplateSelector>(p => p.TemplateSelector, null,
            propertyChanged: (bindable, value, newValue) =>
            {
                if (newValue != null)
                    ((HcListView<T>)bindable).ItemTemplate = new DataTemplate(((HcListView<T>)bindable).GetViewCell);
            });

        public IDataTemplateSelector TemplateSelector
        {
            get { return (IDataTemplateSelector)GetValue(TemplateSelectorProperty); }
            set { SetValue(TemplateSelectorProperty, value); }
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            var cell = (ViewCell)sender;
            if (TemplateSelector != null)
            {
                var template = TemplateSelector.SelectTemplate(cell, cell.BindingContext);
                cell.View = ((ViewCell)template.CreateContent()).View;
            }
        }

        public Type TypeArgument { get; set; }

        public string EmptyText
        {
            get { return _idLabel.Text; }
            set { _idLabel.Text = value; }
        }

        public bool IsPullToRefreshEnabled
        {
            get { return _idListView.IsPullToRefreshEnabled; }
            set { _idListView.IsPullToRefreshEnabled = value; }
        }

        public DataTemplate ItemTemplate
        {
            get { return _idListView.ItemTemplate; }
            set { _idListView.ItemTemplate = value; }
        }
        public bool IsShowVnPay { get; set; }

        public bool IsLoadMoreEnabled { get; set; }

        private void InitView()
        {
            _idLabel = new Label
            {
                Text = AppResources.empty,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                Style = HcStyles.LabelContentWordWrapStyle,
                TextColor = Color.FromHex("#989899"),
                FontAttributes = FontAttributes.Bold,
                XAlign = TextAlignment.Center
            };
            _idListView = new ListViewCustom { HasUnevenRows = true };

            _idLabel.IsVisible = ItemCount <= 0;
            _idListView.IsVisible = ItemCount > 0;

            VerticalOptions = LayoutOptions.FillAndExpand;
            Children.Add(_idLabel);
            Children.Add(_idListView);
        }

        #region ItemCount

        /// <summary>
        ///     Identifies the <see cref="ItemCount" /> bindable property.
        /// </summary>
        public static readonly BindableProperty ItemCountProperty =
            BindableProperty.Create<HcListView<T>, int>(p => p.ItemCount, default(int), BindingMode.TwoWay,
                propertyChanged:
                    (bindable, value, newValue) =>
                    {
                        var control = (HcListView<T>)bindable;

                        control._idLabel.IsVisible = newValue <= 0;
                        control._idListView.IsVisible = newValue > 0;

                        //if (newValue <= 0) control.VerticalOptions = LayoutOptions.Center;

                        //if (newValue > 0 && newValue <= 4 && Common.OS == TargetPlatform.WinPhone)
                        //    control._idListView.HeightRequest = (130 * newValue);
                    });

        /// <summary>
        ///     Gets or sets the <see cref="ItemCount" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        /// </value>
        public int ItemCount
        {
            get { return (int)GetValue(ItemCountProperty); }
            set { SetValue(ItemCountProperty, value); }
        }

        #endregion

        #region ItemsSource

        /// <summary>
        ///     Identifies the <see cref="ItemsSource" /> bindable property.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create<HcListView<T>, Collection<T>>(p => p.ItemsSource, default(Collection<T>),
                BindingMode.OneWay);

        /// <summary>
        ///     Gets or sets the <see cref="ItemsSource" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        /// </value>
        public Collection<T> ItemsSource
        {
            get { return (Collection<T>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        #endregion

        #region RefreshCommand

        /// <summary>
        ///     Identifies the <see cref="RefreshCommand" /> bindable property.
        /// </summary>
        public static readonly BindableProperty RefreshCommandProperty =
            BindableProperty.Create<HcListView<T>, ICommand>(p => p.RefreshCommand, default(ICommand),
                BindingMode.Default);

        /// <summary>
        ///     Gets or sets the <see cref="RefreshCommand" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        /// </value>
        public ICommand RefreshCommand
        {
            get { return (ICommand)GetValue(RefreshCommandProperty); }
            set { SetValue(RefreshCommandProperty, value); }
        }

        #endregion

        #region IsRefreshing

        /// <summary>
        ///     Identifies the <see cref="IsRefreshing" /> bindable property.
        /// </summary>
        public static readonly BindableProperty IsRefreshingProperty =
            BindableProperty.Create<HcListView<T>, bool>(p => p.IsRefreshing, default(bool), BindingMode.TwoWay,
                propertyChanged:
                    (bindable, value, newValue) => { ((HcListView<T>)bindable)._idListView.IsRefreshing = newValue; });

        /// <summary>
        ///     Gets or sets the <see cref="IsRefreshing" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        /// </value>
        public bool IsRefreshing
        {
            get { return (bool)GetValue(IsRefreshingProperty); }
            set { SetValue(IsRefreshingProperty, value); }
        }

        #endregion

        #region ItemClickCommand

        /// <summary>
        ///     Identifies the <see cref="ItemClickCommand" /> bindable property.
        /// </summary>
        public static readonly BindableProperty ItemClickCommandProperty =
            BindableProperty.Create<HcListView<T>, ICommand>(p => p.ItemClickCommand, default(ICommand),
                BindingMode.Default);

        /// <summary>
        ///     Gets or sets the <see cref="ItemClickCommand" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        /// </value>
        public ICommand ItemClickCommand
        {
            get { return (ICommand)GetValue(ItemClickCommandProperty); }
            set { SetValue(ItemClickCommandProperty, value); }
        }

        #endregion

        #region LoadMoreCommand

        /// <summary>
        ///     Identifies the <see cref="LoadMoreCommand" /> bindable property.
        /// </summary>
        public static readonly BindableProperty LoadMoreCommandProperty =
            BindableProperty.Create<HcListView<T>, ICommand>(p => p.LoadMoreCommand, default(ICommand),
                BindingMode.Default, propertyChanged:
                    (bindable, value, newValue) =>
                    {
                        var control = (HcListView<T>)bindable;
                        if (control.IsLoadMoreEnabled)
                        {
                            control._idListView.ItemAppearing += (sender, args) =>
                            {
                                if (!control.CanLoadMore) { }
                                if (!control.CanLoadMore || control.ItemsSource.Count == 0)
                                    return;
                                if (((T)args.Item).Equals(control.ItemsSource.Last()))
                                    control.LoadMoreCommand?.Execute(null);
                            };
                        }
                    });

        /// <summary>
        ///     Gets or sets the <see cref="LoadMoreCommand" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        /// </value>
        public ICommand LoadMoreCommand
        {
            get { return (ICommand)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }

        #endregion

        #region CanLoadMore

        /// <summary>
        ///     Identifies the <see cref="CanLoadMore" /> bindable property.
        /// </summary>
        public static readonly BindableProperty CanLoadMoreProperty =
            BindableProperty.Create<HcListView<T>, bool>(p => p.CanLoadMore, default(bool), BindingMode.TwoWay, propertyChanged:
                (bindable, value, newValue) =>
                {
                    if (newValue)
                    {
                        //true
                    }
                    else
                    {
                        //false
                    }
                });

        /// <summary>
        ///     Gets or sets the <see cref="CanLoadMore" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        /// </value>
        public bool CanLoadMore
        {
            get { return (bool)GetValue(CanLoadMoreProperty); }
            set { SetValue(CanLoadMoreProperty, value); }
        }

        #endregion
    }

    public interface IDataTemplateSelector
    {
        DataTemplate SelectTemplate(object view, object dataItem);
    }
}