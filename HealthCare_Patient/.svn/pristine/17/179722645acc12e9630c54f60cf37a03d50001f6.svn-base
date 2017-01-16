using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HealthCare.WinPhone.Renderer
{
    public partial class WPRatingControl : UserControl
    {
        public WPRatingControl()
        {
            InitializeComponent();
            control.ValueChanged += (s, e) =>
            {
                Value = control.Value;
                ValueChanged?.Invoke(s, control.Value);
            };
        }
        public event EventHandler<double> ValueChanged;
        public double Value { get; set; }
        public void SetRatingValue(double val)
        {
            if (control != null) control.Value = val;
        }

        public bool IsRatingEnabled
        {
            get { return control != null ? control.IsEnabled : false; }
            set { if (control != null) control.IsEnabled = value; }
        }
    }
}
