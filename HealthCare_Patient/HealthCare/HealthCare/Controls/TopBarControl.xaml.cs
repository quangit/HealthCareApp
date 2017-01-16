using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public partial class TopBarControl : ContentView
    {
        public TopBarControl()
        {
            InitializeComponent();
        }

        public bool IsTopLineVisible
        {
            get { return stlTopLine.IsVisible; }
            set { stlTopLine.IsVisible = value; }
        }

        public bool IsTitleVisible
        {
            get { return lbTitle.IsVisible; }
            set { lbTitle.IsVisible = value; }
        }

        public bool IsBottomLineVisible
        {
            get { return stlBottomLine.IsVisible; }
            set { stlBottomLine.IsVisible = value; }
        }

        public string Title
        {
            get { return lbTitle.Text; }
            set { lbTitle.Text = value; }
        }

        public TopBarStyle TopBarStyle
        {
            set
            {
                switch (value)
                {
                    case TopBarStyle.Style1:
                        TopBarControlHeight = HcStyles.TopBarStyle1Height;
                        IsTopLineVisible = true;
                        IsTitleVisible = true;
                        IsBottomLineVisible = true;
                        break;
                    case TopBarStyle.Style2:
                        TopBarControlHeight = HcStyles.TopBarStyle2Height;
                        IsTopLineVisible = false;
                        IsTitleVisible = false;
                        IsBottomLineVisible = true;
                        break;
                    case TopBarStyle.Style3:
                        TopBarControlHeight = HcStyles.TopBarStyle3Height;
                        IsTopLineVisible = true;
                        if (Common.OS == TargetPlatform.WinPhone)
                        {
                            HeightRequest = 120;
                            Title = " ";
                        }
                        else IsTitleVisible = false;
                        IsBottomLineVisible = false;
                        break;
                    case TopBarStyle.Style4:
                        TopBarControlHeight = HcStyles.TopBarStyle4Height;
                        IsTopLineVisible = false;
                        if (Common.OS == TargetPlatform.WinPhone)
                        {
                            HeightRequest = 120;
                            Title = " ";
                        }
                        else IsTitleVisible = false;
                        IsBottomLineVisible = false;
                        break;
                    case TopBarStyle.Style5:
                        HeightRequest = 0;
                        TopBarControlHeight = 0;
                        break;
                }
            }
        }

        public double TopBarControlHeight { get; set; }
    }

    public enum TopBarStyle
    {
        /// <summary>
        ///     Type1: line trên, line dưới, title
        /// </summary>
        Style1,

        /// <summary>
        ///     Type2: line dưới,
        /// </summary>
        Style2,

        /// <summary>
        ///     Type3: line trên
        /// </summary>
        Style3,

        /// <summary>
        ///     Type4: hide all
        /// </summary>
        Style4,

        /// <summary>
        ///     Type4: Invisible all
        /// </summary>
        Style5
    }
}