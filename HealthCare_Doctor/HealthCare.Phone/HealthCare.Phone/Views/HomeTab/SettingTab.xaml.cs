using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Xml;
using HealthCare.Core.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace HealthCare.Phone.Views.HomeTab
{
    public partial class SettingTab : UserControl
    {
        public SettingTab()
        {
            InitializeComponent();
            this.Loaded += SettingTab_Loaded;
        }
        public static string GetAppVersion()
        {
            try
            {
                var xmlReaderSettings = new XmlReaderSettings
                {
                    XmlResolver = new XmlXapResolver()
                };

                using (var xmlReader = XmlReader.Create("WMAppManifest.xml", xmlReaderSettings))
                {
                    xmlReader.ReadToDescendant("App");

                    return xmlReader.GetAttribute("Version");
                }
            }
            catch (Exception)
            {
                
                //throw;
            }
            return string.Empty;
        }
        private void SettingTab_Loaded(object sender, RoutedEventArgs e)
        {
            ((HomeViewModel) DataContext).RaisePropertyChanged("Doctor");

            versionText.Text = GetAppVersion();
        }

        private void UIElement_OnTap(object sender, GestureEventArgs e)
        {
            ((HomeViewModel) DataContext).UpdateProfileCommand.Execute(null);
        }
    }
}
