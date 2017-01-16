using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Java.Lang;

namespace HealthCare.Droid.Controls
{
    [Register("healthcare.droid.controls.ExpandableHeightListView")]
    public class ExpandableHeightListView : MvxListView
    {
        public ExpandableHeightListView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

      

        private bool expanded = true;
        public bool isExpanded()
        {
            return expanded;
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            if (expanded)
            {
                int expandSpec = MeasureSpec.MakeMeasureSpec(Integer.MaxValue >> 2, MeasureSpecMode.AtMost);
                this.OnMeasure(widthMeasureSpec, expandSpec);

                ViewGroup.LayoutParams param = LayoutParameters;
                param.Height = MeasuredHeight;
                this.RequestLayout();
            }
            else
                base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }



    }
}