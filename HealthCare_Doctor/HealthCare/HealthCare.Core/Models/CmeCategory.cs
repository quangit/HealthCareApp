using System;
using System.Collections.Generic;

namespace HealthCare.Core.Models
{
	public class CmeCategory : MyNotifyPropertyChanged
	{

		private string _name;
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}

		private List<CmeClass> _cmeClasses;
		public List<CmeClass> CmeClasses
		{
			get { return _cmeClasses; }
			set { SetProperty(ref _cmeClasses, value); }
		}
	}
}

