﻿#pragma checksum "C:\Dropbox\HealthCare - Doctor\HealthCare\HealthCare.Phone\Controls\MyTextBox.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8751C771149D4E9026DEDADF118675DB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace HealthCare.Phone.Controls {
    
    
    public partial class MyTextBox : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.UserControl UserControl;
        
        internal System.Windows.Controls.TextBlock HintTextBlock;
        
        internal System.Windows.Controls.TextBox MainTextBox;
        
        internal System.Windows.Controls.Viewbox Spinner;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/HealthCare.Phone;component/Controls/MyTextBox.xaml", System.UriKind.Relative));
            this.UserControl = ((System.Windows.Controls.UserControl)(this.FindName("UserControl")));
            this.HintTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("HintTextBlock")));
            this.MainTextBox = ((System.Windows.Controls.TextBox)(this.FindName("MainTextBox")));
            this.Spinner = ((System.Windows.Controls.Viewbox)(this.FindName("Spinner")));
        }
    }
}

