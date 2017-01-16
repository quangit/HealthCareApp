﻿using System;
using HealthCare.Controls;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XLabs.Forms.Controls;
using HealthCare.Helpers;

namespace HealthCare.iOS.Renderers
{

    using UIKit;

    using Xamarin.Forms;
    using Xamarin.Forms.Platform.iOS;

    /// <summary>
    /// Class StyleDescriptor.
    /// </summary>
    public class StyleDescriptor
    {
        /// <summary>
        /// The background color
        /// </summary>
        public UIColor EnabledForegroundColor = UIColor.FromRGBA(0.275f, 0.341f, 0.412f, 1f);
        /// <summary>
        /// The background color
        /// </summary>
        public UIColor EnabledBackgroundColor = Color.FromHex("#2CBE71").ToUIColor(); 
        /// <summary>
        /// The background color
        /// </summary>
        public UIColor BackgroundColor = Color.FromHex("#ffffffff").ToUIColor();
        /// <summary>
        /// The date foreground color
        /// </summary>
        public UIColor DateForegroundColor = UIColor.FromRGBA(0.275f, 0.341f, 0.412f, 1f);
        /// <summary>
        /// The date background color
        /// </summary>
        public UIColor DateBackgroundColor = UIColor.White;
        /// <summary>
        /// The inactive date foreground color
        /// </summary>
        public UIColor InactiveDateForegroundColor = UIColor.LightGray;
        /// <summary>
        /// The inactive date background color
        /// </summary>
        public UIColor InactiveDateBackgroundColor = UIColor.White;
        /// <summary>
        /// The selected date foreground color
        /// </summary>
        public UIColor SelectedDateForegroundColor = Color.FromHex("#ffffffff").ToUIColor();
        /// <summary>
        /// The selected date background color
        /// </summary>
        public UIColor SelectedDateBackgroundColor = Color.FromHex("#ff379bff").ToUIColor();
        /// <summary>
        /// The title foreground color
        /// </summary>
        public UIColor TitleForegroundColor = UIColor.DarkGray;
        /// <summary>
        /// The title background color
        /// </summary>
        public UIColor TitleBackgroundColor = UIColor.LightGray;
        /// <summary>
        /// The today foreground color
        /// </summary>
        public UIColor TodayForegroundColor = UIColor.Red;//Color.FromHex("#ff778088").ToUIColor();
                                                          /// <summary>
                                                          /// The today background color
                                                          /// </summary>
        public UIColor TodayBackgroundColor = UIColor.DarkGray;//Color.FromHex("#ccffcc").ToUIColor();
                                                               /// <summary>
                                                               /// The day of week label foreground color
                                                               /// </summary>
        public UIColor DayOfWeekLabelForegroundColor = UIColor.White;
        /// <summary>
        /// The day of week label background color
        /// </summary>
        public UIColor DayOfWeekLabelBackgroundColor = UIColor.LightGray;
        /// <summary>
        /// The highlighted date foreground color
        /// </summary>
        public UIColor HighlightedDateForegroundColor = Color.FromHex("#ff778088").ToUIColor();
        /// <summary>
        /// The highlighted date background color
        /// </summary>
        public UIColor HighlightedDateBackgroundColor = Color.FromHex("#ccffcc").ToUIColor();
        /// <summary>
        /// The date separator color
        /// </summary>
        public UIColor DateSeparatorColor = Color.FromHex("#ffbababa").ToUIColor();
        /// <summary>
        /// The selection background style
        /// </summary>
        public HealthCare.Controls.CalendarView.BackgroundStyle SelectionBackgroundStyle = HealthCare.Controls.CalendarView.BackgroundStyle.Fill;
        /// <summary>
        /// The today background style
        /// </summary>
        public HealthCare.Controls.CalendarView.BackgroundStyle TodayBackgroundStyle = HealthCare.Controls.CalendarView.BackgroundStyle.Fill;
        /// <summary>
        /// The date label font
        /// </summary>
        public UIFont DateLabelFont = UIFont.BoldSystemFontOfSize(10);
        /// <summary>
        /// The month title font
        /// </summary>
        public UIFont MonthTitleFont = UIFont.BoldSystemFontOfSize(16);
        /// <summary>
        /// The should highlight days of week label
        /// </summary>
        public bool ShouldHighlightDaysOfWeekLabel = false;

    }
}