using System;
using HealthCare.Core.Models;
using HealthCare.Core.Resources;

namespace HealthCare.Core.Models
{
	public class ConsultResponse : MyNotifyPropertyChanged
	{
		public string id { get; set; }
		public string supportRequestId { get; set; }
		public string comment { get; set; }
		public int status { get; set; }
		public long whenCreated { get; set; }
		public Doctor doctor { get; set; }
		public string createdByUserId { get; set; }
		public string IndexString{ get; set;}
	}
}

