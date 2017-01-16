using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using HealthCare.Core.Models;
using HealthCare.Core.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Reactive;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace HealthCare.Phone.Views.HomeTab
{
    public partial class ConsultTab : UserControl
    {
        private HomeViewModel _vm => DataContext as HomeViewModel;
        public ConsultTab()
        {
            //Telerik.Windows.Controls.InputLocalizationManager.Instance.ResourceManager = Core.Resources.AppResources.ResourceManager;

            InitializeComponent();
            
            ConsultList.SetValue(InteractionEffectManager.IsInteractionEnabledProperty, true);
            InteractionEffectManager.AllowedTypes.Add(typeof(RadDataBoundListBoxItem));

            ConsultList.DataVirtualizationMode = DataVirtualizationMode.OnDemandAutomatic;
            ConsultList.DataRequested += ConsultList_DataRequested;
        }

        private async void ConsultList_DataRequested(object sender, EventArgs e)
        {            
            var r = await _vm.LoadSupportRequests();
           
        }



        private void UIElement_OnTap(object sender, GestureEventArgs e)
        {
            _vm.ShowConsultCommand.Execute(((FrameworkElement)sender).DataContext);
        }

        private async void ConsultList_OnRefreshRequested(object sender, EventArgs e)
        {
            var vm = this.DataContext as HomeViewModel;
            if (vm != null)
            {
                await vm.LoadSupportRequests(true);
            }
            ConsultList.StopPullToRefreshLoading(true);           
        }
    }
}
