#if MVVMCROSS
using Cirrious.MvvmCross.ViewModels;
using MyMvxViewModel = HealthCare.Core.ViewModels.MyMvxViewModel;
#else
using MyMvxViewModel = Template10.Mvvm.ViewModelBase;
using Template10.Mvvm;
#endif
using HealthCare.Core.Models;
using HealthCare.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HealthCare.Core.ViewModels
{
    public class WeekTopicFileViewModel : MyMvxViewModel
    {

        private List<TopicFiles> _TopicFiles;

        public List<TopicFiles> TopicFiles
        {
            get { return _TopicFiles; }
            set
            {
                SetProperty(ref _TopicFiles, value);
            }
        }


        private TopicFiles _TopicFile;
        public TopicFiles TopicFile
        {
            get { return _TopicFile; }
            set
            {
                SetProperty(ref _TopicFile, value);
            }
        }

        

        private Topic _Topic;
        public Topic Topic
        {
            get { return _Topic; }
            set
            {
                SetProperty(ref _Topic, value);
            }
        }


        private Models.TopicFiles _SelectedTopic;
        public Models.TopicFiles SelectedTopic
        {
            get { return _SelectedTopic; }
            set
            {
                SetProperty(ref _SelectedTopic, value);
            }
        }


		private MvxCommand<TopicFiles> _showFileTopicCommand;


		public MvxCommand<TopicFiles> ShowFileTopicCommand
        {
            get
            {
				_showFileTopicCommand = _showFileTopicCommand ?? new MvxCommand<TopicFiles>(t => {
					SelectedTopic = t;
					if(TopicSelected != null)
						TopicSelected();
                });
                return _showFileTopicCommand;
            }
        }


        public Action TopicSelected;
        public async override void Init()
        {
            base.Init();
            Topic = GetParam<Topic>();
            TopicFiles = Topic.TopicFiles;
            

            
            //TopicFiles = new ObservableCollection<Models.TopicFiles>();
            //Models.TopicFiles a = new Models.TopicFiles();
            //a.FileName = "anc";
            //a.FileUri = "http://www.google.com.vn";
            //TopicFiles.Add(a);
        }
    }
}
