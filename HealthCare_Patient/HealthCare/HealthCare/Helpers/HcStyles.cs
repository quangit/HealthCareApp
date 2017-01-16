using Xamarin.Forms;

namespace HealthCare.Helpers
{
    public static class HcStyles
    {
        #region Margins, Paddings

        public static readonly Thickness ViewCellPadding = new Thickness(8, 8, Common.OnPlatform<double>(8, 8, 8), 0);

        public static readonly Thickness ViewCellHideScrollPadding = new Thickness(8, 8,
            Common.OnPlatform<double>(8, 8, 24), 0);

        public static readonly GridLength ViewCellHeight = new GridLength(Device.OnPlatform<double>(100, 110, 120));

        public static readonly GridLength HcGridLength =
            Device.OnPlatform(new GridLength(1, GridUnitType.Star),
                new GridLength(1, GridUnitType.Star),
                new GridLength(1, GridUnitType.Auto));

        #endregion

        #region Dimensions

        public static readonly double FontSizeTitle = Device.OnPlatform(16, 16,
            Device.GetNamedSize(NamedSize.Large, typeof (Label)));

        public static readonly double FontSizeSubTitle = Device.OnPlatform(16, 16,
            Device.GetNamedSize(NamedSize.Large, typeof (Label)) - 1);

        public static readonly double FontSizeContent = Device.OnPlatform(14, 14,
            Device.GetNamedSize(NamedSize.Medium, typeof (Label)));

        public static readonly double FontSizeSmall = Device.OnPlatform(12, 14,
           Device.GetNamedSize(NamedSize.Medium, typeof(Label)));

        public static readonly double FontSizeInverseContent = Device.OnPlatform(-16 - 1, -16 - 1,
            -Device.GetNamedSize(NamedSize.Medium, typeof (Label)) - 1);

        public static readonly double FontSizeSubContent = Device.OnPlatform(13, 13,
            Device.GetNamedSize(NamedSize.Small, typeof (Label)));

        public static readonly double EntryLoginFontSizeContent = Device.OnPlatform(14, 14,
            Device.GetNamedSize(NamedSize.Medium, typeof (Label)));

        public static readonly double ButtonHeight = Device.OnPlatform<double>(32, 40, 80);
        public static readonly double ButtonSearchBottomHeight = Device.OnPlatform<double>(32, 36, 80);
        public static readonly double ButtonInverseHeight = Device.OnPlatform<double>(-32, -32, -80);
        public static readonly double ImageButtonHeight = Device.OnPlatform<double>(32, 32, 70);

        public static readonly double ImageInEntryWidth = Device.OnPlatform<double>(13, 18, 18);
        public static readonly double ImageInEntryHeight = Device.OnPlatform<double>(13, 18, 18);

        public static readonly double ButtonFontSize = Device.OnPlatform(13, 13,
            Device.GetNamedSize(NamedSize.Medium, typeof (Button)));

        public static readonly double TopBarStyle1Height = Device.OnPlatform<double>(100, 100, 180);
        public static readonly double TopBarStyle2Height = Device.OnPlatform<double>(92, 92, 120);
        public static readonly double TopBarStyle3Height = Device.OnPlatform<double>(100, 100, 120);
        public static readonly double TopBarStyle4Height = Device.OnPlatform<double>(92, 92, 120);

        #endregion

        #region Colors

        public static readonly Color GreenButtonColor = Color.FromHex("#2cbe71");
        public static readonly Color DarkGreenButtonColor = Color.FromHex("#1c8071");
        public static readonly Color GreenLineColor = Color.FromHex("#04d967");
        public static readonly Color BlueFacebook = Color.FromHex("3B5998");
        public static readonly Color LightBlackTextColor = Color.FromHex("#44000000");
        public static readonly Color BlackTextColor = Color.FromHex("#AA000000");
        public static readonly Color GreenColor = Color.FromHex("#2CBE71");
        public static readonly Color BaseBackground = Color.FromHex("#EFEFF0");
        public static readonly Color BrandColor = Color.FromHex("#4169E1");
        public static readonly Color BorderGrayColor = Color.FromHex("#bbbaba");
        //public static readonly Color BorderLightGrayColor = Color.FromHex("#aad6d6d8");
        public static readonly Color BorderLightGrayColor = Color.FromHex("#aad6d6d8");
        public static readonly Color BorderBlueGrayColor = Color.FromHex("#93abae");
        public static readonly Color OrangeColor = Color.FromHex("#EF6521");
        public static readonly Color LightGrayTextColor = Color.FromHex("#ABADAE");
        public static readonly Color WhitePromotionButtonColor = Color.FromHex("#88ffffff");
        public static readonly Color OverlayImageButtonColor = Color.FromHex("#33ffffff");
        public static readonly Color RedCheckupColor = Color.FromHex("#f26522");
        public static readonly Color YellowCheckupColor = Color.FromHex("#fbc116");
        public static readonly Color GreenCheckupColor = Color.FromHex("#97c93d");
        public static readonly Color GreenCardColor = Color.FromHex("#2dbe6f");
        public static readonly Color DarkerGreenCardColor = Color.FromHex("#177a55");
        public static readonly Color GrayDisabledText = Color.FromHex("#aabbbaba");
        public static readonly Color QAColor = Color.FromHex("#EFEFF0");

