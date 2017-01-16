using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Java.Lang;
using Java.Util;

namespace HealthCare.Droid.Controls
{
	public class MultiSelectSpinner : Spinner, IDialogInterfaceOnMultiChoiceClickListener,IDialogInterfaceOnDismissListener
    {

        string[] _items = null;
        bool[] _selection = null;



        ArrayAdapter<string> _proxyAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiSelectSpinner"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        public MultiSelectSpinner(Context context)
            : base(context)
        {
            _proxyAdapter = new ArrayAdapter<string>(context, Resource.Layout.RepeatSpinnerTemplate);
            _proxyAdapter.Clear();
            _proxyAdapter.Add(Core.Resources.AppResources.ScheduleAdding_OnlyOne);
            base.Adapter = _proxyAdapter;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiSelectSpinner"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="attrs">Attrs.</param>
        public MultiSelectSpinner(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            _proxyAdapter = new ArrayAdapter<string>(context, Resource.Layout.RepeatSpinnerTemplate);
            _proxyAdapter.Clear();
            _proxyAdapter.Add(Core.Resources.AppResources.ScheduleAdding_OnlyOne);
            base.Adapter = _proxyAdapter;
        }

        /// <param name="dialog">The dialog where the selection was made.</param>
        /// <param name="which">The position of the item in the list that was clicked.</param>
        /// <param name="isChecked">True if the click checked the item, else false.</param>
        /// <summary>
        /// This method will be invoked when an item in the dialog is clicked.
        /// </summary>
        public void OnClick(IDialogInterface dialog, int which, bool isChecked)
        {
            if (_selection != null && which < _selection.Length)
            {
                _selection[which] = isChecked;
                
                SetSelection(0);
            }
            else
            {
                throw new IllegalArgumentException("Argument 'which' is out of bounds");
            }
        }
        public event EventHandler OnDismissed;
        /// <summary>
        /// Call this view's OnClickListener, if it is defined.
        /// </summary>
        /// <returns>To be added.</returns>
        public override bool PerformClick()
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(Context);
            
            builder.SetMultiChoiceItems(_items, _selection, this);
			builder.SetOnDismissListener (this);
            builder.Show();
            return true;
        }
        
        public void OnDismiss(IDialogInterface arg0)
        {
            if (OnDismissed != null)
                OnDismissed(this, EventArgs.Empty);
            _proxyAdapter.Clear();
            _proxyAdapter.Add(BuildSelectedItemString());

        }



        /// <summary>
        /// Returns the adapter currently associated with this widget.
        /// </summary>
        /// <value>To be added.</value>
        public override ISpinnerAdapter Adapter
        {
            set { throw new RuntimeException("SetAdapter is not supported by MultiSelectionSpinner."); }
        }

        /// <summary>
        /// Sets the items.
        /// </summary>
        /// <param name="items">Items.</param>
        public void SetItems(string[] items)
        {
            _items = items;
            _selection = new bool[_items.Length];

            Arrays.Fill(_selection, false);
        }

        


        /// <summary>
        /// Sets the items.
        /// </summary>
        /// <param name="items">Items.</param>
        public void SetItems(List<string> items)
        {
            _items = items.ToArray<string>();
            _selection = new bool[_items.Length];

            Arrays.Fill(_selection, false);
        }

        /// <param name="position">Index (starting at 0) of the data item to be selected.</param>
        /// <summary>
        /// Sets the currently selected item.
        /// </summary>
        /// <param name="selection">Selection.</param>
        public void SetSelection(string[] selection)
        {
            foreach (string sel in selection)
            {
                for (int j = 0; j < _items.Length; ++j)
                {
                    if (_items[j].Equals(sel))
                    {
                        _selection[j] = true;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the selection.
        /// </summary>
        /// <param name="selection">Selection.</param>
        public void SetSelection(List<string> selection)
        {
            foreach (string sel in selection)
            {
                for (int j = 0; j < _items.Length; ++j)
                {
                    if (_items[j].Equals(sel))
                    {
                        _selection[j] = true;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the selection.
        /// </summary>
        /// <param name="selectedIndicies">Selected indicies.</param>
        public void SetSelection(int[] selectedIndicies)
        {
            foreach (int index in selectedIndicies)
            {
                if (index >= 0 && index < _selection.Length)
                {
                    _selection[index] = true;
                }
                else
                {
                    throw new IllegalArgumentException("Index " + index + " is out of bounds.");
                }
            }
        }

        /// <summary>
        /// Gets the selected strings.
        /// </summary>
        /// <returns>The selected strings.</returns>
        public List<string> GetSelectedStrings()
        {
            List<string> selection = new List<string>();
            for (int i = 0; i < _items.Length; ++i)
            {
                if (_selection[i])
                {
                    selection.Add(_items[i]);
                }
            }
            return selection;
        }

        /// <summary>
        /// Gets the selected indicies.
        /// </summary>
        /// <returns>The selected indicies.</returns>
        public List<int> GetSelectedIndicies()
        {
            List<int> selection = new List<int>();
            for (int i = 0; i < _items.Length; ++i)
            {
                if (_selection[i])
                {
                    selection.Add(i);
                }
            }
            return selection;
        }

        /// <summary>
        /// Builds the selected item string.
        /// </summary>
        /// <returns>The selected item string.</returns>
        private string BuildSelectedItemString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            bool foundOne = false;

            for (int i = 0; i < _items.Length; ++i)
            {
                if (_selection[i])
                {
                    if (foundOne)
                    {
                        sb.Append(", ");
                    }
                    foundOne = true;

                    sb.Append(_items[i]);
                }
            }
            if (string.IsNullOrEmpty(sb.ToString()))
                return Core.Resources.AppResources.ScheduleAdding_OnlyOne;
            return sb.ToString();
        }


        
    }




}