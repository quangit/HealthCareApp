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
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Android.Util;
using HealthCare.Droid.Adapters;
using Android.Text.Method;

namespace HealthCare.Droid.Controls
{
    public class MvxListViewWithHeader : MvxListView
    {
        /// <summary>
        /// The default id for the grid header/footer.  This means there is no header/footer
        /// </summary>
        private const int DEFAULT_HEADER_ID = -1;

        private int _footerId;
        private int _headerId;

        public MvxListViewWithHeader(Context context, IAttributeSet attrs)
            : base(context, attrs, null)
        {
            IMvxAdapter adapter = new MvxAdapter(context);

            ApplyAttributes(context, attrs);

            var itemTemplateId = MvxAttributeHelpers.ReadListItemTemplateId(context, attrs);
            adapter.ItemTemplateId = itemTemplateId;

            var headers = GetHeaders();
            var footers = GetFooters();
            
            IMvxAdapter headerAdapter = new HeaderMvxAdapter(headers, footers, adapter);

            Adapter = headerAdapter;
        }

        private void ApplyAttributes(Context c, IAttributeSet attrs)
        {
            _headerId = DEFAULT_HEADER_ID;
            _footerId = DEFAULT_HEADER_ID;

            using (var attributes = c.ObtainDisposableStyledAttributes(attrs, Resource.Styleable.ListView))
            {
                foreach (var a in attributes)
                {
                    switch (a)
                    {
                        case Resource.Styleable.ListView_header:
                            _headerId = attributes.GetResourceId(a, DEFAULT_HEADER_ID);
                            break;

                        case Resource.Styleable.ListView_footer:
                            _footerId = attributes.GetResourceId(a, DEFAULT_HEADER_ID);
                            break;
                    }
                }
            }
        }
        public Action<string> ImageAction;
        private IList<ListView.FixedViewInfo> GetFixedViewInfos(int id)
        {
            var viewInfos = new List<ListView.FixedViewInfo>();

            View view = GetBoundView(id);

            if (view != null)
            {

                var landscapeImage = view.FindViewById<MvxImageView>(Resource.Id.landscapeImage);
                if (landscapeImage != null)
                {
                    landscapeImage.Click += (sender, e) => {
                        if (ImageAction != null)
                            ImageAction("");
                    };
                }
                var contentView = view.FindViewById<TextView>(Resource.Id.contentText);
                if (contentView != null)
                {
                    contentView.MovementMethod = new ScrollingMovementMethod();
                    contentView.SetOnTouchListener(new myOnTouch());
                }
                var info = new ListView.FixedViewInfo(this)
                {
                    Data = null,
                    IsSelectable = true,
                    View = view,
                };
                viewInfos.Add(info);
            }

            return viewInfos;
        }

        private IList<ListView.FixedViewInfo> GetFooters()
        {
            return GetFixedViewInfos(_footerId);
        }

        private IList<ListView.FixedViewInfo> GetHeaders()
        {
            return GetFixedViewInfos(_headerId);
        }

        private View GetBoundView(int id)
        {
            if (id == DEFAULT_HEADER_ID) return null;

            IMvxAndroidBindingContext bindingContext = MvxAndroidBindingContextHelpers.Current();
            var view = bindingContext.BindingInflate(id, null);

            return view;
        }
    }


    public class myOnTouch : Java.Lang.Object, View.IOnTouchListener
    {
       
        public bool OnTouch(View v, MotionEvent e)
        {
            v.Parent.RequestDisallowInterceptTouchEvent(true);
            return false;
        }
    }
}