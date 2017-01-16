using Cirrious.MvvmCross.Touch.Views;
using Foundation;
using UIKit;
using System.Diagnostics;
//using Pakaze.Touch.Utilities;
using System.Collections.Generic;
using System;
//using Pakaze.Core.Models;
using CoreGraphics;
using HealthCare.Touch.Utilities;
using HealthCare.Core.Models;
using LumiaLoyalty.Touch.Utilities;
using HealthCare.Core.Resources;
using AllianceCustomPicker;

namespace HealthCare.Touch.Views
{
    public class BaseViewController : MvxViewController
    {

		private static UIBarButtonItem _callButton;
		public UIBarButtonItem _spacer;
        private bool _navigationBarHidden;
        public BaseViewController(string view, NSBundle o, bool navigationBarHidden = false)
            : base(view, o)
        {
            //Debug.WriteLine (view + " " + navigationBarHidden);
            _navigationBarHidden = navigationBarHidden;
        }

        public BaseViewController()
        {
        }

		protected AlliancePicker _alliancePicker;
		private UIView _firstResponder;
		public override void WillRotate (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{

			if (_firstResponder != null) {
				_firstResponder.ResignFirstResponder ();
				MoveTextFieldUp (false);
			}

			if(ModalBgView != null && ModalBgView.Superview != null)
				ModalBgView.RemoveFromSuperview();
			if (_alliancePicker != null) {
				_alliancePicker.Hide ();
				_alliancePicker = null;
			}

			base.WillRotate (toInterfaceOrientation, duration);

			View.Frame = GetViewFrame();

			if (origin != null)
				origin = View.Frame;
		
		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			
			base.DidRotate (fromInterfaceOrientation);
			//GetViewFrame (origin);
//			if (_firstResponder != null)
//				_firstResponder.BecomeFirstResponder ();
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            origin = CGRect.Empty;
			if (!_navigationBarHidden) {

				_spacer = new UIBarButtonItem(UIBarButtonSystemItem.FixedSpace, null);
				_spacer.Width = -15;

				if (_callButton == null) {
					_callButton = new UIBarButtonItem (new UIImage ("phone_sup.png"), UIBarButtonItemStyle.Plain, null);
					_callButton.Clicked += (sender, e) => {
						MessageBox.ShowOKCancel (AppResources.Warning, AppResources.AppBar_CallMessage, result => {
							if (result)
								UIApplication.SharedApplication.OpenUrl (NSUrl.FromString ("tel://" + Data.SupportPhone));
						});
					};
				}

				NavigationItem.SetRightBarButtonItems ( new[] {_spacer, _callButton }, true);
			}


        }

		public void SetRightBarButtonItems(UIBarButtonItem[] buttonItems){
			if (!_navigationBarHidden) {
				var newButtonItems = new List<UIBarButtonItem> (buttonItems);
				newButtonItems.Insert (0,_callButton);
				newButtonItems.Insert (0,_spacer);
				NavigationItem.SetRightBarButtonItems (newButtonItems.ToArray (), true);
			}
		}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            if (NavigationController != null)
            {
                NavigationController.NavigationBar.Translucent = false;
                NavigationController.NavigationBar.Hidden = _navigationBarHidden;

				NavigationController.NavigationBar.BackgroundColor = UIColor.Blue;
				NavigationController.NavigationBar.BarTintColor = Util.UIColorFromHex("#FF2CBE71");
				NavigationController.NavigationBar.TintColor = UIColor.White;
            }
            TextFieldObserver = NSNotificationCenter.DefaultCenter.AddObserver(UITextField.TextDidBeginEditingNotification, TextFieldBegin);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            if (TextFieldObserver != null)
                NSNotificationCenter.DefaultCenter.RemoveObserver(TextFieldObserver);
        }

        private NSObject TextFieldObserver;
		private UIView FirstResponder;
        private UIToolbar DismissKeyboardToolbar;
        private CGRect origin;

