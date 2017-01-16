using System;
using Foundation;
using UIKit;

namespace LumiaLoyalty.Touch.Utilities
{
    public enum MessageBoxButton { Ok, OkCancel, YesNo, Input }
    public static class MessageBox
    {

        private static NSObject nsObject;

        public static void Initialize(NSObject nsObject)
        {
            MessageBox.nsObject = nsObject;
        }

        public static void Show(string title, string message, MessageBoxButton buttons, Action positiveCallback, Action negativeCallback)
        {
            nsObject.InvokeOnMainThread(() =>
            {
                UIAlertView alert = BuildAlert(title, message, buttons);
                alert.Clicked += (sender, buttonArgs) =>
                {
                    if (buttonArgs.ButtonIndex == 0)
                    {
                        positiveCallback();
                    }
                    else
                    {
                        negativeCallback();
                    }
                };

                alert.Show();
            });
        }

        public static void ShowOKCancel(string title, string message, Action<bool> Callback, MessageBoxButton button = MessageBoxButton.OkCancel)
        {
            nsObject.InvokeOnMainThread(() =>
            {
                UIAlertView alert = BuildAlert(title, message, button);
                alert.Clicked += (sender, buttonArgs) =>
                {
                    Callback(buttonArgs.ButtonIndex == 0);
                };

                alert.Show();
            });
        }

        public static void ShowInput(string title, string message, Action<string> positiveCallback)
        {
            nsObject.InvokeOnMainThread(() =>
            {
                UIAlertView alert = BuildAlert(title, message, MessageBoxButton.Input);
                alert.Clicked += (sender, buttonArgs) =>
                {
                    if (buttonArgs.ButtonIndex == 0)
                    {
                        positiveCallback(alert.GetTextField(0).Text);
                    }
                };

                alert.Show();

            });
        }

        public static void Show(string title, string message)
        {
            nsObject.InvokeOnMainThread(() =>
            {
                UIAlertView alert = BuildAlert(title, message, MessageBoxButton.Ok);
                alert.Show();
            });
        }

        public static void Show(string title, string message, MessageBoxButton buttons, Action positiveCallback)
        {
            nsObject.InvokeOnMainThread(() =>
            {
                UIAlertView alert = BuildAlert(title, message, buttons);
                alert.Clicked += (sender, buttonArgs) =>
                {
                    if (buttonArgs.ButtonIndex == 0)
                    {
                        positiveCallback();
                    }
                };

                alert.Show();

            });
        }

        private static UIAlertView BuildAlert(string title, string message, MessageBoxButton buttons)
        {
            switch (buttons)
            {
                case MessageBoxButton.Ok:
                    return new UIAlertView(title, message, null, "Ok", null);
                case MessageBoxButton.OkCancel:
                    return new UIAlertView(title, message, null, "Ok", "Cancel");
                case MessageBoxButton.Input:
                    {
                        var alert = new UIAlertView(title, message, null, "Ok", "Cancel");
                        alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
                        return alert;
                    }
                case MessageBoxButton.YesNo:
                    return new UIAlertView(title, message, null, "Yes", "No");
            }
            throw new NotImplementedException(buttons.ToString());
        }
    }
}