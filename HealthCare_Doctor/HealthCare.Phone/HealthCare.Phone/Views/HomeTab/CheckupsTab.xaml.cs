using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using HealthCare.Core.Models;
using HealthCare.Core.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls;

namespace HealthCare.Phone.Views.HomeTab
{
    public partial class CheckupsTab : UserControl
    {
        private HomeViewModel _vm => DataContext as HomeViewModel;
        public CheckupsTab()
        {
            InitializeComponent();

            this.CheckupList.SetValue(InteractionEffectManager.IsInteractionEnabledProperty, true); InteractionEffectManager.AllowedTypes.Add(typeof(RadDataBoundListBoxItem));
            //this.Loaded += CheckupsTab_Loaded;
            CheckupList.DataVirtualizationMode = DataVirtualizationMode.OnDemandAutomatic;
            CheckupList.DataRequested += OnDataRequested;
        }

        private async void OnDataRequested(object sender, EventArgs e)
        {
            
            var m = await _vm.LoadCheckups();
           

        }

        //private async void CheckupsTab_Loaded(object sender, RoutedEventArgs e)
        //{
        //   // var b = await (this.DataContext as HomeViewModel).LoadCheckups();
        //}

        //private async void RadDataBoundListBox_OnDataRequested(object sender, EventArgs e)
        //{

        //    ((RadDataBoundListBox)sender).DataVirtualizationMode = DataVirtualizationMode.None;
        //}


        private void ItemTapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var r = ((FrameworkElement)sender).DataContext as Checkup;
            if (r != null)
            {
                (this.DataContext as HomeViewModel).CheckupViewCommand.Execute(r);
            }
        }

        private async void Checkup_OnRefreshRequested(object sender, EventArgs e)
        {
            await (this.DataContext as HomeViewModel).LoadCheckups(true);
            CheckupList.StopPullToRefreshLoading(true);
        }
    }
}
