using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ExifLib;
using HealthCare.Pages;
using HealthCare.ViewModels;
using HealthCare.WinPhone.Renderer;
using Microsoft.Phone.Shell;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Grid = System.Windows.Controls.Grid;
using Image = System.Windows.Controls.Image;
using Page = Xamarin.Forms.Page;

[assembly: ExportRenderer(typeof(PhotoEditPage), typeof(CropImagePageRenderer))]
namespace HealthCare.WinPhone.Renderer
{
    public class CropImagePageRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                PhotoEditViewModel.Instance.CreateToolBar(e.NewElement);
                var cropControl = new CropUserControl(UserViewModel.Instance.AvatarByteArray);
                SetNativeControl(new Grid { Children = { cropControl } });
            }
        }
    }
}
