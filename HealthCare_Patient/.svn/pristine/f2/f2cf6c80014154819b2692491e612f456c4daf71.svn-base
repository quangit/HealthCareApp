using HealthCare.Conveters;
using HealthCare.Enums;
using HealthCare.Helpers;
using HealthCare.Resx;
using Xamarin.Forms;

namespace HealthCare.Controls.ViewCells
{
    public partial class CheckupViewCell : ViewCell
    {
        public CheckupViewCell()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            dynamic context = BindingContext;

            if (context != null)
            {
                MapCheckupStatus(context.Status, context.IsPaid, context.IsRated);
            }
        }

        private void MapCheckupStatus(CheckupStatus status, bool isPaid, bool isRated)
        {
            if (status == CheckupStatus.Accepted)
            {
                LeftLine.BackgroundColor =
                    lbDoctorName.TextColor = isPaid
                        ? HcStyles.CheckupAcceptedColor
                        : HcStyles.CheckupPaymentPendingColor;
                lbTime.IsVisible = true;
                //stlTimeLeft.IsVisible = true;
                GridContent.Children.Add(CreateStackTimeLeft());
                stlRecjectedMessage.IsVisible = false;
            }
            else if (status == CheckupStatus.Pending)
            {
                LeftLine.BackgroundColor =
                    lbDoctorName.TextColor = HcStyles.CheckupPendingColor;
                lbTime.IsVisible = true;
                //stlTimeLeft.IsVisible = true;
                GridContent.Children.Add(CreateStackTimeLeft());
                stlRecjectedMessage.IsVisible = false;
            }
            else if (status == CheckupStatus.Rejected)
            {
                LeftLine.BackgroundColor =
                    lbDoctorName.TextColor = HcStyles.CheckupPendingColor;
                lbTime.IsVisible = false;
                //stlTimeLeft.IsVisible = false;
                stlRecjectedMessage.IsVisible = true;
            }
            else if (status == CheckupStatus.Finished)
            {
                LeftLine.BackgroundColor =
                    lbDoctorName.TextColor = HcStyles.CheckupFinishColor;
                lbTime.IsVisible = true;
                //stlTimeLeft.IsVisible = false;
                stlRecjectedMessage.IsVisible = false;
                GridContent.Children.Add(CreateStackRating(isRated));
            }
        }

        private StackLayout CreateStackTimeLeft()
        {
            var img = new Image
            {
                Source = new FileImageSource { File = "ic_alert.png" },
                HeightRequest = Common.OnPlatform<double>(12, 12, 16),
                WidthRequest = Common.OnPlatform<double>(12, 12, 16),
            };

            var lb = new Label
            {
                Style = HcStyles.LabelSubContentStyle,
            };
            lb.BindingContext = this.BindingContext;
            lb.SetBinding(Label.TextProperty,
                new Binding("Schedule.StartDateTime",
                converter: new TimeLeftConverter()));

            var stl = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.End,
                Children = { img, lb }
            };

            Grid.SetRow(stl, 2);
            Grid.SetColumn(stl, 2);

            return stl;
        }

        private StackLayout CreateStackRating(bool isRate)
        {
            //stack not rated
            var img = new Image
            {
                Source = new FileImageSource { File = "ic_alert.png" },
                HeightRequest = Common.OnPlatform<double>(12, 12, 16),
                WidthRequest = Common.OnPlatform<double>(12, 12, 16),
            };

            var lb = new Label
            {
                Style = HcStyles.LabelSubContentStyle,
                BindingContext = this.BindingContext,
                Text = AppResources.need_rating
            };

            var stlNotRated = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.End,
                Children = { img, lb }
            };

            var rating = new RatingControl
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                IsEnabled = false,
                WidthRequest = Common.OnPlatform<double>(80, 200, 200)
            };
            rating.SetBinding(RatingControl.ValueProperty, new Binding("Rating"));

            var stl = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.End
            };
            if (isRate)
            {
                GridContainer.HeightRequest = Common.OnPlatform<double>(100, 120, 182);
                stl.Children.Add(rating);
            }
            else stl.Children.Add(stlNotRated);

            Grid.SetRow(stl, 2);
            Grid.SetColumn(stl, 1);
            Grid.SetColumnSpan(stl, 2);

            return stl;
        }
    }
}