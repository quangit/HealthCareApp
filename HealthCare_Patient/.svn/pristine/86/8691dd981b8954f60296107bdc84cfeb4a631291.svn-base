using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GalaSoft.MvvmLight.Command;
using HealthCare.Enums;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.Pages;
using HealthCare.Resx;
using HealthCare.Services;
using HealthCare.Services.Interfaces;
using HealthCare.WebServices.Interfaces;
using Xamarin.Forms;

namespace HealthCare.ViewModels
{
    public class MoreSupportViewModel2 : BaseViewModel<MoreSupportViewModel2>
    {
        private readonly IMoreSupportWS _moreSupportWs;
        private ObservableCollection<SupportQuestionModel> _listQuestion;
        private ObservableCollection<SupportResponsesModel> _listAnswer;
        private SupportAnswerModel _answerInfo;

        private bool _isCanLoadMore, _isCanLoadMore2, _isShowPopup;
        private int _itemCount, _itemCount2;
        private int _startIndex, _startIndex2;
        private int _page, _page2;
        private ICommand _loadMoreTappedCommand, _askCommand, _readMoreCommand;
        private string _keyword, _answerText;
        private ICommand _getQuestionListCommand, _addQuestionCommand, _getAnswerListLoadMoreCommand, _zoomImageCommand;
        private ICommand _questionItemClickedCommand;
        private SupportQuestionModel _supportQuestionSelectedItem;
        private bool _isFocusSearch;
        private Color _searchBorderColor = HcStyles.BorderLightGrayColor;
        private bool _isShowClearSearch;
        private RelayCommand _clearSearchCommand;
        private RelayCommand _textChangeCommand, _goBackCommand;
        private bool _isRefreshing;
        private ICommand _itemTappedCommand;
        private RelayCommand _refreshCommand;

        #region Properties

        public static readonly double FontSizeTitle = HcStyles.FontSizeTitle - 3;

        public static readonly double FontSizeSubTitle = HcStyles.FontSizeSubTitle - 3;

        public static readonly double FontSizeContent = HcStyles.FontSizeContent - 3;

        public static readonly double FontSizeSubContent = HcStyles.FontSizeSubContent - 3;

        public static readonly double FontLegalNoticeContent = HcStyles.FontSizeContent - Common.OnPlatform(2, 2, 7);

        public string Keyword
        {
            get { return _keyword; }
            set { _keyword = value; RaisePropertyChanged(); }
        }
        public string AnswerText
        {
            get { return _answerText; }
            set { _answerText = value; RaisePropertyChanged(); }
        }
        public bool IsFocusSearch
        {
            get { return _isFocusSearch; }
            set
            {
                _isFocusSearch = value;
                if (Common.OS != TargetPlatform.Android)
                    SearchBorderColor = value ? HcStyles.GreenColor : HcStyles.BorderBlueGrayColor;
                RaisePropertyChanged("IsFocusSearch");
            }
        }

        public bool IsShowClearSearch
        {
            get { return _isShowClearSearch; }
            set
            {
                _isShowClearSearch = value;
                RaisePropertyChanged("IsShowClearSearch");
            }
        }

