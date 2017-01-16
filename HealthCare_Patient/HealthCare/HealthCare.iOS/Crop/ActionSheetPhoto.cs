using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Foundation;
using UIKit;

namespace HealthCare.iOS.Crop
{
    public class ActionSheetPhoto : UIActionSheet, INotifyPropertyChanged
    {
        public CropData CroppingData;

        private readonly Class0 class0_0;

        private UIImage uiimage_0;

        private PropertyChangedEventHandler propertyChangedEventHandler_0;

        public UIImage SelectedImage
        {
            get
            {
                return this.uiimage_0;
            }
            private set
            {
                this.uiimage_0 = value;
                this.OnPropertyChanged("SelectedImage");
            }
        }

        public ActionSheetPhoto(UIViewController viewController)
        {
            if (this.class0_0 == null)
            {
                this.class0_0 = new Class0(viewController);
            }
            if (this.CroppingData == null)
            {
                this.CroppingData = new CropData();
            }
            this.method_0();
        }

        private void method_0()
        {
            this.AddButton("Take photo".smethod_0());
            this.AddButton("Choose  photo".smethod_0());
            this.AddButton("Cancel".smethod_0());
            this.CancelButtonIndex = (2);
            base.Clicked += new EventHandler<UIButtonEventArgs>(this.method_1);

            //base.Clicked(new EventHandler<UIButtonEventArgs>(this.method_1));
        }

        private void method_1(object sender, UIButtonEventArgs e)
        {
            if (e.ButtonIndex != 0)
            {
                if (e.ButtonIndex == 1)
                {
                        this.class0_0.method_3((NSDictionary nsdictionary_0) => {
                        UIImage uIImage = nsdictionary_0.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
                        if (uIImage != null)
                        {
                            this.method_2(this.CroppingData, uIImage);
                        }
                    });
                }
                return;
            }
            //if (!Class2.smethod_0())
            //{
            //    (new UIAlertView("Simulator Error", "This can't run in the simulator", null, "Ok", null)).Show();
            //    return;
            //}
            this.class0_0.method_2((NSDictionary nsdictionary_0) => {
                UIImage uIImage = nsdictionary_0.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
                this.method_2(this.CroppingData, uIImage);
            });
        }

        public void method_2(CropData cropData_0, UIImage uiimage_1)
        {
            this.class0_0.method_0().NavigationBarHidden = (true);
            this.class0_0.method_0().NavigationBar.Translucent = (true);
            Control1 control1 = new Control1(cropData_0, uiimage_1);
            this.class0_0.method_0().PushViewController(control1, true);
            control1.method_3((UIImage argument0) => this.SelectedImage = argument0);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler propertyChangedEventHandler0 = this.propertyChangedEventHandler_0;
            if (propertyChangedEventHandler0 != null)
            {
                propertyChangedEventHandler0(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                PropertyChangedEventHandler propertyChangedEventHandler;
                PropertyChangedEventHandler propertyChangedEventHandler0 = this.propertyChangedEventHandler_0;
                do
                {
                    propertyChangedEventHandler = propertyChangedEventHandler0;
                    PropertyChangedEventHandler propertyChangedEventHandler1 = (PropertyChangedEventHandler)System.Delegate.Combine(propertyChangedEventHandler, value);
                    propertyChangedEventHandler0 = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.propertyChangedEventHandler_0, propertyChangedEventHandler1, propertyChangedEventHandler);
                }
                while ((object)propertyChangedEventHandler0 != (object)propertyChangedEventHandler);
            }
            remove
            {
                PropertyChangedEventHandler propertyChangedEventHandler;
                PropertyChangedEventHandler propertyChangedEventHandler0 = this.propertyChangedEventHandler_0;
                do
                {
                    propertyChangedEventHandler = propertyChangedEventHandler0;
                    PropertyChangedEventHandler propertyChangedEventHandler1 = (PropertyChangedEventHandler)System.Delegate.Remove(propertyChangedEventHandler, value);
                    propertyChangedEventHandler0 = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.propertyChangedEventHandler_0, propertyChangedEventHandler1, propertyChangedEventHandler);
                }
                while ((object)propertyChangedEventHandler0 != (object)propertyChangedEventHandler);
            }
        }
    }
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
    {
        public string ParameterName
        {
            get;
            private set;
        }

        public NotifyPropertyChangedInvocatorAttribute()
        {
        }

        public NotifyPropertyChangedInvocatorAttribute(string parameterName)
        {
            this.ParameterName = parameterName;
        }
    }
}
