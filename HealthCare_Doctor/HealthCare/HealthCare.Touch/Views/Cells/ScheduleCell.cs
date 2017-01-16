
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;

namespace HealthCare.Touch.Views.Cells
{
	public partial class ScheduleCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("ScheduleCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("ScheduleCell");


		private readonly MvxImageViewLoader _logoloader;

		public ScheduleCell (IntPtr handle) : base (handle)
		{
			_logoloader = new MvxImageViewLoader (() => HospitalLogoImage);
			//ContentView.BackgroundColor = UIColor.Cyan;

			this.DelayBind (() => {
				this.AddBindings(new Dictionary<object, string>() {
					{_logoloader, "ImageUrl Hospital.Photos.Logo"},
					{ScheduleTitleLabel, "Text Hospital.Name"},
					{HospitalNameLabel, "Text TimeExam"},
				});
			});
		}

		public static ScheduleCell Create ()
		{
			return (ScheduleCell)Nib.Instantiate (null, null) [0];
		}
	}
}

