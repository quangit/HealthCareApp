
using System;

using Foundation;
using UIKit;
using HealthCare.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using HealthCare.Core.Resources;

namespace HealthCare.Touch.Views
{
	public partial class CheckupView : BaseViewController
	{
		public CheckupView () : base ("CheckupView", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.

			this.AddBindings (new Dictionary<object, string> () {
				{ ViewTitleLabel, "Text [CheckupView_Title]" },
				{ DoctorLabel, "Text [CheckupView_Doctor]" },
				{ DoctorNameLabel, "Text Checkup.Doctor.Name" },
				{ TimeLabel, "Text [CheckupView_Appoint]" },
				{ TimeValueLabel,"Text Checkup.Date" },
				{ PlaceLabel, "Text [CheckupView_Hospital]" },
				{ PlaceValueLabel, "Text Checkup.Hospital.Name" },
				{ CheckupTypeLabel, "Text [CheckupView_CheckType]" },
				{ CheckupTypeValueLabel,"Text Checkup.CheckupType" },
				{ SymtompLabel, "Text [CheckupView_Sym]" },
				{ SymtompValueTV, "Text Checkup.Symptom" },
				{ PatientLabel, "Text [CheckupView_Patient]" },
				{ PatientCodeLabel,"Text Checkup.Patient.Code" },
				{ PatientNameLabel, "Text Checkup.Patient.Name; Placeholder [SignUp_FirstName]" },
				{ PatientPhoneLabel, "Text Checkup.Patient.Phone; Placeholder [SignUp_Phone]" },
				{ PatientSSNLabel, "Text Checkup.Patient.IdNo, Converter = TextHint, ConverterParameter = SignUp_SID" },
				{ PatientEmailLabel, "Text Checkup.Patient.Address; Placeholder [SignUp_Address]" },
				{AppointmentNumberLabel, "Text Checkup.AppointmentNoStr"}
			});

			Title = Core.Resources.AppResources.CheckupView_Title;
		}
	}
}

