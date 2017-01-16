using System;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using HealthCare.Touch.Views.Tabs;
using HealthCare.Core.Resources;
using HealthCare.Core.ViewModels;
using ObjCRuntime;
using MenuButtonType = HealthCare.Touch.Views.SideMenuController.MenuButtonType;
using SidebarNavigation;
using LumiaLoyalty.Touch.Utilities;
using Foundation;

namespace HealthCare.Touch.Views
{
	public class HomeView : BaseViewController, IUIGestureRecognizerDelegate
    {
        private int SelectedIndex;
        private UIViewController[] ViewControllers;
        private UIViewController SelectedViewController;

        private UIBarButtonItem _menuButton;

        private UIBarButtonItem _addScheduleButton;
        public SidebarNavigation.SidebarController SidebarController { get; private set; }

        private HomeViewModel _vm
        {
            get
            {
                return base.ViewModel as HomeViewModel;
            }
        }

        public HomeView() : base(null, null)
        {
            // need this additional call to ViewDidLoad because UIkit creates the view before the C# hierarchy has been constructed

            // because the UIKit base class does ViewDidLoad, we have to make a second call here

            //ViewDidLoad();
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

		[Export ("gestureRecognizer:shouldRecognizeSimultaneouslyWithGestureRecognizer:")]
		public bool ShouldRecognizeSimultaneously (UIKit.UIGestureRecognizer gestureRecognizer, UIKit.UIGestureRecognizer otherGestureRecognizer)
		{
			return true;
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationItem.SetHidesBackButton(true, false);

            _menuButton = new UIBarButtonItem(new UIImage("menu_icon.png"), UIBarButtonItemStyle.Plain, null);
            _addScheduleButton = new UIBarButtonItem(new UIImage("calendar_add.png"), UIBarButtonItemStyle.Plain, null);
            _addScheduleButton.Clicked += (s, e) =>
            {
                ((HomeViewModel)ViewModel).ShowScheduleAddingCommand.Execute(null);
            };
            _menuButton.Clicked += (sender, e) =>
            {
                SidebarController.ToggleMenu();
            };
            NavigationItem.SetLeftBarButtonItems(new[] { _spacer, _menuButton }, false);


            if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
                EdgesForExtendedLayout = UIRectEdge.None;


            var viewControllers = new UIViewController[]
            {
                CreateTabFor(AppResources.Schedule_Title, "Schedule", new ScheduleHomeTab()),
                CreateTabFor(AppResources.Consult_Title, "Consultancy", new ConsultHomeTab()),
                CreateTabFor(AppResources.Checkups_Title, "Handbook", new CheckUpHomeTab()),
				CreateTabFor(AppResources.WeakTopics_Title, "Skype", new WeekTopicHomeTab()),
				CreateTabFor(AppResources.WeakTopics_Title, "Skype", new CmeLibraryHomeTab()),
				CreateTabFor(AppResources.Settings, "Setting", new SettingTab()),
				CreateTabFor(AppResources.CHbase_Title, "Chbase", new ChbaseHomeTab()),

            };


            ViewControllers = viewControllers;
            SelectedIndex = 0;
            SelectedViewController = ViewControllers[SelectedIndex];
            var sideMenuController = new SideMenuController();
            sideMenuController.MenuButtonClicked = MenuButtonClicked;

            SidebarController = new SidebarController(this, SelectedViewController, sideMenuController);
            SidebarController.MenuWidth = 220;
            SidebarController.ReopenOnRotate = false;
            SidebarController.MenuLocation = SidebarController.MenuLocations.Left;


            MainView_ViewControllerSelected();

        }


        private void MainView_ViewControllerSelected()
        {
            var index = SelectedIndex;
            UIBarButtonItem[] buttonItems;

            switch (index)
            {
                case 0:
				buttonItems = new[] {  _spacer, _addScheduleButton };
                    break;
                //			case 1:
                //				buttonItems = new[] { _spacer, _addVoucherButton };
                //				break;
                //			case 2:
                //				buttonItems = new[] { _spacer, _searchButton };
                //				break;
                default:
                    buttonItems = new[] { _spacer };
                    break;
            }

            SetRightBarButtonItems(buttonItems);

            SelectedViewController = ViewControllers[SelectedIndex];
            SidebarController.ChangeContentView(ViewControllers[SelectedIndex]);
            NavigationController.Title = ViewControllers[SelectedIndex].Title;
            NavigationItem.Title = Core.Resources.AppResources.ApplicationTitle;
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                ForegroundColor = UIColor.White,
            };
//            if (SelectedIndex == 2)
//            {
//                var r = this.ViewModel as HomeViewModel;
//                if (r != null)
//                    r.LoadCheckups();
//            }
			var t = SidebarController.ContentAreaController.View;
			if (t.GestureRecognizers != null && t.GestureRecognizers.Length > 0)
				foreach (var g in t.GestureRecognizers)
					g.Delegate = this;

        }

