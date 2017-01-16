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
    public partial class ProcedurePage : TopBarContentPage
    {
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ToolbarItems.Clear();
            await Task.Delay(1000);
            ToolbarItems.Add(new ToolbarItem() { Text = AppResources.add_new, Icon = new FileImageSource() { File = "ic_plug.png" }, Command = ProcedureViewModel.Instance.GoToAddProcedurePageCommand });
        }
        public ProcedurePage()
        {
            InitializeComponent();
        }

        
    }
}
