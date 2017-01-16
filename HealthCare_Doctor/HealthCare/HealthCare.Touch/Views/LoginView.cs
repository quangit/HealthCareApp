using System;
using System.Drawing;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;

namespace HealthCare.Touch.Views
{
	public partial class LoginView : BaseViewController
    {
        public LoginView() : base("LoginView", null, true)
        {
            //TODO

        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }
		private void SetLeftViewImage(UITextField tf, string image, int width, int height)
		{
			var imageView = new UIImageView(UIImage.FromBundle(image));
			imageView.Frame = new CoreGraphics.CGRect (0, 0, width, height);
			imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			tf.LeftView = imageView;
			tf.LeftViewMode = UITextFieldViewMode.Always;
		}
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


			SetLeftViewImage (PhoneTF, "cellphone.png", 18, 18);
			SetLeftViewImage (PassTF, "lock.png", 18, 18);

			this.AddBindings (new Dictionary<object, string> () {
				{ TitleLabel, "Text [SignIn_Title]" },
				{ PassTF, "Text Password,Mode=TwoWay;Placeholder [SignUp_Password]" },
				{ PhoneTF, "Text UserName,Mode=TwoWay;Placeholder [SignUp_UserName]" },
				{ RemeberLabel, "Text [SignIn_Remeber]" },
				{ RememberSwitch,"On Remember,Mode=TwoWay" },
				{ RegisterButton, "Title [SignIn_Register]; TouchUpInside SignUpCommand" },
				{ LoginButton, "Title [SignIn_Login]; TouchUpInside LoginCommand" },
				{ ResetButton, "Title [LoginView_ResetPass]; TouchUpInside ResetCommand" },

			});
            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}