		public void TextFieldBegin(NSNotification notification)
		{
			TextFieldBegin (notification.Object as UIView);
		}
		public void TextFieldBegin(UIView view)
        {
			FirstResponder = view;
			_firstResponder = FirstResponder;
            //Console.WriteLine("TextFieldBegin " + notification.ToString());
            if (DismissKeyboardToolbar == null)
            {
				DismissKeyboardToolbar = new UIToolbar(new CoreGraphics.CGRect(0, 0, GetViewFrame().Width, 40));
                DismissKeyboardToolbar.Items = new[] {
                    new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace),
                    new UIBarButtonItem (UIBarButtonSystemItem.Done, delegate {
                        if (FirstResponder != null){
                            FirstResponder.ResignFirstResponder ();
                            MoveTextFieldUp (false);
							_firstResponder = null;
                        }
                    })
                };
            }
            else
            {
				

				DismissKeyboardToolbar.Frame = new CGRect(0,0, GetViewFrame().Width,40);

                DismissKeyboardToolbar.Items[1] = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
                {
                    if (FirstResponder != null)
                    {
                        FirstResponder.ResignFirstResponder();
                        MoveTextFieldUp(false);
                    }
                });
            }
			var ftf = (FirstResponder as UITextField);
			if(ftf != null)
				ftf.InputAccessoryView = DismissKeyboardToolbar;
			var ftv = (FirstResponder as UITextView);
			if(ftv != null)
				ftv.InputAccessoryView = DismissKeyboardToolbar;
			MoveTextFieldUp(true,FirstResponder is UITextField);
        }

		public CGRect GetViewFrame(CGRect view = default (CGRect)){
			if (view == default (CGRect))
				view = View.Frame;
			double width, height;
			if (InterfaceOrientation == UIInterfaceOrientation.Portrait) {
				width = Math.Min (view.Width, view.Height);
				height = Math.Max (view.Width, view.Height);

			} else {
				width = Math.Max (view.Width, view.Height);
				height = Math.Min (view.Width, view.Height);
			}

			var x = (width == view.Width) ?  view.X : view.Y;
			var y = (width == view.Width) ?  view.Y : view.X;

			Debug.WriteLine ("GetViewFrame(): w - " + width + ", h - " + height);
			return new CGRect(x,y,width, height);
		}

		public CGRect GetFirstResponderFrame(){
			double width, height;
			var responder = FirstResponder;
			if (responder == null)
				return CGRect.Empty;
			if (InterfaceOrientation == UIInterfaceOrientation.Portrait) {
				width = Math.Min (responder.Bounds.Width, responder.Bounds.Height);
				height = Math.Max (responder.Bounds.Width, responder.Bounds.Height);

			} else {
				width = Math.Max (responder.Bounds.Width, responder.Bounds.Height);
				height = Math.Min (responder.Bounds.Width, responder.Bounds.Height);
			}

			var x = (width == responder.Bounds.Width) ?  responder.Frame.X : responder.Frame.Y;
			var y = (width == responder.Bounds.Width) ?  responder.Frame.Y : responder.Frame.X;

			Debug.WriteLine ("GetFirstResponderFrame(): w - " + width + ", h - " + height);
			return new CGRect(x,y,width, height);
		}

		public void MoveTextFieldUp(bool up = true,bool tf =true )
        {
            if (origin == CGRect.Empty)
                origin = View.Frame;
            else
                View.Frame = origin;
            int animatedDistance;

            int moveUpValue = (int)(FirstResponder.Frame.Y + FirstResponder.Frame.Size.Height) + 20;
            UIInterfaceOrientation orientation =
                UIApplication.SharedApplication.StatusBarOrientation;
            if (orientation == UIInterfaceOrientation.Portrait ||
                orientation == UIInterfaceOrientation.PortraitUpsideDown)
            {

                animatedDistance = 216 - (460 - moveUpValue - 5);
            }
            else
            {
                animatedDistance = 162 - (320 - moveUpValue - 5);
            }


            if (animatedDistance > 0)
            {
				int movementDistance = tf ? animatedDistance : moveUpValue;
                const float movementDuration = 0.3f;
                int movement = (up ? -movementDistance : movementDistance);
                UIView.BeginAnimations(string.Empty, System.IntPtr.Zero);
                UIView.SetAnimationBeginsFromCurrentState(true);
                UIView.SetAnimationDuration(movementDuration);

                if (ChildViewControllers.Length == 0)
                {// only shift up the child view
                    if (up)
                    {
                        var frame = View.Frame;
                        frame.Y += movement;
                        View.Frame = frame;
                    }
                }
                //View.Frame.Offset (0, -100);//movement);     
                UIView.CommitAnimations();
            }
        }


        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public UIButton ModalExitButton;
        public UIView ModalBgView;
        public void ShowModal(UIView modalView, bool animated = true, Action onCancelAction = null)
        {
            if (ModalBgView == null)
            {
                ModalBgView = //_singleSwipeVC.View;
                    new UIView
                    {
                        BackgroundColor = UIColor.Black.ColorWithAlpha(0.5f),
                        //Center = SingleBrowseView.Center
                        //AutoresizingMask = UIViewAutoresizing.None
                    };
            }
            var margin = 35;
            var x = margin / 2;
            var y = margin / 2;
            var width = View.Frame.Width - margin;
            var height = View.Frame.Height - margin;
			var frame = new CoreGraphics.CGRect(x, y, width, height);
			var buttonSize = margin - 5;

            if (ModalExitButton == null)
            {
                ModalExitButton = new UIButton(UIButtonType.RoundedRect)
                {
                    Frame = new CoreGraphics.CGRect(x - buttonSize / 2 + width, y - buttonSize / 2, buttonSize, buttonSize),
                    BackgroundColor = UIColor.White
                };

                ModalExitButton.SetTitle("X", UIControlState.Normal);
                ModalExitButton.TitleLabel.Font = UIFont.BoldSystemFontOfSize(buttonSize * 0.625f);
                ModalExitButton.Layer.CornerRadius = buttonSize / 2;
                ModalExitButton.Layer.BorderColor = ModalExitButton.TitleLabel.TextColor.CGColor;
                ModalExitButton.Layer.BorderWidth = 2;
                ModalExitButton.TouchUpInside += (sender, e) =>
                {
                    ModalBgView.RemoveFromSuperview();
                    if (onCancelAction != null)
                        onCancelAction();
                };

            }

			ModalExitButton.Frame = new CoreGraphics.CGRect (x - buttonSize / 2 + width, y - buttonSize / 2, buttonSize, buttonSize);

            
            ModalBgView.Frame = View.Bounds;
            ModalBgView.AddSubview(modalView);
            ModalBgView.AddSubview(ModalExitButton);
            ModalBgView.Transform = View.Transform;
			modalView.Frame = frame;

            View.AddSubview(ModalBgView);
            View.BringSubviewToFront(ModalBgView);

            if (!animated) return;

            ModalBgView.Alpha = 0;
            FluentAnimate.Linear(0.5f, () => ModalBgView.Alpha = 1).Start();
        }

		public void ShowModal(UIView modalView, nfloat height = default(nfloat),bool top = false, bool animated = true)
		{
			if (ModalBgView == null) 
			{
				ModalBgView = //_singleSwipeVC.View;
					new UIView {
					BackgroundColor = UIColor.Black.ColorWithAlpha (0.5f),
					//Center = SingleBrowseView.Center
					//AutoresizingMask = UIViewAutoresizing.None
				};
			}
			var margin = 35;
			var x = margin / 2;
			nfloat y = margin / 2;
			var width = View.Frame.Width - margin;
			if (height == default(nfloat))
				height = View.Frame.Height - margin;
			else {
				y = (View.Frame.Height - margin - height ) / 2;
			}

			y = top ? 20 : y;
			var frame = new CoreGraphics.CGRect (x,y,width,height);
			var buttonSize = margin - 5;
			if (ModalExitButton == null) {
				ModalExitButton = new UIButton (UIButtonType.RoundedRect){
					Frame = new CoreGraphics.CGRect (x - buttonSize/2 + width, y - buttonSize/2,buttonSize,buttonSize),
					BackgroundColor = UIColor.White
				};


				ModalExitButton.SetTitle ("X",UIControlState.Normal);
				ModalExitButton.TitleLabel.Font = UIFont.BoldSystemFontOfSize(buttonSize * 0.625f);
				ModalExitButton.Layer.CornerRadius = buttonSize / 2;
				ModalExitButton.Layer.BorderColor = ModalExitButton.TitleLabel.TextColor.CGColor;
				ModalExitButton.Layer.BorderWidth = 2;
				ModalExitButton.TouchUpInside += (sender, e) => {ModalBgView.RemoveFromSuperview();};

			}
			ModalExitButton.Frame = new CoreGraphics.CGRect (x - buttonSize / 2 + width, y - buttonSize / 2, buttonSize, buttonSize);



			ModalBgView.Frame = View.Bounds;
			ModalBgView.AddSubview (modalView);
			ModalBgView.AddSubview (ModalExitButton);
			ModalBgView.Transform = View.Transform;
			modalView.Frame = frame;
			View.AddSubview(ModalBgView);
			View.BringSubviewToFront(ModalBgView);

			if (!animated) return;

			ModalBgView.Alpha = 0;
			FluentAnimate.Linear(0.5f, () => ModalBgView.Alpha = 1).Start();
		}


        private UIPickerView picker;
        private UIToolbar toolbar;
        public void BuildPickerView()
        {
            PickerModel model = new PickerModel(new List<string>() { "Travel", "Food", "Retail", "Favourite", "Teach", "Emergency" });

            if (picker == null)
            {
                picker = new UIPickerView();
            }
            picker.ShowSelectionIndicator = true;
            picker.BackgroundColor = UIColor.White;
            picker.Model = model;
            picker.Hidden = false; ;

            //__________________
            // Setup the toolbar
            if (toolbar == null)
            {
                toolbar = new UIToolbar();
            }
            toolbar.BarStyle = UIBarStyle.Black;
            toolbar.Translucent = false;
            toolbar.SizeToFit();
            toolbar.Hidden = false;

            UIButton doneButton = new UIButton(UIButtonType.Custom);
            doneButton.Frame = new CoreGraphics.CGRect(260, 8, 50, 30);
            doneButton.SetTitle("Done", UIControlState.Normal);
            doneButton.BackgroundColor = UIColor.White;
            doneButton.Layer.CornerRadius = 5f;
            doneButton.SetTitleColor(UIColor.Black, UIControlState.Normal);

            doneButton.TouchUpInside += (sender, e) =>
            {
                toolbar.RemoveFromSuperview();
                picker.RemoveFromSuperview();
            };

            UILabel titleLabel = new UILabel();
            titleLabel.BackgroundColor = UIColor.Clear;
            titleLabel.TextColor = UIColor.LightTextColor;
            titleLabel.Frame = new CoreGraphics.CGRect(5, 13, 200, 20);
            titleLabel.Text = "Choose Category:";

            toolbar.AddSubview(titleLabel);
            toolbar.AddSubview(doneButton);

            this.View.AddSubview(toolbar);
            this.View.AddSubview(picker);

        }
    }

    public class PickerModel : UIPickerViewModel
    {
        private readonly List<string> values;

        public event EventHandler PickerChanged;

        public int selectedCategoryID = 0;

        public PickerModel(List<string> collection)
        {
            this.values = collection;
        }

        public override nint GetComponentCount(UIPickerView picker)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView picker, nint component)
        {
            return values.Count;
        }

        public override string GetTitle(UIPickerView picker, nint row, nint component)
        {
            return values[(int)row];
        }

        public override nfloat GetRowHeight(UIPickerView picker, nint component)
        {
            return 40f;
        }

        public override void Selected(UIPickerView picker, nint row, nint component)
        {

        }
    }
}

