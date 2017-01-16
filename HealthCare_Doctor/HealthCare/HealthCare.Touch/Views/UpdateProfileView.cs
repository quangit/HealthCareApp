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
    public partial class UpdateProfileView : BaseViewController
    {
        private MvxImageViewLoader _logoloader;

        public UpdateProfileView() : base("UpdateProfileView", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        private UIImagePickerController _imagePicker;

        private UpdateProfileViewModel MyViewModel
        {
            get { return ((UpdateProfileViewModel)ViewModel); }
        }

        private void SetLeftViewImage(UITextField tf, string image, int width, int height)
        {
            var imageView = new UIImageView(UIImage.FromBundle(image));
            imageView.Frame = new CGRect(20, 0, width, height);
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

            // Perform any additional setup after loading the view, typically from a nib.
            _logoloader = new MvxImageViewLoader(() => ProfileImage);

            ShowLoading();
            MyViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Loading")
                {
                    ShowLoading();
                }
            };
            SetLeftViewImage(PhoneTF, "cellphone.png", 18, 18);
            SetLeftViewImage(FirstNameTF, "ic1.png", 18, 18);
            SetLeftViewImage(LastNameTF, "ic1.png", 18, 18);
            SetLeftViewImage(BobTF, "ic2.png", 18, 18);
            SetLeftViewImage(GenderTF, "ic3.png", 18, 18);
            SetLeftViewImage(EmailTF, "ic4.png", 18, 18);
            SetLeftViewImage(IdTF, "ic5.png", 18, 18);
            SetLeftViewImage(AddrTF, "ic6.png", 18, 18);
            SetLeftViewImage(CityTF, "ic7.png", 18, 18);
            SetLeftViewImage(DistrictTF, "ic7.png", 18, 18);
            SetLeftViewImage(CheckupTF, "ic8.png", 18, 18);

            this.AddBindings(new Dictionary<object, string>
                {
                    {TitleLabel, "Text [UpdateProfile_Title]"},
                    {PhoneTF, "Text Account.Phone,Mode=TwoWay;Placeholder [SignUp_Phone]"},
                    {EmailTF, "Text Account.Email,Mode=TwoWay;Placeholder [SignUp_Email]"},
                    {AddrTF, "Text Account.Address,Mode=TwoWay;Placeholder [SignUp_Address]"},
                    {IdTF, "Text Account.IdNo,Mode=TwoWay;Placeholder [SignUp_SID]"},
                    {FirstNameTF, "Text Account.FirstName,Mode=TwoWay;Placeholder [SignUp_FirstName]"},
                    {LastNameTF, "Text Account.LastName,Mode=TwoWay;Placeholder [SignUp_LastName]"},
                    {SaveButton, "Title [SignUp_SaveButton]; TouchUpInside SaveCommand"},
                    {BobTF, "Text Account.BirthDay; Placeholder [SignUp_Birthday]"},
                    {CityTF, "Text Account.City.Name; Placeholder [SignUp_City]"},
                    {DistrictTF, "Text Account.District.Name; Placeholder [SignUp_District]"},
                    {GenderTF, "Text Account.Gender; Placeholder [SignUp_Gender]"},
                    {CheckupTF, "Text Account.CheckupType; Placeholder [SignUp_CheckUp]"},
                    {_logoloader,"ImageUrl Account.Photo"}
                });

            var datePicker = new UIDatePicker(CGRect.Empty);
            datePicker.Mode = UIDatePickerMode.Date;
            datePicker.ValueChanged +=
				(s, e) => { ((UpdateProfileViewModel)ViewModel).Account.BirthDate = Util.NSDateToDateTime(datePicker.Date); };
            BobTF.InputView = datePicker;


            var bindindSet = this.CreateBindingSet<UpdateProfileView, UpdateProfileViewModel>();
            //SetListPicker(bindindSet, CityTF, "Cities", "Account.City", "Account.City.Name");
            CityTF.ShouldBeginEditing = field =>
            {
				_alliancePicker = MyPicker.Create(AppResources.SignUp_City, this, MyViewModel.Cities.Select(x => x.Name).ToList(), (newText, index) =>
                {
                    MyViewModel.Account.City = MyViewModel.Cities[index];
                    MyViewModel.Account.City.Name = newText;
                });
                return false;
            };

            //SetListPicker(bindindSet, DistrictTF, "Account.City.Districts", "Account.District", "Account.District.Name");
            DistrictTF.ShouldBeginEditing = field =>
            {
                if (MyViewModel.Account.City != null)
                {
					_alliancePicker = MyPicker.Create(AppResources.SignUp_District, this, MyViewModel.Account.City.DistrictNames, (newText, index) =>
                    {
                        MyViewModel.Account.District = MyViewModel.Account.City.Districts[index];
                        MyViewModel.Account.District.Name = newText;
                    });
                }
                return false;
            };

            //SetListPicker(bindindSet, CheckupTF, "CheckupTypes", "Account.CheckupType", "Account.CheckupType");
            CheckupTF.ShouldBeginEditing = field =>
            {
				_alliancePicker = MyPicker.Create(AppResources.SignUp_CheckUp, this, MyViewModel.CheckupTypes.Select(x => x.ToString()).ToList(), (newText, index) =>
                {
                    MyViewModel.Account.CheckupType = MyViewModel.CheckupTypes[index];
                    //MyViewModel.Account.District.Name = newText;
                });
                return false;
            };

            //SetListPicker(bindindSet, GenderTF, "Genders", "Account.Gender", "Account.Gender");
            GenderTF.ShouldBeginEditing = field =>
            {
				_alliancePicker = MyPicker.Create(AppResources.SignUp_Gender, this, MyViewModel.Genders.Select(x => x.ToString()).ToList(), (newText, index) =>
                {
                    MyViewModel.Account.Gender = MyViewModel.Genders[index];
                    //MyViewModel.Account.District.Name = newText;
                });
                return false;
            };



			ImageButton.TouchUpInside += SetProfileImage;

            Title = Core.Resources.AppResources.UpdateProfile_Title;

        }

		private void SetProfileImage(object sender,EventArgs e){

			_imagePicker = new UIImagePickerController
			{
				SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
				MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary)
			};
			_imagePicker.FinishedPickingMedia += OnFinishedPickingMedia;
			_imagePicker.Canceled += OnCanceled;
			NavigationController.PresentModalViewController(_imagePicker, true);
		}


        private async void OnFinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            try
            {
                var isImage = false;
                switch (e.Info[UIImagePickerController.MediaType].ToString())
                {
                    case "public.image":
                        Console.WriteLine("Image selected");
                        isImage = true;
                        break;
                    case "public.video":
                        Console.WriteLine("Video selected");
                        break;
                }

                // get common info (shared between images and video)
                var referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
                if (referenceURL != null)
                    Console.WriteLine("Url:" + referenceURL);

				// dismiss the picker
				_imagePicker.DismissModalViewController(true);
                // if it was an image, get the other image info
                if (isImage)
                {
                    // get the original image
                    var originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
                    if (originalImage != null)
                    {
                        // do something with the image
                        Console.WriteLine("got the original image");
                        

						var imageEditor = new ImageEditor{Image = originalImage};

						NavigationController.PushViewController(imageEditor, true);
						imageEditor.OnFinish += (sender1, e1) => {
							MyViewModel.Account.Images = imageEditor.Image.AsJPEG().ToArray();
							ProfileImage.Image = imageEditor.Image; // display
						};
                    }
                }
                else
                {
                    // if it's a video
                    // get video url
                    var mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
                    if (mediaURL != null)
                    {
                        Console.WriteLine(mediaURL.ToString());
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void OnCanceled(object sender, EventArgs e)
        {
            try
            {
                _imagePicker.DismissModalViewController(true);
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void SetListPicker(MvxFluentBindingDescriptionSet<UpdateProfileView, UpdateProfileViewModel> bindingSet,
            UITextField tf,
            string itemsSourcePath,
            string selectedItemPath,
            string textPath)
        {
            var picker = new UIPickerView();

            var pickerViewModel = new MvxPickerViewModel(picker);

            tf.InputView = picker;

            picker.Model = pickerViewModel;
            picker.ShowSelectionIndicator = true;
            bindingSet.Bind(pickerViewModel).For(p => p.ItemsSource).To(itemsSourcePath);
            bindingSet.Bind(pickerViewModel).For(p => p.SelectedItem).To(selectedItemPath);
            bindingSet.Bind(tf).To(textPath);
            bindingSet.Apply();
            View.AddGestureRecognizer(new UITapGestureRecognizer(() => tf.ResignFirstResponder()));
        }
    }
}

