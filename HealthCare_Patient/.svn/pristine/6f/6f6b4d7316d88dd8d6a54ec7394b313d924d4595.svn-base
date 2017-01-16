using System.Windows.Controls;
using HealthCare;
using HealthCare.Controls;
using HealthCare.WinPhone.Renderer;
using Microsoft.Phone.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Application = System.Windows.Application;
using Style = System.Windows.Style;
using Thickness = System.Windows.Thickness;

[assembly: ExportRenderer(typeof(EntryLogin), typeof(EntryCustomRenderer))]

namespace HealthCare.WinPhone.Renderer
{
    public class EntryCustomRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Control.Margin = new Thickness(0);
            foreach (var item in Control.Children)
            {
                if (item is PhoneTextBox)
                {
                    ((Control)item).Style = (Style)Application.Current.Resources[
                        ((EntryLogin)e.NewElement).IsLogin ? "LoginTextBoxStyle" : "TextBoxStyle"];
                }
                else if (item is PasswordBox && ((EntryLogin)e.NewElement).IsLogin)
                {
                    ((PasswordBox)item).Style = (Style)Application.Current.Resources["PasswordBoxStyle"];
                }
                
                ((Control)item).FontSize = ((EntryLogin)e.NewElement).FontSize;
            }

        }
    }
}