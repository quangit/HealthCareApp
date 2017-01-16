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
using Telerik.Windows.Controls;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace HealthCare.Phone.Views.HomeTab
{
    public partial class CmeLibraryTab : UserControl
    {
        public CmeLibraryTab()
        {
            InitializeComponent();
        }

        private void UIElement_OnTap(object sender, GestureEventArgs e)
        {
            var vm = this.DataContext as HomeViewModel;
            if (vm.IsBusy)
                return;
            var s = sender as Grid;
            if (vm != null && s != null) vm.CmeCommand.Execute(s.DataContext);

        }

        private void RadTextBox_OnActionButtonTap(object sender, EventArgs e)
        {
            var vm = this.DataContext as HomeViewModel;
            vm.CmeCategoriesSearch = (sender as RadTextBox).Text;
        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = this.DataContext as HomeViewModel;
            vm.CmeCategoriesSearch = (sender as RadTextBox).Text;
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as HomeViewModel;
            vm.ShowViewModel<CmeSearchViewModel>();
        }
    }
}
