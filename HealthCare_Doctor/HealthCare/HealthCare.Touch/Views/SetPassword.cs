using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.Binding.BindingContext;
using CoreGraphics;
using UIKit;

namespace HealthCare.Touch.Views
{
    public partial class SetPassword : BaseViewController
    {
        public SetPassword() : base("SetPassword", null)
        {
        }
        private void SetLeftViewImage(UITextField tf, string image, int width, int height)
        {
            var imageView = new UIImageView(UIImage.FromBundle(image));
            imageView.Frame = new CGRect(20, 0, width, height);
            imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            tf.LeftView = imageView;
            tf.LeftViewMode = UITextFieldViewMode.Always;
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetLeftViewImage(OldPasswordTF, "lock.png", 18, 18);
            SetLeftViewImage(NewPasswordTF, "lock.png", 18, 18);
            SetLeftViewImage(ConfirmTF, "lock.png", 18, 18);

            this.AddBindings(new Dictionary<object, string>
            {
                {NewPasswordTF, "Text NewPassword,Mode=TwoWay;Placeholder [SetPassword_New]"},
                {ConfirmTF, "Text ConfirmPassword,Mode=TwoWay;Placeholder [SetPassword_Confirm]"},
                {OldPasswordTF, "Text OldPassword,Mode=TwoWay;Placeholder [SetPassword_Old]"},
				{SubmitBarButton, "Title [SetPassword_Change]; Clicked SetPasswordCommand"},
            });
			
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


