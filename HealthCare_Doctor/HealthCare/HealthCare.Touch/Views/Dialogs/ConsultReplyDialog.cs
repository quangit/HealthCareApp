using System;

using UIKit;
using HealthCare.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;

namespace HealthCare.Touch.Views.Dialogs
{
	public partial class ConsultReplyDialog : BaseViewController
	{
		public ConsultReplyDialog () : base ("ConsultReplyDialog", null)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			ReplyHintLabel.Hidden = !string.IsNullOrEmpty (ReplyTV.Text);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			this.AddBindings(new Dictionary<object, string>() {
				{ReplyTitleLabel, "Text [ConsultView_Reply]"},
				{ReplyTV, "Text Request.ReplyContent, Mode=TwoWay;"},
				{SubmitButton,"Title [Consult_Send]; TouchUpInside ReplyCommand;" },
				{ReplyHintLabel,"Text [Consult_DoctorDesc]"}
			});

			ReplyTV.Layer.BorderColor = UIColor.Gray.CGColor;
			ReplyTV.Layer.CornerRadius = 8f;
			ReplyTV.Layer.BorderWidth = 1f;
			ReplyTV.ShouldBeginEditing = sender => {
				ReplyHintLabel.Hidden = true;
				TextFieldBegin(sender);
				return true;
			};
			ReplyTV.ShouldEndEditing = sender => {
				if(string.IsNullOrEmpty(ReplyTV.Text))
					ReplyHintLabel.Hidden = false;
				return true;
			};
			SubmitButton.Layer.CornerRadius = 8f;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

//@property (retain, nonatomic) IBOutlet UILabel *ReplyTitleLabel;
//@property (retain, nonatomic) IBOutlet UITextView *ReplyTV;
//@property (retain, nonatomic) IBOutlet UIButton *SubmitButton;