        //Checkup colors
        public static readonly Color CheckupAcceptedColor = Color.FromHex("#97C93D");
        public static readonly Color CheckupPendingColor = Color.FromHex("#F26522");
        public static readonly Color CheckupPaymentPendingColor = Color.FromHex("#FBC116");
        public static readonly Color CheckupFinishColor = Color.FromHex("#C59336");

        public static readonly Color GreenLabelColor = Color.FromHex("#5EA226");
        public static readonly Color RedLabelColor = Color.FromHex("#FF0000");
        public static readonly Color GrayColor = Color.FromHex("#E4E4E4");
        public static readonly Color TextPayment = Color.FromHex("#ff9900");
        public static readonly Color ButtonPayment = Color.FromHex("#0086b3");

        #endregion

        #region Styles

        public static Style LabelTitleStyle
        {
            get
            {
                return new Style(typeof (Label))
                {
                    Setters =
                    {
                        new Setter {Property = Label.XAlignProperty, Value = TextAlignment.Start},
                        new Setter {Property = Label.TextColorProperty, Value = BlackTextColor},
                    }
                };
            }
        }

        public static Style LabelContentWordWrapStyle
        {
            get
            {
                return new Style(typeof (Label))
                {
                    BasedOn = LabelContentStyle,
                    Setters =
                    {
                        new Setter {Property = Label.LineBreakModeProperty, Value = LineBreakMode.WordWrap}
                    }
                };
            }
        }

        public static Style LabelSubTitleStyle
        {
            get
            {
                return new Style(typeof (Label))
                {
                    Setters =
                    {
                        new Setter {Property = Label.XAlignProperty, Value = TextAlignment.Start},
                        new Setter {Property = Label.TextColorProperty, Value = BlackTextColor},
                        new Setter {Property = Label.FontAttributesProperty, Value = FontAttributes.Bold},
                        new Setter {Property = Label.FontSizeProperty, Value = FontSizeSubTitle}
                    }
                };
            }
        }

        public static Style LabelContentStyle
        {
            get
            {
                return new Style(typeof (Label))
                {
                    Setters =
                    {
                        new Setter {Property = Label.XAlignProperty, Value = TextAlignment.Start},
                        new Setter {Property = Label.TextColorProperty, Value = BlackTextColor},
                        new Setter {Property = Label.FontSizeProperty, Value = FontSizeContent}
                    }
                };
            }
        }

        public static Style LabelSubContentStyle
        {
            get
            {
                return new Style(typeof (Label))
                {
                    Setters =
                    {
                        new Setter {Property = Label.XAlignProperty, Value = TextAlignment.Start},
                        new Setter {Property = Label.TextColorProperty, Value = BlackTextColor},
                        new Setter {Property = Label.FontSizeProperty, Value = FontSizeSubContent}
                    }
                };
            }
        }

        public static Style ButtonStyle
        {
            get
            {
                return new Style(typeof (Button))
                {
                    Setters =
                    {
                        new Setter {Property = Button.BorderWidthProperty, Value = 0},
                        new Setter {Property = Button.BorderColorProperty, Value = Color.Transparent},
                        new Setter {Property = Button.BorderRadiusProperty, Value = 0},
                        new Setter {Property = VisualElement.HeightRequestProperty, Value = ButtonHeight},
                        new Setter {Property = Button.FontSizeProperty, Value = ButtonFontSize},
                        new Setter {Property = VisualElement.BackgroundColorProperty, Value = GreenButtonColor},
                        new Setter {Property = Button.TextColorProperty, Value = Color.White}
                    }
                };
            }
        }

        public static Style ButtonForBottomSearchStyle
        {
            get
            {
                return new Style(typeof (Button))
                {
                    Setters =
                    {
                        new Setter {Property = Button.BorderWidthProperty, Value = 0},
                        new Setter {Property = Button.BorderColorProperty, Value = Color.Transparent},
                        new Setter {Property = Button.BorderRadiusProperty, Value = 0},
                        new Setter {Property = VisualElement.HeightRequestProperty, Value = ButtonSearchBottomHeight},
                        new Setter {Property = Button.FontSizeProperty, Value = ButtonFontSize},
                        new Setter {Property = VisualElement.BackgroundColorProperty, Value = GreenButtonColor},
                        new Setter {Property = Button.TextColorProperty, Value = Color.White}
                    }
                };
            }
        }

        public static Style NavigationPageStyle
        {
            get
            {
                return new Style(typeof (NavigationPage))
                {
                    Setters =
                    {
                        new Setter {Property = NavigationPage.BarBackgroundColorProperty, Value = GreenColor},
                        new Setter {Property = NavigationPage.BarTextColorProperty, Value = Color.White},
                        new Setter {Property = VisualElement.BackgroundColorProperty, Value = GreenColor}
                    }
                };
            }
        }
        public static Style LabelGreenStyle
        {
            get
            {
                return new Style(typeof(Label))
                {
                    Setters =
                    {
                        new Setter {Property = Label.XAlignProperty, Value = TextAlignment.Start},
                        new Setter {Property = Label.TextColorProperty, Value = GreenLabelColor},
                        new Setter {Property = Label.FontSizeProperty, Value = FontSizeSmall}
                    }
                };
            }
        }
        #endregion
    }
}