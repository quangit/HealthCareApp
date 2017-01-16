using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using System.Diagnostics;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using Debug = System.Diagnostics.Debug;

namespace HealthCare.Droid.Controls
{
	public class LoadMoreMvxAdapter : MvxAdapter
	{
	    public event EventHandler LoadMore;
	    public LoadMoreMvxAdapter(Context context) : base(context)
	    {
	    }

	    public LoadMoreMvxAdapter(Context context, IMvxAndroidBindingContext bindingContext) : base(context, bindingContext)
	    {
	    }

	    protected LoadMoreMvxAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
	    {
	    }

	    protected override View GetView(int position, View convertView, ViewGroup parent, int templateId)
	    {
            if (position == Count - 1)
            {
                LoadMore?.Invoke(this, null);
            }
            //Debug.WriteLine("Adapter: " + position + " - " + (Count - 1));
            return base.GetView(position, convertView, parent, templateId);
	    }
        
	}
}