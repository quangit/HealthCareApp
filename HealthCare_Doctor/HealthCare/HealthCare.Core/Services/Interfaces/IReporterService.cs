using System;
using System.Threading.Tasks;

namespace HealthCare.Core.Services.Interfaces
{
	public interface IReporterService
	{
	    //bool IsEnabled { get; set; }
		void ReportError(string error, ErrorType errorType);
		void ReportMessage(string title, string message);

		void ShowProgress();
		void StopProgress();

		Task<bool> RequestRetry();

        event EventHandler<ErrorEventArgs> ErrorReported;
    }
}

