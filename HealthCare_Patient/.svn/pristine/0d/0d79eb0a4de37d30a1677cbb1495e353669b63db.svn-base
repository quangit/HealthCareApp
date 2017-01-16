using HealthCare.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Resx;
using HealthCare.ViewModels.CHBases;
using Xamarin.Forms;

namespace HealthCare.Pages.CHBases
{
    public partial class ImmunizationPage : TopBarContentPage
    {
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ToolbarItems.Clear();
            await Task.Delay(1000);
            ToolbarItems.Add(new ToolbarItem() { Text = AppResources.add_new, Icon = new FileImageSource() { File = "ic_plug.png" }, Command = ImmunizationViewModel.Instance.GoToAddImmunizationPageCommand });
        }
        public ImmunizationPage()
        {
            InitializeComponent();
        }
    }
}
