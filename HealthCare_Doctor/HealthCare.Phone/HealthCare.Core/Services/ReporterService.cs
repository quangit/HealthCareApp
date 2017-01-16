#if MVVMCROSS
using Cirrious.MvvmCross.ViewModels;
#endif
using HealthCare.Core.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace HealthCare.Core.Services
{

    public enum ErrorType { Message, ErrorMessage, Location, Network, StartProgress, StopProgress, RequestRetry };

    public class ErrorEventArgs : EventArgs
    {

        public object Title { get; private set; }
        public object Message { get; private set; }
        public ErrorType ErrorType { get; private set; }

        public ErrorEventArgs(object title, object message, ErrorType errorType)
        {
            Title = title;
            Message = message;
            ErrorType = errorType;
        }

        public ErrorEventArgs(object message, ErrorType errorType)
        {
            Message = message;
            ErrorType = errorType;
        }
    }

    #if MVVMCROSS
    public class ReporterService
        : MvxNavigatingObject, IReporterService
    {
        // public bool IsEnabled { get; set; }

        public ReporterService()
        {
            // IsEnabled = true;
        }
        public void ReportError(string error, ErrorType errorKind)
        {
            if (ErrorReported == null)
                return;

            Dispatcher.RequestMainThreadAction(() =>
            {
                var handler = ErrorReported;
                if (handler != null)
                    handler(this, new ErrorEventArgs(error, errorKind));
            });
        }

        public void ReportMessage(string title, string message)
        {
            //if (!IsEnabled) return;

            if (ErrorReported == null)
                return;

            Dispatcher.RequestMainThreadAction(() =>
            {
                var handler = ErrorReported;
                if (handler != null)
                    handler(this, new ErrorEventArgs(title, message, ErrorType.Message));
            });
        }

        public Task<bool> RequestRetry()
        {
            var tcs = new TaskCompletionSource<bool>();
            Dispatcher.RequestMainThreadAction(() =>
            {
                var handler = ErrorReported;
                if (handler != null)
                    handler(this, new ErrorEventArgs(tcs, ErrorType.RequestRetry));
            });
            return tcs.Task;
        }


        public event EventHandler<ErrorEventArgs> ErrorReported;


        public void ShowProgress()
        {
            ReportError("", ErrorType.StartProgress);
        }

        public void StopProgress()
        {
            ReportError("", ErrorType.StopProgress);
        }
    }
#endif
}

