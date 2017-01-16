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
using Android.Util;
using Android.Graphics;
using Android.Support.V4.Widget;

namespace HealthCare.Droid.Views.Utilities
{
    public class SwipeRefreshHintLayout : RelativeLayout, ViewTreeObserver.IOnPreDrawListener
    {
        public SwipeRefreshHintLayout(Context context)
            : base(context)
        {

        }

        public SwipeRefreshHintLayout(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {

        }

        public SwipeRefreshHintLayout(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
        }

        private Rect oldBounds = new Rect(), newBounds = new Rect();
        private View swipeTarget;
        private SwipeRefreshLayout _swipeRefreshLayout;
        public void SetSwipeLayoutTarget(SwipeRefreshLayout swipeRefreshLayout)
        {
            _swipeRefreshLayout = swipeRefreshLayout;
            swipeTarget = swipeRefreshLayout.GetChildAt(0);
            if (swipeTarget == null)
            {
                return;
            }
            swipeTarget.ViewTreeObserver.AddOnPreDrawListener(this);
        }

        public bool OnPreDraw()
        {
            newBounds.Set(swipeTarget.Left, _swipeRefreshLayout.Top, swipeTarget.Right, swipeTarget.Top);
            if (!oldBounds.Equals(newBounds))
            {
                this.LayoutParameters.Height = newBounds.Height();
                RequestLayout();
                oldBounds.Set(newBounds);
            }
            return true;
        }
    }
}