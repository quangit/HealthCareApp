using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.Resx;
using Xamarin.Forms;

namespace HealthCare.Controls.ViewCells
{
    public partial class CHBaseViewCell : ViewCell
    {
        public CHBaseViewCell()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty Title1BindableProperty = BindableProperty.Create("Title1BindableProperty", typeof(string), typeof(CHBaseViewCell), default(string),
            propertyChanged:
            (bindable, value, newValue) =>
            {
                ((CHBaseViewCell)bindable).lblTitle1.Text = (string)newValue;
                ((CHBaseViewCell)bindable).lblTitle1.IsVisible = !string.IsNullOrWhiteSpace((string)newValue);
            });

        public string Title1
        {
            get { return (string)GetValue(Title1BindableProperty); }
            set { SetValue(Title1BindableProperty, value); }
        }

        public static readonly BindableProperty Title2Property = BindableProperty.Create("Title2Property", typeof(string), typeof(CHBaseViewCell), default(string),
            propertyChanged:
            (bindable, value, newValue) =>
            {
                ((CHBaseViewCell)bindable).lblTitle2.Text = (string)newValue;
                ((CHBaseViewCell)bindable).lblTitle2.IsVisible = !string.IsNullOrWhiteSpace((string)newValue);
            });

        public string Title2
        {
            get { return (string)GetValue(Title2Property); }
            set { SetValue(Title2Property, value); }
        }

        public static readonly BindableProperty Content1Property = BindableProperty.Create("Content1", typeof(string), typeof(CHBaseViewCell), default(string),
            propertyChanged:
            (bindable, value, newValue) =>
            {
                var rawVal = (string)newValue;
                string val = null;

                if (!string.IsNullOrWhiteSpace(rawVal))
                {
                    val = StringToFake(rawVal);
                }

                 ((CHBaseViewCell)bindable).lblContent1.Text = val;
                ((CHBaseViewCell)bindable).lblContent1.IsVisible = !string.IsNullOrWhiteSpace(val);
            });

        public string Content1
        {
            get { return (string)GetValue(Content1Property); }
            set { SetValue(Content1Property, value); }
        }

        public static readonly BindableProperty Content2Property = BindableProperty.Create("Content2", typeof(string), typeof(CHBaseViewCell), default(string),
            propertyChanged:
            (bindable, value, newValue) =>
            {
                var rawVal = (string)newValue;
                string val = null;

                if (!string.IsNullOrWhiteSpace(rawVal))
                {
                    val = StringToFake(rawVal);
                }

                 ((CHBaseViewCell)bindable).lblContent2.Text = val;
                ((CHBaseViewCell)bindable).lblContent2.IsVisible = !string.IsNullOrWhiteSpace(val);
            });

        public string Content2
        {
            get { return (string)GetValue(Content2Property); }
            set { SetValue(Content2Property, value); }
        }

        private static string StringToFake(string item)
        {
            if (!string.IsNullOrWhiteSpace(item))
            {
                var limit = MaxCharacters;
                if (item.Length >= limit)
                    return item.Substring(0, limit - 3) + "...";
                if (item.IndexOf("\n", StringComparison.Ordinal) > -1)
                {
                    return item.Substring(0, item.IndexOf("\n", StringComparison.Ordinal)) + "...";
                }
                if (item.IndexOf("\r", StringComparison.Ordinal) > -1)
                {
                    return item.Substring(0, item.IndexOf("\r", StringComparison.Ordinal)) + "...";
                }
                return item;
            }
            return item;
        }

        private static readonly int MaxCharacters = Common.OnPlatform<int>(18, 20, 35);
    }
}
