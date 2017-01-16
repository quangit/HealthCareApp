using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HealthCare.Core.ViewModels;
using System.Diagnostics;

namespace HealthCare.Phone.Views.HomeTab
{
    public partial class ChbaseTab : UserControl
    {
        private HomeViewModel _vm => DataContext as HomeViewModel;
        public ChbaseTab()
        {
            InitializeComponent();
            var vm = this.DataContext as HomeViewModel;

			webview.Source = new Uri(_vm.CHBASE_URL);
        }
    }
}