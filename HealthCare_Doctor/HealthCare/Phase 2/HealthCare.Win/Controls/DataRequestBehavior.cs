using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Microsoft.Xaml.Interactivity;

namespace HealthCare.Win.Controls
{
    public class CustomPanel : Panel
    {

        private double _maxWidth;
        private double _maxHeight;

        protected override Size ArrangeOverride(Size finalSize)
        {
            var x = 0.0;
            var y = 0.0;

            foreach (var child in Children)
            {
                // if there is not enough space left, put in new column
                // si il n'a a pas assez d'espace, crée une nouvelle colonne
                if ((_maxHeight + y) > finalSize.Height)
                {
                    y = 0;
                    x += _maxWidth;
                }

                var newpos = new Rect(x, y, _maxWidth, _maxHeight);

                child.Arrange(newpos);

                y += _maxHeight;
            }
            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            // check the maximum width/height for all children
            // cherche la largeur/hauteur maximum
            foreach (var child in Children)
            {
                child.Measure(availableSize);

                var desirtedwidth = child.DesiredSize.Width;
                if (desirtedwidth > _maxWidth)
                    _maxWidth = desirtedwidth;

                var desiredheight = child.DesiredSize.Height;
                if (desiredheight > _maxHeight)
                    _maxHeight = desiredheight;
            }

            // take the available height to compute how many items per column
            // utilise la hauteur disponible pour calculer le nombre d'item par colonne
            var itemspercolumn = Math.Floor(availableSize.Height / _maxHeight);


            // compute the number of columns needed
            // calcule le nombre de colonne necessaires
            var columns = Math.Ceiling(Children.Count / itemspercolumn);

            Debug.WriteLine("Max width : " + _maxWidth);
            Debug.WriteLine("Max height : " + _maxHeight);
            Debug.WriteLine("Items per columns : " + itemspercolumn);
            Debug.WriteLine("Columns : " + columns);

            return new Size(_maxWidth * columns, itemspercolumn * _maxHeight);
        }
    }
    public class DataRequestBehavior : Behavior<Windows.UI.Xaml.Controls.ListViewBase>
    {
        public class DataSource : IList, ISupportIncrementalLoading, INotifyCollectionChanged
        {
            List<object> _storage = new List<object>();

            #region IList

            public int Add(object value)
            {
                throw new NotImplementedException();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public bool Contains(object value)
            {
                return _storage.Contains(value);
            }

            public int IndexOf(object value)
            {
                return _storage.IndexOf(value);
            }

            public void Insert(int index, object value)
            {
                throw new NotImplementedException();
            }

            public bool IsFixedSize
            {
                get { return false; }
            }

            public bool IsReadOnly
            {
                get { return true; }
            }

            public void Remove(object value)
            {
                throw new NotImplementedException();
            }

            public void RemoveAt(int index)
            {
                throw new NotImplementedException();
            }

            public object this[int index]
            {
                get
                {
                    return _storage[index];
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public void CopyTo(Array array, int index)
            {
                ((IList)_storage).CopyTo(array, index);
            }

            public int Count
            {
                get { return _storage.Count; }
            }

            public bool IsSynchronized
            {
                get { return false; }
            }

            public object SyncRoot
            {
                get { throw new NotImplementedException(); }
            }

            public IEnumerator GetEnumerator()
            {
                return _storage.GetEnumerator();
            }

            #endregion


            public DataSource(IEnumerable collection)
            {
                foreach (var t in collection)
                {
                    _storage.Add(t);
                }

                var i = collection as INotifyCollectionChanged;
                if (i != null)
                {
                    i.CollectionChanged -= I_CollectionChanged;
                    i.CollectionChanged += I_CollectionChanged;
                }
            }

            private void I_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        _storage.InsertRange(e.NewStartingIndex, e.NewItems.Cast<object>());
                        break;
                    case NotifyCollectionChangedAction.Move:
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                CollectionChanged?.Invoke(this, e);
            }

            public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
            {
                return AsyncInfo.Run((c) => LoadMoreItems());
            }

            public Action<TaskCompletionSource<bool>> DataRequested;
            private TaskCompletionSource<bool> _tcs;

            private async Task<LoadMoreItemsResult> LoadMoreItems()
            {
                _tcs = new TaskCompletionSource<bool>();
                DataRequested?.Invoke(_tcs);
                _hasMoreItem = await _tcs.Task;
                return new LoadMoreItemsResult();
            }

            private bool _hasMoreItem = true;
            public bool HasMoreItems => _hasMoreItem;
            public event NotifyCollectionChangedEventHandler CollectionChanged;
        }

        private DataSource _source;

        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set
            {
                if (value != ItemsSource)
                {
                    SetValue(ItemsSourceProperty, value);
                    SetSource();
                }
            }
        }

        public event Action<TaskCompletionSource<bool>> DataRequested;


        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(object), typeof(DataRequestBehavior), new PropertyMetadata(null));


        protected override void OnAttached()
        {
            //if (ItemsSource == null)
            //{
            //}
            //else
            //{
            //    SetSource();
            //}

            base.OnAttached();
        }

        private void SetSource()
        {
            if (ItemsSource == null)
                return;

            _source = new DataSource(ItemsSource as IEnumerable) { DataRequested = DataRequested };

            if (AssociatedObject.ItemsSource != _source)
                AssociatedObject.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = _source });
        }
    }
}