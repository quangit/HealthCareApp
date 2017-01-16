using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Controls;
using HealthCare.Resx;
using HealthCare.ViewModels.CHBases;
using Xamarin.Forms;

namespace HealthCare.Pages.CHBases
{
    public partial class WeightPage : TopBarContentPage
    {
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ToolbarItems.Clear();
            await Task.Delay(1000);
            this.ToolbarItems.Add(new ToolbarItem() { Text = AppResources.add_new, Icon = new FileImageSource { File = "ic_plug.png" }, Command = WeightViewModel.Instance.GoToAddNewCommand });
        }
        public WeightPage()
        {
            InitializeComponent();
            
        }
    }
}