        private void MenuButtonClicked(MenuButtonType whichButton)
        {

            switch (whichButton)
            {
                case MenuButtonType.Schedule:
                    SelectedIndex = 0;
                    break;
                case MenuButtonType.Consultancy:
                    SelectedIndex = 1;
                    break;
                case MenuButtonType.Handbook:
                    SelectedIndex = 2;
                    break;
                case MenuButtonType.Skype:
                    SelectedIndex = 3;
                    break;
				case MenuButtonType.CmeLibrary:
					SelectedIndex = 4;
					break;
                //			case MenuButtonType.Bill:
                //				SelectedIndex = 4;
                //				break;
                //			case MenuButtonType.FeaturedDeal:
                //				SelectedIndex = 5;
                //				break;
                case MenuButtonType.Setting:
                    SelectedIndex = 5;
                    break;
				case MenuButtonType.Chbase:
					SelectedIndex = 6;
					break;                case MenuButtonType.Logout:
                    LogoutButton_Clicked();
                    break;
            }

            MainView_ViewControllerSelected();
        }

        private void SettingButton_Clicked()
        {

        }

        private void LogoutButton_Clicked()
        {

            MessageBox.ShowOKCancel(AppResources.MainPage_logoutAppBar, AppResources.Logout_contentMessageBox, result =>
            {
                if (result)
                    _vm.DoLogoutTap();
            });
        }

        #region Create Tab
        private int _createdSoFarCount = 0;

        private UIViewController CreateTabFor(string title, string imageName, MvxViewController screen)
        {
            title = title.TrimEnd('>');
            var controller = new UINavigationController();
            //this.CreateViewControllerFor(viewModel) as MvxViewController;
            screen.ViewModel = ViewModel;
            SetTitleAndTabBarItem(screen, title, imageName);
            if (screen != null)
            {
                screen.View.AutoresizingMask = UIViewAutoresizing.FlexibleHeight; //was needed because mvvm is enabling autostretch automatically? 

                controller.PushViewController(screen, false);
            }
            //screen.EdgesForExtendedLayout = UIRectEdge.All;

            //screen.View.LayoutMargins = new UIEdgeInsets (0, 0 ,200 , 0);
            return controller;
        }

        private void SetTitleAndTabBarItem(UIViewController screen, string title, string imageName)
        {
            screen.Title = title;
            //			bool retina = (View.Frame.Height > 480f);
            //			var imgName = imageName + (retina ? "@2" : "") + ".png";
            var tabbarItem = new UITabBarItem(title, UIImage.FromBundle(imageName),
                _createdSoFarCount);
            //tabbarItem.ImageInsets = new UIEdgeInsets(6, 0, -6, 0);
            screen.TabBarItem = tabbarItem;
            _createdSoFarCount++;
        }

        public void ShowGrandChild(IMvxTouchView view)
        {
            //var currentNav = SelectedViewController as UINavigationController;
            //currentNav.PushViewController(view as UIViewController, true);
        }
        #endregion
    }
}

