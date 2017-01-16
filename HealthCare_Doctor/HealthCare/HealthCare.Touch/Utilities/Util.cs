using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreAnimation;

namespace HealthCare.Touch.Utilities
{
    public static class Util
    {
		public static DateTime NSDateToDateTime(NSDate date,bool utc = false)
        {
			DateTime reference;

			if(!utc)
				reference = TimeZone.CurrentTimeZone.ToLocalTime(
				new DateTime(2001, 1, 1, 0, 0, 0));
			else 
				reference = new DateTime(2001, 1, 1, 0, 0, 0);
			var ret = reference.AddSeconds(date.SecondsSinceReferenceDate);
			return ret;
        }

        public static NSDate DateTimeToNSDate(DateTime date)
        {
            var secs = date - new DateTime(1970, 1, 1);
            return NSDate.FromTimeIntervalSince1970(secs.TotalSeconds);
        }

//        public static ModalPickerViewController CreateDateModalPicker(UIViewController vc)
//        {
//            var modalPicker = new ModalPickerViewController(ModalPickerType.Date, "", vc)
//            {
//                HeaderBackgroundColor = UIColor.Red,
//                HeaderTextColor = UIColor.White,
//                TransitioningDelegate = new ModalPickerTransitionDelegate(),
//                ModalPresentationStyle = UIModalPresentationStyle.Custom,
//            };
//            var datePicker = modalPicker.DatePicker;
//            datePicker.Mode = UIDatePickerMode.Date;
//
//            return modalPicker;
//        }

		public static UIColor UIColorFromHex(string color)
		{
			var hash = color.StartsWith ("#");
			var offset = hash ? 1 : 0;
			if (color.Length > 7)
				color.Insert (offset, "FF");
			var alpha = Convert.ToInt32(color.Substring(0 + offset, 2), 16) / 255f;
			var red = Convert.ToInt32(color.Substring(2 + offset, 2), 16) / 255f;
			var green = Convert.ToInt32(color.Substring(4 + offset, 2), 16) / 255f;
			var blue = Convert.ToInt32(color.Substring(6 + offset, 2), 16) / 255f;
			return UIColor.FromRGBA(red, green, blue, alpha);

			//            var a = Byte.Parse(color.Substring(0 + offset, 2), NumberStyles.HexNumber);
			//            var r = Byte.Parse(color.Substring(2 + offset, 2), NumberStyles.HexNumber);
			//            var g = Byte.Parse(color.Substring(4 + offset, 2), NumberStyles.HexNumber);
			//            var b = Byte.Parse(color.Substring(6 + offset, 2), NumberStyles.HexNumber);
			//			return UIColor.FromRGBA((nfloat)r, (nfloat)g, (nfloat)b, (nfloat)a);
		}

        public static UIView Picker(nfloat width, nfloat height, IList<string> items, Action<int> selectedItem)
        {

            var pickerView = new UIView(new CoreGraphics.CGRect(0, 0, width, height));
            pickerView.BackgroundColor = new UIColor(0.1f, 0.1f, 0.1f, 0.8f);

            var pickerModel = new PickerModel(items);
            string selectedValue = "";
            int selectedIndex = -1;
            pickerModel.PickerChanged += (sender, e) =>
            {
                selectedValue = e.SelectedValue;
                selectedIndex = e.SelectedIndex;
            };


            var picker = new UIPickerView();
            picker.ShowSelectionIndicator = true;
            picker.Model = pickerModel;
            picker.BackgroundColor = UIColor.White;

            //nfloat buttonWidth = 200;
            var pickbutton = UIButton.FromType(UIButtonType.System);
            pickbutton.Frame = new CoreGraphics.CGRect(0, picker.Frame.Height + 10, width, 50);
            pickbutton.BackgroundColor = UIColor.Gray;
            pickbutton.TintColor = UIColor.White;
            pickbutton.SetTitle("Ok", UIControlState.Normal);
            pickbutton.TitleLabel.Font = UIFont.BoldSystemFontOfSize(18);
            pickbutton.TouchUpInside += (sender, e) =>
            {
                pickerView.Hidden = true;
                selectedItem(selectedIndex);
            };
            pickerView.Add(picker);
            pickerView.Add(pickbutton);
            pickerView.Hidden = true;
            return pickerView;

        }

		public static void VisibleHeightConstraint(UIView view, bool visibleCondition,int height){
			var heightConstraint = view.Constraints.First(x => x.FirstAttribute == NSLayoutAttribute.Height);
			heightConstraint.Constant = (visibleCondition) ? height : 0;
			view.Hidden = !visibleCondition;
		}



		public static void DrawCircularProgress(UIView view, nfloat progress, CoreGraphics.CGColor color , nfloat width){

			var center = new CoreGraphics.CGPoint (view.Bounds.Width / 2, view.Bounds.Height / 2);
			var r = (view.Bounds.Width / 2) - width;
			UIBezierPath circlePath = UIBezierPath.FromArc (center, r, (float)(-0.5f * Math.PI), (float)(1.5 * Math.PI), true);

			var _progressCircle = new CAShapeLayer ();
			_progressCircle.Path = circlePath.CGPath;
			_progressCircle.StrokeColor = color;
			_progressCircle.FillColor = UIColor.Clear.CGColor;
			_progressCircle.LineWidth = width;
			_progressCircle.StrokeStart = 0f;
			_progressCircle.StrokeEnd = progress;
		
			view.Layer.AddSublayer (_progressCircle);
		}

    }


    public class PickerModel : UIPickerViewModel
    {
        private readonly IList<string> values;

        public event EventHandler<PickerChangedEventArgs> PickerChanged;

        public PickerModel(IList<string> values)
        {
            this.values = values;
        }

        public override nint GetComponentCount(UIPickerView picker)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView picker, nint component)
        {
            return values.Count;
        }

        public override string GetTitle(UIPickerView picker, nint row, nint component)
        {
            return values[(int)row];
        }

        public override nfloat GetRowHeight(UIPickerView picker, nint component)
        {
            return 40f;
        }

        public override void Selected(UIPickerView picker, nint row, nint component)
        {
            if (this.PickerChanged != null)
            {
                this.PickerChanged(this, new PickerChangedEventArgs { SelectedValue = values[(int)row], SelectedIndex = (int)row });
            }
        }
    }

    public class PickerChangedEventArgs : EventArgs
    {
        public string SelectedValue { get; set; }
        public int SelectedIndex { get; set; }
    }

}