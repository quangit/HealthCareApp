using System;
using System.Drawing;
using System.Collections.Generic;
using AllianceCustomPicker;
using MonoTouch.UIKit;

namespace Sample
{
	public partial class SampleViewController : UIViewController
	{
		public SampleViewController () : base ("SampleViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{ 
			base.DidReceiveMemoryWarning (); 
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad (); 
			this.Title = "Alliance Custom Picker";
			this.EdgesForExtendedLayout = UIRectEdge.None;


			//List Items
			BtnSearch.TouchUpInside += (object sender, EventArgs e) => {
				var items = new List<string> {
					"Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut"
				};
				var picker = new AlliancePicker (this);
				picker.PlainPickerItems = items;
				picker.SourceField = TxtListItems;
				picker.Type = PickerType.List;
				picker.HeaderTitle = "Choose State";
				picker.Show ();
			};

			//Date Picker
			BtnDate.TouchUpInside += (object sender, EventArgs e) => {
				var picker1 = new AlliancePicker (this);
				picker1.SourceField = TxtDatePicker;
				picker1.Type = PickerType.Date;
				picker1.DateFormat = SelectDateFormat.DMY;
				picker1.Seperator = "-";
				picker1.BorderColor = (UIColor.Brown).CGColor;
				picker1.BackgrondColor = UIColor.Brown;
				picker1.ChooseButtonColor = UIColor.Blue;
				picker1.PlusMinusColor = UIColor.White;
				picker1.IsFullMonthRequired = true;
				picker1.HeaderTitle = "Choose Date";
				picker1.Show ();
			};

			//Time Picker
			BtnTime.TouchUpInside += (object sender, EventArgs e) => {
				var picker2 = new AlliancePicker (this);
				picker2.SourceField = TxtTimePicker;
				picker2.Seperator = ":";
				picker2.Type = PickerType.Time;
				picker2.BorderColor = (UIColor.Magenta).CGColor;
				picker2.BackgrondColor = UIColor.Magenta;
				picker2.ChooseButtonColor = UIColor.Blue;
				picker2.PlusMinusColor = UIColor.Black;
				picker2.HeaderTitle = "Choose Time";
				picker2.Show ();
			};

			//Date Time Picker
			BtnDateTime.TouchUpInside += (object sender, EventArgs e) => {
				var picker2 = new AlliancePicker (this);
				picker2.SourceField = TxtDateTimePicker;
				picker2.Seperator = "/";
				picker2.Type = PickerType.DateTime;
				picker2.PickerFont = UIFont.FromName ("AvenirNext", 13);
				picker2.BorderColor = (UIColor.Green).CGColor;
				picker2.BackgrondColor = UIColor.Green;
				picker2.ChooseButtonColor = UIColor.Blue;
				picker2.PlusMinusColor = UIColor.Black;
				picker2.HeaderTitle = "Choose Date Time";
				picker2.Show ();
			};

			//Plain Event
			BtnPlainEvent.TouchUpInside += (object sender, EventArgs e) => {
				var picker2 = new AlliancePicker (this);
				picker2.Type = PickerType.Plain;
				picker2.HeaderTitle = "Sample Description";
				picker2.PlainDescription = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in" +
					"voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
				picker2.Show ();
			};

			//Login
			BtnLogin.TouchUpInside += (object sender, EventArgs e) => {
				var picker2 = new AlliancePicker (this);
				picker2.Type = PickerType.Login;
				picker2.LoginNameString = "Authenticate";
				picker2.ChooseButtonColor = UIColor.DarkGray;
				picker2.UserNameMaxLength = 8;
				picker2.PasswordMaxLength = 8;
				picker2.LoginClicked += (object s1, EventArgs e1) => {
					UIAlertView view = new UIAlertView ("Login", "User Name : " + picker2.UserName + "\n" + "Password : " + picker2.Password, null, "Ok", null);
					view.Show ();
				};
				picker2.Show ();
			};

		}
	}
}

