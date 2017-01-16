using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.Views;
using HealthCare.Core.Models;
using HealthCare.Core.ViewModels;
using HealthCare.Droid.Utilities;
using HealthCare.Core.Resources;
using Lyft.Scissors;
using Android.Graphics;
using System.IO;
using Android.Provider;

namespace HealthCare.Droid.Views
{
     [Register("healthcare.droid.views.UpdateProfileView"), Activity()]
    public class UpdateProfileView : MvxActionBarActivity
     {

         private UpdateProfileViewModel vm;

        protected override int LayoutResource
        {
            get { return Resource.Layout.UpdateProfileView; }
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetIcon(Resource.Drawable.logo_bs);

            SupportActionBar.Title = AppResources.ApplicationTitle;
            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnCreate(Bundle bundle)
         {
             base.OnCreate(bundle);
             vm = ViewModel as UpdateProfileViewModel;

             vm.PropertyChanged += vm_PropertyChanged;



            // var gender = FindViewById<MvxSpinner>(Resource.Id.spinnerSex).ItemsSource = 
             FindViewById<TextView>(Resource.Id.spinnerSex).Click += (s, e) =>
             {
                 vm.Account.Gender = new Gender() { IsFemale = !vm.Account.Gender.IsFemale };
             };
             var birthDate = vm.Account.BirthDate;
            if (birthDate != null)
                FindViewById<TextView>(Resource.Id.textDOB).Text = ((DateTime)birthDate).ToShortDateString();
            else
                FindViewById<TextView>(Resource.Id.textDOB).Text = DateTime.Now.ToShortDateString();
            FindViewById<TextView>(Resource.Id.textDOB).Click += (sender, e) =>
            {
                MyDatePickerDialog d = new MyDatePickerDialog((date) =>
                {
                    vm.Account.BirthDate = date;
                    FindViewById<TextView>(Resource.Id.textDOB).Text = date.ToShortDateString();
                });
                d.Show(this.FragmentManager, null);
            };





            FindViewById<MvxImageView>(Resource.Id.profileImage).Click += (sender, e) =>
             {
                 var imageIntent = new Intent();
                 imageIntent.SetType("image/*");
                 imageIntent.SetAction(Intent.ActionGetContent);
                 StartActivityForResult(
                 Intent.CreateChooser(imageIntent, "Select photo"), 0);
             };


            FindViewById<ImageButton>(Resource.Id.rotateBut).Enabled =FindViewById<ImageButton>(Resource.Id.checkBut).Enabled = false;


        }

         protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
         {
             base.OnActivityResult(requestCode, resultCode, data);
             if (resultCode == Result.Ok)
             {
                FindViewById<LinearLayout>(Resource.Id.contentLayout).Visibility = ViewStates.Gone;
                FindViewById<FrameLayout>(Resource.Id.ImageFix).Visibility = ViewStates.Visible;
                var cropView = FindViewById<CropView>(Resource.Id.crop_view);
                Bitmap img  = PictureHelpers.LoadScaledBitmap(data.Data);
                cropView.SetImageBitmap(img);
                //cropView.SetScaleType(ImageView.ScaleType.CenterCrop);
                var rotateBut = FindViewById<ImageButton>(Resource.Id.rotateBut);
                var cropBut = FindViewById<ImageButton>(Resource.Id.cropBut);
                var checkBut = FindViewById<ImageButton>(Resource.Id.checkBut);
                var resultImg = FindViewById<ImageView>(Resource.Id.resultImage);
                cropBut.Click += (sender, e) =>
                {
                    img = cropView.Crop();
                    resultImg.SetImageBitmap(img);
                    cropView.Visibility = ViewStates.Gone;
                    resultImg.Visibility = ViewStates.Visible;
                    cropBut.Enabled = false;
                    checkBut.Enabled  = rotateBut.Enabled = true;
                };

               
                int curRotate = 0;
                rotateBut.Click += (sender, e) => {
                    curRotate -= 90;
                    var mat = new Matrix();
                    mat.PostRotate(curRotate);
                    var bMapRotate = Bitmap.CreateBitmap(img, 0, 0, img.Width, img.Height, mat, true);
                    cropView.SetImageBitmap(bMapRotate);
                    resultImg.SetImageBitmap(bMapRotate);
                };

                checkBut.Click += (sender, e) => {
                    img = cropView.Crop();
                    FindViewById<FrameLayout>(Resource.Id.ImageFix).Visibility = ViewStates.Gone;
                    FindViewById<LinearLayout>(Resource.Id.contentLayout).Visibility = ViewStates.Visible;
                    FindViewById<ImageView>(Resource.Id.profileImage).SetImageBitmap(img);
                    string path = MediaStore.Images.Media.InsertImage(this.ContentResolver, img, "Title", null);
                    vm.Account.Images = PictureHelpers.LoadInMemoryBitmap(Android.Net.Uri.Parse(path)).ToArray();
                    FindViewById<MvxImageView>(Resource.Id.avatarImage).Visibility = ViewStates.Gone;
                    cropView.Visibility = ViewStates.Visible;
                    resultImg.Visibility = ViewStates.Gone;
                    FindViewById<ImageButton>(Resource.Id.rotateBut).Enabled = FindViewById<ImageButton>(Resource.Id.checkBut).Enabled = false;
                    cropBut.Enabled = true;
                };

                // cropView.SetImageResource(Resource.Drawable.logo_bs);
                //cropView.SetImageURI(data.Data);


                //vm.OnPicture(PictureHelpers.LoadInMemoryBitmap(data.Data));


            }
         }

        

        void vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
         {
             if (e.PropertyName=="Cities")
             {
                 if (vm.Cities != null && vm.Account.City != null)
                 {
                     var spinnerCity = FindViewById<MvxSpinner>(Resource.Id.spinnerCity);
                     int cityPosition = vm.Cities.FindIndex(x => x.Id == vm.Account.CityId);
                     spinnerCity.SetSelection(cityPosition);
                     spinnerCity.ItemSelected += OnItemSelected;
                 }
             }

             if (e.PropertyName == "CheckupTypes")
             {
                 if (vm.CheckupTypes != null)
                 {
                     var spinnerCheckup = FindViewById<MvxSpinner>(Resource.Id.spinnerCheckup);
                     int checkupPosition = vm.CheckupTypes.FindIndex(x => { return vm.Account.CheckupType != null && x.Id == vm.Account.CheckupType.Id; });
                     spinnerCheckup.SetSelection(checkupPosition);
                 }
             }
         }

       

         private void OnItemSelected(object sender, AdapterView.ItemSelectedEventArgs itemSelectedEventArgs)
         {
             vm.Districts = vm.Cities[itemSelectedEventArgs.Position].Districts;
             //vm.Account.District 
             int disPosition = vm.Districts.FindIndex(x => x.Id == vm.Account.DistrictId);
             FindViewById<MvxSpinner>(Resource.Id.spinnerDistrict).SetSelection(disPosition);
         }
    }
}