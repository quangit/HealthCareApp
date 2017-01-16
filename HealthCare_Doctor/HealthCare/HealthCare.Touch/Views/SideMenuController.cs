
using System;

using Foundation;
using UIKit;
using HealthCare.Core.Resources;

namespace HealthCare.Touch.Views
{
	public partial class SideMenuController : UIViewController
	{

		public enum MenuButtonType{
			Schedule,
			Chbase,
			Consultancy,
			Handbook,
			Skype,
			CmeLibrary,
			Setting,
			Logout,
		};

		public Action<MenuButtonType> MenuButtonClicked;

		public SideMenuController () : base ("SideMenuController", null)
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
			SetupButtons();
		}

		private void SetupButtons(){
			MenuLabel.Text = AppResources.Home_Menu;
			MoreLabel.Text = AppResources.Home_More;

			ScheduleButton.SetTitle(AppResources.Schedule_Title,UIControlState.Normal);
			ScheduleButton.TouchUpInside += (sender, e) => ButtonClicked(MenuButtonType.Schedule);
			ScheduleButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			ChbaseButton.SetTitle(AppResources.CHbase_Title,UIControlState.Normal);
			ChbaseButton.TouchUpInside += (sender, e) => ButtonClicked(MenuButtonType.Chbase);
			ChbaseButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			ConsultancyButton.SetTitle(AppResources.Consult_Title,UIControlState.Normal);
			ConsultancyButton.TouchUpInside += (sender, e) => ButtonClicked(MenuButtonType.Consultancy);
			ConsultancyButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			HandbookButton.SetTitle(AppResources.Checkups_Title,UIControlState.Normal);
			HandbookButton.TouchUpInside += (sender, e) => ButtonClicked(MenuButtonType.Handbook);
			HandbookButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			SkypeButton.SetTitle(AppResources.WeakTopics_Title,UIControlState.Normal);
			SkypeButton.TouchUpInside += (sender, e) => ButtonClicked(MenuButtonType.Skype);
			SkypeButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			CmeLibraryButton.SetTitle(AppResources.CmeLibrary_Title,UIControlState.Normal);
			CmeLibraryButton.TouchUpInside += (sender, e) => ButtonClicked(MenuButtonType.CmeLibrary);
			CmeLibraryButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;

			SettingButton.SetTitle(AppResources.Settings,UIControlState.Normal);
			SettingButton.TouchUpInside += (sender, e) => ButtonClicked(MenuButtonType.Setting);
			SettingButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			LogoutButton.SetTitle(AppResources.Logout,UIControlState.Normal);
			LogoutButton.TouchUpInside += (sender, e) => ButtonClicked(MenuButtonType.Logout);
			LogoutButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;

		}

		public void ButtonClicked (MenuButtonType whichButton){
			if (MenuButtonClicked != null) {
				MenuButtonClicked (whichButton);
			}
		}


	}
}

