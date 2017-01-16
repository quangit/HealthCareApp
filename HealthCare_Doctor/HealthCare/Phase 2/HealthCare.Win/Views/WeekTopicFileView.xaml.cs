using System;
using Windows.System;
using Windows.UI.Xaml.Controls;
using HealthCare.Core.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HealthCare.Win.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WeekTopicFileView : Page
    {
        public WeekTopicFileView()
        {
            this.InitializeComponent();
        }

        private async void WeekTopicList_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var file = e.ClickedItem as TopicFiles;
            if (file != null)
                await Launcher.LaunchUriAsync(new Uri(file.FileUri, UriKind.RelativeOrAbsolute));
        }
    }
}
