using System;
using System.Collections.ObjectModel;
#if MVVMCROSS
using Cirrious.MvvmCross.ViewModels;
using MyMvxViewModel = HealthCare.Core.ViewModels.MyMvxViewModel;
#else
using MyMvxViewModel = Template10.Mvvm.ViewModelBase;
using Template10.Mvvm;
using HealthCare.Core;
#endif
using HealthCare.Core.Models;
using HealthCare.Core.Resources;
using HealthCare.Core.Services;
using HealthCare.Core.Services.Interfaces;
using System.Collections.Generic;

namespace HealthCare.Core.ViewModels
{
    public class ConsultViewModel : MyMvxViewModel
    {

        private string _inviteEmail;
        private string _inviteMessage;
        private readonly IMessageService _messageService;
        private MvxCommand _replyCommand;
        private ConsultRequest _request;

        public ConsultViewModel(IMessageService messageService)
        {
            _messageService = messageService;
        }
#if !MVVMCROSS
        public ConsultViewModel() : this(IoC.Resolve<IMessageService>())
        {

        }
#endif
        private MvxCommand _inviteCommand;

        public MvxCommand InviteCommand
        {
            get { return _inviteCommand ?? (_inviteCommand = new MvxCommand(Invite)); }
        }

        public event EventHandler MesssageSent;
        private async void Invite()
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(InviteMessage))
            {
                if (!Utils.RegexUtilities.IsEmail(Email))
                {
                    await _messageService.ShowMessageAsync(AppResources.FailureInvalidEmail, AppResources.Warning);
                    return;
                }
                var result = await HealthCareService.Current.ForwardSupportRequest(Request.Id, InviteMessage, Email);
                if (result)
                {
                    await _messageService.ShowMessageAsync(AppResources.ConsultView_InviteSuccess, AppResources.Consult_Title);
                    Email = string.Empty;
                    InviteMessage = string.Empty;
                    if (MesssageSent != null)
                        MesssageSent(this, EventArgs.Empty);
                }
            }
            else
            {
                await _messageService.ShowMessageAsync(AppResources.SignUp_Invalid, AppResources.Warning);
            }
        }

        public ConsultRequest Request
        {
            get { return _request; }
            set { SetProperty(ref _request, value); }
        }


        private ObservableCollection<ConsultResponse> _responses;
        public ObservableCollection<ConsultResponse> Responses
        {
            get { return _responses; }
            set { SetProperty(ref _responses, value); }
        }

        public string InviteMessage
        {
            get { return _inviteMessage; }
            set { SetProperty(ref _inviteMessage, value); }
        }

        public string Email
        {
            get { return _inviteEmail; }
            set { SetProperty(ref _inviteEmail, value); }
        }


        private MvxCommand _imageZoomCommand;

        public MvxCommand ImageZoomCommand
        {
            get { return _imageZoomCommand ?? (_imageZoomCommand = new MvxCommand(() => ShowViewModel<ImageZoomViewModel>(Request))); }
        }

        public Action<string> InviteDialogAction;

        private MvxCommand _showinviteCommand;

        public MvxCommand ShowInviteCommand
        {
            get { return _showinviteCommand ?? (_showinviteCommand = new MvxCommand(ShowInvite)); }
        }

        public Action<string> ReplyDialogAction;
        private MvxCommand _showreplyCommand;

        public MvxCommand ShowReplyCommand
        {
            get { return _showreplyCommand ?? (_showreplyCommand = new MvxCommand(ShowReply)); }
        }

        private void ShowReply()
        {
            if (ReplyDialogAction != null)
                ReplyDialogAction("");
        }



        private void ShowInvite()
        {
            if (InviteDialogAction != null)
                InviteDialogAction("");
        }


        public MvxCommand ReplyCommand
        {
            get { return _replyCommand ?? (_replyCommand = new MvxCommand(Reply)); }
        }

        private async void Reply()
        {
            if (string.IsNullOrEmpty(Request.ReplyContent))
            {
                await
                    _messageService.ShowMessageAsync(AppResources.SignUp_Invalid,
                        AppResources.Consult_Title);

                return;

            }
            if (await HealthCareService.Current.ReplySupportRequest(Request.Id, Request.ReplyContent))
            {
                Refresh();
                await
                    _messageService.ShowMessageAsync(AppResources.ConsultView_SentSuccessfuly,
                        AppResources.Consult_Title);
                Close(this);
            }
        }

        async void Refresh()
        {
            var requestData = await HealthCareService.Current.GetRequests();
            if (requestData != null)
            {
                Data.User.ConsultRequests.Clear();
                foreach (var consultRequest in requestData.Data)
                {
                    Data.User.ConsultRequests.Add(consultRequest);
                }
            }
        }


        public override async void Init()
        {
            base.Init();
            var temp = GetParam<ConsultRequest>();

			Request = temp;
            var resp = await HealthCareService.Current.GetRequestDetail(temp.Id);
            if (resp != null)
            {
                Responses = new ObservableCollection<ConsultResponse>(resp.Responses);
                for (int i = 0; i < Responses.Count; i++)
                {
					Request = resp.Question;
                    var index = (i + 1);
                    Responses[i].IndexString = "#" + index + AppResources.Consult_Of + Request.ReplyCount + ((Request.ReplyCount > 1) ? AppResources.Consult_Answers : AppResources.Consult_Answer);
                }
            }
            //				if (!Request.CanReply) {
            //					var myResp = new ConsultResponse (){ comment = Request.ReplyContent, doctor = Data.User.Profile };
            //					Responses.Insert (0, myResp);
            //				}
            //			}
        }
    }
}