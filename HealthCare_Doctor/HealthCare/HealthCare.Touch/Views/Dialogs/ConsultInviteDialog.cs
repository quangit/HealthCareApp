using System;
using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using HealthCare.Core.ViewModels;
using HealthCare.Touch.Controls;
using HealthCare.Core.Resources;

namespace HealthCare.Touch.Views.Dialogs
{
	public partial class ConsultInviteDialog : BaseViewController
	{
		private ConsultViewModel _vm{
			get{ return base.ViewModel as ConsultViewModel;}
		}
		public ConsultInviteDialog () : base ("ConsultInviteDialog", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			ContentHintLabel.Hidden = !string.IsNullOrEmpty (ContentValueTV.Text);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			//ContentValueTV = new PlaceholderTextView(AppResources.Consult_DoctorDesc);
			this.AddBindings(new Dictionary<object, string>() {
				{EmailLabel, "Text [SignUp_Email]"},
				{EmailValueTF, "Text Email, Mode=TwoWay;Placeholder [Consult_DoctorEmail]"},
				{ContentLabel, "Text [ConsultView_InviteContent]"},
				{ContentValueTV, "Text InviteMessage, Mode=TwoWay"},
				{SubmitButton, "Title [Consult_Send]; TouchUpInside InviteCommand"},
				{ContentHintLabel,"Text [Consult_DoctorDesc]"}
			});

			//var contentHint = AppResources.Consult_DoctorDesc;
			EmailValueTF.Layer.BorderColor = UIColor.LightGray.CGColor;
			EmailValueTF.Layer.CornerRadius = 8f;
			EmailValueTF.Layer.BorderWidth = 1f;
			ContentValueTV.Layer.BorderColor = UIColor.LightGray.CGColor;
			ContentValueTV.Layer.CornerRadius = 8f;
			ContentValueTV.Layer.BorderWidth = 1f;
			ContentValueTV.ShouldBeginEditing = sender => {
				ContentHintLabel.Hidden = true;
				TextFieldBegin(sender);
				return true;
			};

			SubmitButton.Layer.CornerRadius = 8f;
			ContentValueTV.ShouldEndEditing = sender => {
				if(string.IsNullOrEmpty(ContentValueTV.Text))
					ContentHintLabel.Hidden = false;
				return true;
			};


		}
	}
}