        public Color SearchBorderColor
        {
            get { return _searchBorderColor; }
            set { _searchBorderColor = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<SupportQuestionModel> ListQuestion
        {
            get { return _listQuestion; }
            set { _listQuestion = value; RaisePropertyChanged(); }
        }

        public SupportQuestionModel SupportQuestionSelectedItem
        {
            get { return _supportQuestionSelectedItem; }
            set { _supportQuestionSelectedItem = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<SupportResponsesModel> ListAnswer
        {
            get { return _listAnswer; }
            set { _listAnswer = value; RaisePropertyChanged(); }
        }
        public SupportAnswerModel AnswerInfo
        {
            get { return _answerInfo; }
            set { _answerInfo = value; RaisePropertyChanged(); }
        }
        public bool IsCanLoadMore
        {
            get { return _isCanLoadMore; }
            set
            {
                _isCanLoadMore = value;
                RaisePropertyChanged();
            }
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged();
            }
        }

        public int ItemCount
        {
            get { return _itemCount; }
            set
            {
                _itemCount = value;
                RaisePropertyChanged();
            }
        }
        public int ItemCount2
        {
            get { return _itemCount2; }
            set
            {
                _itemCount2 = value;
                RaisePropertyChanged();
            }
        }
        public bool IsCanLoadMore2
        {
            get { return _isCanLoadMore2; }
            set
            {
                _isCanLoadMore2 = value;
                RaisePropertyChanged();
            }
        }
        public bool IsShowPopup
        {
            get { return _isShowPopup; }
            set
            {
                _isShowPopup = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand ClearSearchCommand => _clearSearchCommand ??
                                              (_clearSearchCommand = new RelayCommand(ClearKeyword));
        public RelayCommand TextChangeCommand => _textChangeCommand ??
                                               (_textChangeCommand = new RelayCommand(HandleChangedText));
        public RelayCommand GoBackCommand => _goBackCommand ??
                                            (_goBackCommand = new RelayCommand(() =>
                                            {
                                                NavigationService.GoBack();
                                            }));

        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new RelayCommand(async () =>
                {
                    IsRefreshing = true;
                    await GetQuestionList(false, true);
                    IsRefreshing = false;
                }));
            }
        }

        public ICommand GetQuestionListLoadMoreCommand
        {
            get
            {
                return _loadMoreTappedCommand ??
                       (_loadMoreTappedCommand = new RelayCommand(async () => { await GetQuestionListLoadMore(true, Keyword); }));
            }
        }


        public ICommand GetQuestionListCommand
        {
            get
            {
                return _getQuestionListCommand ?? (_getQuestionListCommand = new RelayCommand(async () =>
                {
                    await GetQuestionList(false, true);
                }));
            }
        }

        //public ICommand AskCommand => _askCommand ??
        //                                  (_askCommand = new RelayCommand(Ask));

        public ICommand ReadMoreCommand => _readMoreCommand ??
                                        (_readMoreCommand = new RelayCommand<string>(ReadMore));



        public ICommand QuestionItemClickedCommand
        {
            get
            {
                return _questionItemClickedCommand ??
                       (_questionItemClickedCommand = new RelayCommand<SupportQuestionModel>(async item =>
                      {
                          if (item != null)
                          {
                              SupportQuestionSelectedItem = item;
                              await GetAnswerList(item.Id);
                              //Goto detail page
                              NavigationService.NavigateTo(typeof(AskDoctorDetailPage));

                          }
                      }));
            }
        }
        private void ReadMore(string s)
        {
            AnswerText = s;
            NavigationService.NavigateTo(typeof(ReadMorePage));
        }
        public void Ask()
        {
            NavigationService.NavigateTo(typeof(AddQuestionPage));
        }

        public ICommand AddQuestionCommand
        {
            get
            {
                return _addQuestionCommand ??
                       (_addQuestionCommand = new RelayCommand(async () =>
                      {
                          if (await AskDoctorViewModel.Instance.CheckSkypeExist())
                              NavigationService.NavigateTo(typeof(AddQuestionPage));

                      }));
            }
        }

        public ICommand GetAnswerListLoadMoreCommand
        {
            get
            {
                return _getAnswerListLoadMoreCommand ??
                       (_getAnswerListLoadMoreCommand = new RelayCommand(async () => { await GetAnswerListLoadMore(true, Keyword); }));
            }
        }
        public ICommand ZoomImageCommand
        {
            get
            {
                return _zoomImageCommand ??
                       (_zoomImageCommand = new RelayCommand(ZoomImage));
            }
        }


        private ICommand _changeQuestionStatusCommand;
        public ICommand ChangeQuestionStatusCommand
        {
            get
            {
                return _changeQuestionStatusCommand ?? (_changeQuestionStatusCommand = new RelayCommand(async () =>
               {
                   await ChangeQuestionStatus();
               }));
            }
        }

        #endregion


        #region Methods        

        private async Task ChangeQuestionStatus()
        {
            try
            {
                Common.ShowLoading();
                await _moreSupportWs.ChangeStatusQuestion(SupportQuestionSelectedItem.Id, !SupportQuestionSelectedItem.Shared);
                SupportQuestionSelectedItem.Shared = !SupportQuestionSelectedItem.Shared;
                await Common.AlertAsync(AppResources.change_success);
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
        }

        private void ZoomImage()
        {
            NavigationService.NavigateTo(typeof(ImagePopupPage));
        }
        private void HandleChangedText()
        {
            IsShowClearSearch = !string.IsNullOrEmpty(Keyword);
        }

        public void ClearKeyword()
        {
            Keyword = "";
        }


        public async Task GetQuestionList(bool isResetKeyword, bool isShowLoadingDialog)
        {
            ResetLoadMore();
            Keyword = isResetKeyword ? "" : Keyword?.Trim();
            await GetQuestionListLoadMore(isShowLoadingDialog, Keyword);
        }

        private async Task GetQuestionListLoadMore(bool isShowLoading, string keySearch)
        {
            try
            {
                if (!IsCanLoadMore) return;

                if (isShowLoading)
                    Common.ShowLoading();

                _startIndex += _page > 0 ? AppConstant.ITEMS_PER_PAGE : 0;
                IsCanLoadMore = false;
                var l = await _moreSupportWs.GetQuestionList(keySearch, _startIndex, AppConstant.ITEMS_PER_PAGE);
                IsCanLoadMore = true;

                await Task.Delay(100);//make sure the IsCanLoadMore = true is notify to the HcListView CanLoadMoreProperty

                if (l.Count > 0)
                {
                    foreach (var questionModel in l)
                    {
                        ListQuestion.Add(questionModel);
                    }

                    _page++;
                    IsCanLoadMore = true;
                }

                if (l.Count == 0 || l.Count < AppConstant.ITEMS_PER_PAGE)
                {
                    IsCanLoadMore = false;
                }
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
                IsCanLoadMore = true;
            }
            finally
            {
                ItemCount = ListQuestion.Count;
                UserDialogs.Instance.HideLoading();
            }
        }

        private void ResetLoadMore()
        {
            _page = 0;
            _startIndex = 0;
            IsCanLoadMore = true;
            ItemCount = -1;
            ListQuestion = new ObservableCollection<SupportQuestionModel>();
        }


        private void ResetLoadMore2()
        {
            _page2 = 0;
            IsCanLoadMore2 = true;
            _startIndex2 = 0;
            ItemCount2 = -1;
            ListAnswer = new ObservableCollection<SupportResponsesModel>();
            GC.Collect();
        }
        #endregion

        public MoreSupportViewModel2(INavigationService navigationService, IMoreSupportWS moreSupportWs) : base(navigationService)
        {
            _moreSupportWs = moreSupportWs;
            IsCanLoadMore = true;
            IsCanLoadMore2 = true;
            ListQuestion = new ObservableCollection<SupportQuestionModel>();
            SearchBorderColor = HcStyles.BorderLightGrayColor;
            ListTranforms = new List<ITransformation>
            {
                new CornersTransformation(10, CornerTransformType.AllRounded)
            };
            ListTranformsCrop = new List<ITransformation>()
            {
                new CropTransformation(1, 0, 0, AppConstant.ScreenWidth, AppConstant.Height9X16)
            };
        }

        public List<ITransformation> ListTranforms { get; set; }
        public List<ITransformation> ListTranformsCrop { get; set; }

        public async Task GetAnswerList(string id)
        {
            ResetLoadMore2();
            await GetAnswerListLoadMore(true, id);
        }

        private async Task GetAnswerListLoadMore(bool isShowLoading, string keySearch)
        {
            try
            {
                if (!IsCanLoadMore2) return;

                if (isShowLoading)
                    Common.ShowLoading();

                _startIndex2 += _page2 > 0 ? AppConstant.ITEMS_PER_PAGE : 0;
                IsCanLoadMore2 = false;
                var l = await _moreSupportWs.GetAnswerList(keySearch, _startIndex2, AppConstant.ITEMS_PER_PAGE);
                AnswerInfo = new SupportAnswerModel()
                {
                    Responses = new ObservableCollection<SupportResponsesModel>(l),
                    Question = SupportQuestionSelectedItem
                };

                if (!ListAnswer.Any(x => x.CreatedByUserId.Equals("patient")))
                    ListAnswer.Add(new SupportResponsesModel()
                    {
                        Comment = SupportQuestionSelectedItem.Symptom,
                        CreatedByUserId = "patient",
                        Doctor = new DoctorModel()
                        {
                            //FirstName = SupportQuestionSelectedItem.FullName,
                            FullName = SupportQuestionSelectedItem.FullName,
                            Photo = SupportQuestionSelectedItem.PatientPhoto,
                            Address = SupportQuestionSelectedItem.BasicInfo
                        },
                        WhenCreated = SupportQuestionSelectedItem.WhenCreated,

                    });
                IsCanLoadMore2 = true;

                await Task.Delay(100);//make sure the IsCanLoadMore = true is notify to the HcListView CanLoadMoreProperty

                if (AnswerInfo.Responses.Count > 0)
                {
                    foreach (var answerModel in AnswerInfo.Responses)
                    {
                        ListAnswer.Add(answerModel);
                    }
                    //RaisePropertyChanged("ListQuestion");
                    _page2++;
                    IsCanLoadMore2 = true;
                }

                if (AnswerInfo.Responses.Count == 0 || AnswerInfo.Responses.Count < AppConstant.ITEMS_PER_PAGE)
                {
                    IsCanLoadMore2 = false;
                }
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
            finally
            {
                ItemCount2 = ListAnswer.Count;
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}
