﻿#pragma checksum "C:\Dropbox\HealthCare - Doctor\HealthCare.Phone\HealthCare.Phone\Views\HomeTab\WeekTopicTab.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "24BE55640C00EAB53900FFF70AA5B7F3"
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
using Telerik.Windows.Controls;


namespace HealthCare.Phone.Views.HomeTab {
    
    
    public partial class WeekTopicTab : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Primitives.ToggleButton ToggleButtonAll;
        
        internal System.Windows.Controls.Primitives.ToggleButton ToggleButtonOn;
        
        internal System.Windows.Controls.Primitives.ToggleButton ToggleButtonOff;
        
        internal Telerik.Windows.Controls.RadDataBoundListBox WeekTopicList;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/HealthCare.Phone;component/Views/HomeTab/WeekTopicTab.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ToggleButtonAll = ((System.Windows.Controls.Primitives.ToggleButton)(this.FindName("ToggleButtonAll")));
            this.ToggleButtonOn = ((System.Windows.Controls.Primitives.ToggleButton)(this.FindName("ToggleButtonOn")));
            this.ToggleButtonOff = ((System.Windows.Controls.Primitives.ToggleButton)(this.FindName("ToggleButtonOff")));
            this.WeekTopicList = ((Telerik.Windows.Controls.RadDataBoundListBox)(this.FindName("WeekTopicList")));
        }
    }
}

