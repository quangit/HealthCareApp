using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using CoreGraphics;
using Foundation;
using HealthCare.Core.Services;
using HealthCare.Core.ViewModels;
using HealthCare.Touch.Utilities;
using UIKit;
using HealthCare.Core.Resources;
using System.Linq;

namespace HealthCare.Touch.Views
{
    public partial class SignUpView : BaseViewController
    {
        private UIImagePickerController  _imagePicker;

        public SignUpView() : base("SignUpView", null, true)
        {
        }

        private SignUpViewModel MyViewModel
        {
            get { return ((SignUpViewModel)ViewModel); }
        }

        private void SetLeftViewImage(UITextField tf, string image, int width, int height)
        {
            var imageView = new UIImageView(UIImage.FromBundle(image));
            imageView.Frame = new CGRect(0, 0, width, height);
            imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            tf.LeftView = imageView;
            tf.LeftViewMode = UITextFieldViewMode.Always;
        }

        private void ShowLoading()
        {
            if (MyViewModel.Loading)
            {
                Setup.DoError(new ErrorEventArgs(null, ErrorType.StartProgress));
            }
            else
            {
                Setup.DoError(new ErrorEventArgs(null, ErrorType.StopProgress));
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ShowLoading();
            MyViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Loading")
                {
                    ShowLoading();
                }
            };
            SetLeftViewImage(PhoneTF, "cellphone.png", 18, 18);
            SetLeftViewImage(PassTF, "lock.png", 18, 18);
            SetLeftViewImage(RePassTF, "lock.png", 18, 18);
            SetLeftViewImage(FirstNameTF, "ic1.png", 18, 18);
            SetLeftViewImage(LastNameTF, "ic1.png", 18, 18);
            SetLeftViewImage(EmailTF, "ic4.png", 18, 18);

            this.AddBindings(new Dictionary<object, string>
            {
                {TitleLabel, "Text [SignUp_Title]"},
                {PassTF, "Text Account.Password,Mode=TwoWay;Placeholder [SignUp_Password]"},
                {PhoneTF, "Text Account.Phone,Mode=TwoWay;Placeholder [SignUp_Phone]"},
                {RePassTF, "Text Account.RePass,Mode=TwoWay;Placeholder [SignUp_RetypePassword]"},
                {EmailTF, "Text Account.Email,Mode=TwoWay;Placeholder [SignUp_Email]"},
                {FirstNameTF, "Text Account.FirstName,Mode=TwoWay;Placeholder [SignUp_FirstName]"},
                {LastNameTF, "Text Account.LastName,Mode=TwoWay;Placeholder [SignUp_LastName]"},
                {RegisterButton, "Title [SignIn_Register]; TouchUpInside SignUpCommand"},
                {BackButton, "Title [SignIn_Login]; TouchUpInside BackCommand"},
            });
			
            // Perform any additional setup after loading the view, typically from a nib.
        }

    }
}