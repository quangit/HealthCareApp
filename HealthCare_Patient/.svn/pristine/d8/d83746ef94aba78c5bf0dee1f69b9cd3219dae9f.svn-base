using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HealthCare.Helpers
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void RaisePropertyChanged(string info)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(info));
        //    }
        //}

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler changedEventHandler = this.PropertyChanged;
            changedEventHandler?.Invoke((object)this, new PropertyChangedEventArgs(propertyName));
        }
    }
}