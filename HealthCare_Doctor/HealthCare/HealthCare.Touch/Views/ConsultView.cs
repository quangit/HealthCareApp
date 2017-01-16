
using System;

using Foundation;
using UIKit;
using HealthCare.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using HealthCare.Touch.Views.Dialogs;
using HealthCare.Core.ViewModels;
using Cirrious.MvvmCross.Binding.Touch.Views;
using HealthCare.Touch.Views.Cells;
using HealthCare.Core.Resources;
using HealthCare.Touch.Utilities;


namespace HealthCare.Touch.Views
{
	public partial class ConsultView : BaseViewController
	{
		private MvxImageViewLoader _landscapeloader;
		private MvxImageViewLoader _patientloader;
		private MvxImageViewLoader _doctorloader;
		private ConsultViewModel _vm{
			get{ return base.ViewModel as ConsultViewModel;}
		}
		public ConsultView () : base ("ConsultView", null)
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
			InviteBarButton.Enabled = _vm.Request.CanReply;
			ReplyBarButton.Enabled = _vm.Request.CanReply;
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			_landscapeloader = new MvxImageViewLoader (() => LandscapeImage);
			_patientloader = new MvxImageViewLoader (() => PatientImage);
			_doctorloader = new MvxImageViewLoader (() => DoctorImage);

			var source = new ConsultRespTableViewSource(TableView, ConsultResponseCell.Key, ConsultResponseCell.Key){ViewModel = _vm};
			TableView.Source = source;
			//TableView.RowHeight = 200;
			// Perform any additional setup after loading the view, typically from a nib.
			this.AddBindings (new Dictionary<object, string> () {
				{_patientloader, "ImageUrl Request.PatientPhoto"},
				{_doctorloader, "ImageUrl Request.LatestDoctorPhoto"},
//				{ ViewTitleLabel, "Text [Consult_Title]" },
//				{ DoctorLabel, "Text [CheckupView_Doctor]" },
				{ DoctorNameLabel, "Text Request.LatestDoctorFullName" },
				{ ConsultLabel, "Text [ConsultView_Content]" },
				{ ConsultValueTV, "Text Request.Symptom" },
//				{ PatientLabel, "Text [CheckupView_Patient]" },
				{ PatientNameLabel, "Text Request.PatientName;" },
//				{ PatientDescLabel, "Text [ConsultView_PatientDesc]" },
				{ PatientDescValueLabel, "Text Request.PatientDesc" },
//				{ InviteButton, "Title [ConsultView_Invite]; Visible Request.CanReply" },
//				{ ReplyButton,"Title [ConsultView_Reply]; TouchUpInside ReplyCommand; Visible Request.CanReply" },
				{source, "ItemsSource Responses"},//; SelectionChangedCommand DealTapCommand
				{_landscapeloader,"ImageUrl Request.LandscapeImage"},
				{ReplyBarButton,"Enabled Request.CanReply"},
				{ReplyCountLabel, "Text Request.ReplyCountStr"},
				{WhenCreatedLabel, "Text Request.WhenCreated, Converter = MilisecondToDate"},
				{ImageZoomButton, "TouchUpInside ImageZoomCommand"},
			});
//			TableView.TableHeaderView = null;
			var inviteVc = new ConsultInviteDialog (){ViewModel = ViewModel};
			_vm.MesssageSent += (sender, e) => ModalBgView.RemoveFromSuperview();
			InviteBarButton.Clicked += (sender, e) => {
				
				ShowModal (inviteVc.View,250,true);
					//ModalBgView.RemoveFromSuperview();
			};

			var replyVc = new ConsultReplyDialog (){ViewModel = ViewModel};
			ReplyBarButton.Clicked += (sender, e) => {
				ShowModal (replyVc.View,250,true);
			};
			Title = Core.Resources.AppResources.Consult_Title;

			SkypeBarButton.Clicked += (sender, e) => {
				var installed = UIApplication.SharedApplication.CanOpenUrl(new NSUrl(_vm.Request.PatientSkypeUrl));
				if (installed)
					UIApplication.SharedApplication.OpenUrl(new NSUrl(_vm.Request.PatientSkypeUrl));
				else
					UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.skype.com/go/getskype-iphone/"));

					

			};
			if (string.IsNullOrEmpty (_vm.Request.PatientSkypeUrl))
				SkypeBarButton.Enabled = false;

		}
	}

	/*
@property (retain, nonatomic) IBOutlet UILabel *ViewTitleLabel;
@property (retain, nonatomic) IBOutlet UILabel *PatientLabel;
@property (retain, nonatomic) IBOutlet UILabel *PatientNameLabel;
@property (retain, nonatomic) IBOutlet UILabel *PatientDescLabel;
@property (retain, nonatomic) IBOutlet UILabel *PatientDescValueLabel;
@property (retain, nonatomic) IBOutlet UILabel *DoctorLabel;
@property (retain, nonatomic) IBOutlet UILabel *DoctorNameLabel;
@property (retain, nonatomic) IBOutlet UILabel *HospitalLabel;
@property (retain, nonatomic) IBOutlet UILabel *HospitalNameLabel;
@property (retain, nonatomic) IBOutlet UILabel *ConsultLabel;
@property (retain, nonatomic) IBOutlet UITextView *ConsultValueTV;
@property (retain, nonatomic) IBOutlet UILabel *ConsultReplyLabel;
@property (retain, nonatomic) IBOutlet UITextView *ConsultReplyValueTV;
@property (retain, nonatomic) IBOutlet UIButton *InviteButton;
@property (retain, nonatomic) IBOutlet UIButton *ReplyButton;
	 */
}

