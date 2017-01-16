using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using Foundation;
using HealthCare;
using HealthCare.Helpers;
using HealthCare.iOS.Renderers;
using HealthCare.ViewModels;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(PlaceholderEditor), typeof(PlaceholderEditorRenderer))]

namespace HealthCare.iOS.Renderers
{
    public class PlaceholderEditorRenderer : EditorRenderer
    {
        UILabel labelPlaceHolder;
        UITextView replacingControl;

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (this.Element is PlaceholderEditor)
            {
                replacingControl = new UITextView(Control.Bounds);
                var adelegate = new myTextViewDelegate();
                var element = this.Element as PlaceholderEditor;
                adelegate.Placeholder = element.Placeholder;
                replacingControl.Delegate = adelegate;
                replacingControl.TextColor = UIColor.LightGray;
                replacingControl.Font =
                    UIFont.SystemFontOfSize((float)HcStyles.FontSizeContent -2 );
                replacingControl.Text = adelegate.Placeholder;
                replacingControl.ScrollRangeToVisible(new NSRange(0, 10));
                replacingControl.ScrollEnabled = false;
                AppConstant.SymptomIOS = "";
                this.SetNativeControl(replacingControl);
            }
        }
    }

    public class myTextViewDelegate : UITextViewDelegate
    {
        public string Placeholder { get; set; }

     
        public override void Changed(UITextView textView)
        {
            if (textView.Text != Placeholder)
            {
                AppConstant.SymptomIOS = textView.Text;
            }
        }
        public override void EditingStarted(UITextView textView)
        {
            if (textView.Text == Placeholder)
            {
                textView.Text = "";
                textView.TextColor = UIColor.Black;
            }
            textView.BecomeFirstResponder();
            UIView view = getRootSuperView(textView);

            CGRect rect = view.Frame;

            rect.Y -= 80;

            view.Frame = rect;

        }

        private UIView getRootSuperView(UIView view)
        {
            if (view.Superview == null)
                return view;
            else
                return getRootSuperView(view.Superview);
        }

        public override void EditingEnded(UITextView textView)
        {
            if (textView.Text == "")
            {
                textView.Text = Placeholder;
                textView.TextColor = UIColor.LightGray;
            }
            textView.ResignFirstResponder();

            UIView view = getRootSuperView(textView);

            CGRect rect = view.Frame;

            rect.Y += 80;

            view.Frame = rect;

        }
    }
}
