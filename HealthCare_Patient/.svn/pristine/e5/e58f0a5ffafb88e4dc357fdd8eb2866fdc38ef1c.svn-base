using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Controls;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Models.ChBaseModel
{

    public class CHBaseDetailUIModel : NotifyPropertyChanged
    {
        private string _title;
        private string _value;
        private CHBaseDetailTypeEnum _type;

        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(); }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; RaisePropertyChanged(); }
        }

        public CHBaseDetailTypeEnum Type
        {
            get { return _type; }
            set { _type = value; RaisePropertyChanged(); }
        }
    }

    public enum CHBaseDetailTypeEnum
    {
        OneLine,
        OneLineOneText,
        TwoLine
    }

    public class CHBaseDetailTemplateSelector : IDataTemplateSelector
    {
        public DataTemplate OneLineDataTemplate { get; set; }
        public DataTemplate OneLineOneTextDataTemplate { get; set; }
        public DataTemplate TwoLineDataTemplate { get; set; }

        public DataTemplate SelectTemplate(object view, object dataItem)
        {
            var val = dataItem as CHBaseDetailUIModel;
            if (val?.Type == CHBaseDetailTypeEnum.OneLine)
            {
                return OneLineDataTemplate;
            }
            else if (val?.Type == CHBaseDetailTypeEnum.OneLineOneText)
            {
                return OneLineOneTextDataTemplate;
            }
            return TwoLineDataTemplate;

        }
    }
}
