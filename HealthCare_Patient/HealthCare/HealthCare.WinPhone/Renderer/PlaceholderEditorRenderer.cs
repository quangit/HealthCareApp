using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using HealthCare;
using HealthCare.ViewModels;
using HealthCare.WinPhone.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Color = System.Windows.Media.Color;
using Editor = Xamarin.Forms.Editor;

[assembly: ExportRenderer(typeof(PlaceholderEditor), typeof(PlaceholderEditorRenderer))]
namespace HealthCare.WinPhone.Renderer
{
    public class PlaceholderEditorRenderer : EditorRenderer
    {
        private bool isDelegated = false;
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            var editor = e.NewElement as PlaceholderEditor;
            if (Control != null && editor != null && !isDelegated)
            {
                Control.Text = editor.Placeholder;
                Control.Foreground = new SolidColorBrush(Color.FromArgb(200, 130, 130, 130));
                Control.FontSize = MoreSupportViewModel2.FontLegalNoticeContent;

                Control.GotFocus += (sender, args) =>
                {
                    if (!string.IsNullOrWhiteSpace(Control.Text) && Control.Text.Equals(editor.Placeholder))
                    {
                        Control.Text = "";
                        Control.Foreground = new SolidColorBrush(Colors.Black);
                    }
                };

                Control.LostFocus += (sender, args) =>
                {
                    if (string.IsNullOrWhiteSpace(Control.Text))
                    {
                        Control.Foreground = new SolidColorBrush(Color.FromArgb(200, 130, 130, 130));
                        Control.Text = editor.Placeholder;
                    }
                };
            }


        }
    }
}
