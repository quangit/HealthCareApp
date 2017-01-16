using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthCare.Exceptions;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class ListViewExtented<T> : ContentView
    {
        private static Label _messageLable;
        private static ActivityIndicator _activityIndicator;
        //public event EventHandler<object> SelectedItem;
        public static BindableProperty ItemClickCommandProperty =
            BindableProperty.Create<ListViewExtented<T>, ICommand>(x => x.ItemClickCommand, null);

        private Func<int, Task<ObservableCollection<T>>> _functionLoadData;
        private DataTemplate _itemTemplate;
        private ListViewLoadMore _listView;

        public ListViewExtented()
        {
        }

        public ListViewExtented(string message)
        {
            //ItemTemplate = itemTemplate;
            //FunctionLoadData = functionLoadData;
            MesssageNoItem = message;
            Run();
        }

        public bool HasHasUnevenRows { get; set; } = true;
        public bool IsPullToRefresh { get; set; }

        public DataTemplate ItemTemplate
        {
            get { return _itemTemplate; }
            set
            {
                _itemTemplate = value;
                Run();
            }
        }

        public Func<int, Task<ObservableCollection<T>>> FunctionLoadData
        {
            get { return _functionLoadData; }
            set
            {
                _functionLoadData = value;
                Run();
            }
        }

        public string MesssageNoItem { get; set; }
        public bool IsLoadMore { get; set; } = true;
        public ObservableCollection<T> ListItem => _listView.ListItem;

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
                ((ListView) sender).SelectedItem = null;
            }
        }

        public void Reset()
        {
            _listView.Reset();
        }

        private void Run()
        {
            if (FunctionLoadData != null && ItemTemplate != null)
            {
                _listView = new ListViewLoadMore(ItemTemplate, async page => await FunctionLoadData(page),
                    IsLoadMore, IsPullToRefresh, RefreshingCommand, HasHasUnevenRows);
                //_listView.BindingContext = this;
                //_listView.SetBinding(ListView.IsRefreshingProperty, "IsRefreshing", BindingMode.TwoWay);

                _messageLable = new Label
                {
                    Text = MesssageNoItem,
                    FontSize = 20,
                    IsVisible = false,
                    TextColor = Color.Black,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };

                _activityIndicator = new ActivityIndicator
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    IsRunning = true
                };

                var layout = new Grid();
                layout.Children.Add(_listView);
                layout.Children.Add(_messageLable);
                layout.Children.Add(_activityIndicator);

                Content = layout;

                _listView.ItemTapped += OnItemTapped;
                _listView.ItemSelected += (s, e) => { ((ListView) s).SelectedItem = null; };

                _listView.Run();
            }
        }

        private class ListViewLoadMore : ListView
        {
            public readonly ObservableCollection<T> ListItem = new ObservableCollection<T>();

            public ListViewLoadMore(DataTemplate itemTemplate,
                Func<int, Task<ObservableCollection<T>>> loadData, bool isLoadMore,
                bool isPullToRefresh, ICommand pullToRefreshCommand,
                bool isHasHasUnevenRows = false)
            {
                HasUnevenRows = isHasHasUnevenRows;
                if (isLoadMore)
                    ItemAppearing +=
                        ListViewExtent_ItemAppearing;
                LoadData = loadData;
                HasUnevenRows = true;
                ItemTemplate = itemTemplate;
                ItemsSource = ListItem;

                //this.IsPullToRefreshEnabled = isPullToRefresh;
                //this.RefreshCommand = pullToRefreshCommand;
                //this.Refreshing += (sender, args) =>
                //{
                //    Page = 1;
                //    Run();
                //    EndRefresh();
                //};
            }

            private int Page { get; set; } = 1;
            private bool IsLoading { get; set; }
            private Func<int, Task<ObservableCollection<T>>> LoadData { get; }

            public async void Run()
            {
                await GetData();
            }

            public async void Reset()
            {
                if (ListItem.Count > 0)
                {
                    while (ListItem.Count > 0)
                    {
                        ListItem.RemoveAt(ListItem.Count - 1);
                    }
                    Page = 1;
                    await GetData();
                }
            }

            private async Task GetData()
            {
                _activityIndicator.IsVisible = _activityIndicator.IsRunning = IsLoading = true;

                ObservableCollection<T> result = null;

                try
                {
                    result = await LoadData(Page);
                }
                catch (NetworkException)
                {
                    result = null;
                }
                catch (ApiException)
                {
                    result = null;
                }

                if (result != null && result.Count > 0)
                {
                    foreach (var item in result)
                        ListItem.Add(item);
                    Page++;
                }
                else
                    _messageLable.IsVisible = true;
                _activityIndicator.IsVisible = _activityIndicator.IsRunning = IsLoading = false;
            }

            private async void ListViewExtent_ItemAppearing(object sender, ItemVisibilityEventArgs e)
            {
                if (IsLoading || ListItem.Count == 0)
                    return;
                if (((T) e.Item).Equals(ListItem[ListItem.Count - 1]))
                    await GetData();
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

        #region RefreshingCommand

        /// <summary>
        ///     Identifies the <see cref="RefreshingCommand" /> bindable property.
        /// </summary>
        public static readonly BindableProperty RefreshingCommandProperty =
            BindableProperty.Create<ListViewExtented<T>, ICommand>(p => p.RefreshingCommand, default(ICommand),
                BindingMode.Default);

        /// <summary>
        ///     Gets or sets the <see cref="RefreshingCommand" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        /// </value>
        public ICommand RefreshingCommand
        {
            get { return (ICommand) GetValue(RefreshingCommandProperty); }
            set { SetValue(RefreshingCommandProperty, value); }
        }

        #endregion

        #region IsRefreshing

        /// <summary>
        ///     Identifies the <see cref="IsRefreshing" /> bindable property.
        /// </summary>
        public static readonly BindableProperty IsRefreshingProperty =
            BindableProperty.Create<ListViewExtented<T>, bool>(p => p.IsRefreshing, default(bool), BindingMode.Default);

        /// <summary>
        ///     Gets or sets the <see cref="IsRefreshing" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        /// </value>
        public bool IsRefreshing
        {
            get { return (bool) GetValue(IsRefreshingProperty); }
            set { SetValue(IsRefreshingProperty, value); }
        }

        #endregion
    }
}