﻿#pragma checksum "C:\Dropbox\HealthCare - Doctor\HealthCare.Phone\HealthCare.Phone\Views\CmeClassView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5D1F656667D388249F50E68B5B9CCB66"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Cirrious.MvvmCross.WindowsPhone.Views;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace HealthCare.Phone.Views {
    
    
    public partial class CmeClassView : Cirrious.MvvmCross.WindowsPhone.Views.MvxPhonePage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.WebBrowser WebBrowser;
        
        internal System.Windows.Controls.Button BackButton;
        
        internal System.Windows.Controls.Button ForwardButton;
        
        internal System.Windows.Controls.Button HomeButton;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/HealthCare.Phone;component/Views/CmeClassView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.WebBrowser = ((Microsoft.Phone.Controls.WebBrowser)(this.FindName("WebBrowser")));
            this.BackButton = ((System.Windows.Controls.Button)(this.FindName("BackButton")));
            this.ForwardButton = ((System.Windows.Controls.Button)(this.FindName("ForwardButton")));
            this.HomeButton = ((System.Windows.Controls.Button)(this.FindName("HomeButton")));
        }
    }
}

