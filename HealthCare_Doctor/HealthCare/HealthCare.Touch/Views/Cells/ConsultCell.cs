
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using HealthCare.Core.Models;
using HealthCare.Core.Resources;

namespace HealthCare.Touch.Views.Cells
{
	public partial class ConsultCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("ConsultCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("ConsultCell");
		private readonly MvxImageViewLoader _patientloader;
		private readonly MvxImageViewLoader _doctorloader;
		public ConsultCell (IntPtr handle) : base (handle)
		{
			_patientloader = new MvxImageViewLoader (() => PatientImage);
			_doctorloader = new MvxImageViewLoader (() => DoctorImage);
			this.DelayBind (() => {
				this.AddBindings(new Dictionary<object, string>() {
					{_patientloader, "ImageUrl PatientPhoto"},
					{_doctorloader, "ImageUrl LatestDoctorPhoto"},
					{PatientNameLabel, "Text PatientName"},
					{PatientAgeLabel, "Text PatientDesc"},
					{DoctorNameLabel, "Text LatestDoctorFullName"},
					{HospitalNameLabel, "Text LatestReplyTime, Converter = MilisecondToDate"},
					{SymptomTV, "Text Symptom"},
					{ReplyCountLabel, "Text ReplyCountStr"},
					{WhenCreatedLabel, "Text WhenCreated, Converter = MilisecondToDate"},
				});

			});
		}

		public static ConsultCell Create ()
		{
			return (ConsultCell)Nib.Instantiate (null, null) [0];
		}
	}
}

