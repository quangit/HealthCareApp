using System;
using Foundation;
using UIKit;
using AllianceCustomPicker;
using System.Collections.Generic;

namespace HealthCare.Touch
{
	public class MyPicker
	{
		private static MyTextField _myTf;
		public static AlliancePicker Create(string headerTitle,UIViewController vc, List<string> names, Action<string,int> itemSelected){
			var picker = new AlliancePicker(vc);

			_myTf = new MyTextField (newText => {
				itemSelected (newText, names.IndexOf(newText));
				picker = null;
			});
			picker.PlainPickerItems = names;
			picker.SourceField = _myTf;
			picker.Type = PickerType.List;
			picker.HeaderTitle = headerTitle;
			picker.BackgrondColor = UIColor.White;
			picker.Show();
			return picker;
		}

	}

	[Register("MyTextField")]
	public class MyTextField : UITextField
	{
		public override string Text {
			get {
				return base.Text;
			}
			set {
				base.Text = value;
				TextChanged (value);
			}
		}


		public override Foundation.NSAttributedString AttributedText {
			get {
				return base.AttributedText;
			}
			set {
				base.AttributedText = value;
			}
		}

		public Action<string> TextChanged;
		public MyTextField (Action<string> textChanged) :base()
		{
			TextChanged = textChanged;
		}

		public MyTextField (IntPtr handle) :base(handle)
		{

		}   
	}
}

