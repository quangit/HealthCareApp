
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;

namespace HealthCare.Touch.Views.Cells
{
	public partial class CheckUpCell : MvxTableViewCell
	{

		private readonly MvxImageViewLoader _patientloader;
		private readonly MvxImageViewLoader _doctorloader;
		public static readonly UINib Nib = UINib.FromName ("CheckUpCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("CheckUpCell");

		public CheckUpCell (IntPtr handle) : base (handle)
		{
			_patientloader = new MvxImageViewLoader (() => PatientImage);
			_doctorloader = new MvxImageViewLoader (() => DoctorImage);
			this.DelayBind (() => {
				this.AddBindings(new Dictionary<object, string>() {
					{PatientNameLabel, "Text Patient.Name"},
					{PatientAgeLabel, "Text Patient.Address"},
					{DoctorNameLabel, "Text Doctor.Name"},
					{HospitalNameLabel, "Text Hospital.Name"},
					{SymptomLabel, "Text Symptom"},
					{TimeLabel, "Text Date"},
					{_patientloader, "ImageUrl Patient.Photo"},
					{_doctorloader, "ImageUrl Doctor.Photo"},
				});
			});
		}

		public static CheckUpCell Create ()
		{
			return (CheckUpCell)Nib.Instantiate (null, null) [0];
		}
	}
}

