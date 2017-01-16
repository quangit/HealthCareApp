using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;

namespace HealthCare.Touch.Views.Cells
{
	public partial class ConsultResponseCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString ("ConsultResponseCell");
		public static readonly UINib Nib;

		private readonly MvxImageViewLoader _logoloader;

		static ConsultResponseCell ()
		{
			Nib = UINib.FromName ("ConsultResponseCell", NSBundle.MainBundle);
		}

		public ConsultResponseCell (IntPtr handle) : base (handle)
		{

			_logoloader = new MvxImageViewLoader (() => DoctorImage);
			//ContentView.BackgroundColor = UIColor.Cyan;

			this.DelayBind (() => {
				this.AddBindings(new Dictionary<object, string>() {
					{_logoloader, "ImageUrl doctor.Photo"},
					{NameLabel, "Text doctor.FullName"},
					{TimeLabel, "Text whenCreated, Converter = MilisecondToDate"},
					{ReplyTV, "Text comment"},
					{ReplyLabel, "Text [Consult_Reply]"},
					{AnswerCountLabel,"Text IndexString"}
				});
				ResponseView.Layer.CornerRadius = 10f;
				ResponseView.Layer.MasksToBounds = true;
			});
		}
	}
}
