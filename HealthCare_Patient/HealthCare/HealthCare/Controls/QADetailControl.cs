using System;
using System.Collections.Generic;
using System.Windows.Input;
using FFImageLoading.Forms;
using HealthCare.Helpers;
using HealthCare.ViewModels;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class QADetailControl : ContentView
    {
        public static BindableProperty ChangeStatusCommandProperty =
            BindableProperty.Create<QADetailControl, ICommand>(ctrl => ctrl.ChangeStatusCommand, null,
                BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (QADetailControl)bindable;
                    if (ctrl._btnChangeStatus != null) ctrl._btnChangeStatus.Command = newValue;
                });

        public static BindableProperty IsShowchangeStatusQuestionProperty =
            BindableProperty.Create<QADetailControl, bool>(ctrl => ctrl.IsShowchangeStatusQuestion, false,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (QADetailControl)bindable;
                    ctrl.IsShowchangeStatusQuestion = newValue;
                });

        public static BindableProperty StatusStringProperty =
            BindableProperty.Create<QADetailControl, string>(ctrl => ctrl.StatusString, null, BindingMode.TwoWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (QADetailControl)bindable;
                    //if (ctrl._btnChangeStatus != null) ctrl._btnChangeStatus.Text = newValue;
                });

        public static BindableProperty BasicInfoProperty =
            BindableProperty.Create<QADetailControl, string>(ctrl => ctrl.BasicInfo, string.Empty, BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (QADetailControl)bindable;
                    ctrl.BasicInfo = newValue;
                });

        public static BindableProperty CreatedByUserIdProperty =
            BindableProperty.Create<QADetailControl, string>(ctrl => ctrl.CreatedByUserId, string.Empty,
                BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (QADetailControl)bindable;
                    ctrl.CreatedByUserId = newValue;
                });

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create<QADetailControl, ICommand>(p => p.Command, null, BindingMode.TwoWay);

        public static readonly BindableProperty ReadMoreCommandProperty =
            BindableProperty.Create<QADetailControl, ICommand>(p => p.ReadMoreCommand, null, BindingMode.TwoWay);

        public static BindableProperty AvatarProperty =
            BindableProperty.Create<QADetailControl, string>(ctrl => ctrl.Avatar, string.Empty, BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (QADetailControl)bindable;
                    ctrl.Avatar = newValue;
                });

        public static BindableProperty NameProperty =
            BindableProperty.Create<QADetailControl, string>(ctrl => ctrl.Name, string.Empty, BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (QADetailControl)bindable;
                    ctrl.Name = newValue;
                });

        public static BindableProperty IsQuestionProperty =
            BindableProperty.Create<QADetailControl, bool>(ctrl => ctrl.IsQuestion, false, BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (QADetailControl)bindable;
                    ctrl.IsQuestion = newValue;
                });

        public static BindableProperty GenderProperty =
            BindableProperty.Create<QADetailControl, string>(ctrl => ctrl.Gender, string.Empty, BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (QADetailControl)bindable;
                    ctrl.Gender = newValue;
                });

        public static BindableProperty DateTimeProperty =
            BindableProperty.Create<QADetailControl, DateTime>(ctrl => ctrl.DateTime, DateTime.Now, BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (QADetailControl)bindable;
                    ctrl.DateTime = newValue;
                });

        public static BindableProperty TextProperty =
            BindableProperty.Create<QADetailControl, string>(ctrl => ctrl.Text, string.Empty, BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (QADetailControl)bindable;
                    ctrl.Text = newValue;
                });

        public static BindableProperty OldProperty =
            BindableProperty.Create<QADetailControl, int>(ctrl => ctrl.Old, 0, BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (QADetailControl)bindable;
                    ctrl.Old = newValue;
                });

        private ButtonRadiusControl _btnChangeStatus;

        private Grid _gridMain;
        private ContentView _ctvChangeStatus;

        public string Avatar
        {
            get { return (string)GetValue(AvatarProperty); }
            set { SetValue(AvatarProperty, value); }
        }

        public bool IsQuestion
        {
            get { return (bool)GetValue(IsQuestionProperty); }
            set { SetValue(IsQuestionProperty, value); }
        }

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public string Gender
        {
            get { return (string)GetValue(GenderProperty); }
            set { SetValue(GenderProperty, value); }
        }

        public string BasicInfo
        {
            get { return (string)GetValue(BasicInfoProperty); }
            set { SetValue(BasicInfoProperty, value); }
        }

        public DateTime DateTime
        {
            get { return (DateTime)GetValue(DateTimeProperty); }
            set { SetValue(DateTimeProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string CreatedByUserId
        {
            get { return (string)GetValue(CreatedByUserIdProperty); }
            set { SetValue(CreatedByUserIdProperty, value); }
        }

        public int Old
        {
            get { return (int)GetValue(OldProperty); }
            set { SetValue(OldProperty, value); }
        }

        public ICommand ReadMoreCommand
        {
            get { return (ICommand)GetValue(ReadMoreCommandProperty); }
            set { SetValue(ReadMoreCommandProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public ICommand ChangeStatusCommand
        {
            get { return (ICommand)GetValue(ChangeStatusCommandProperty); }
            set { SetValue(ChangeStatusCommandProperty, value); }
        }

        public bool IsShowchangeStatusQuestion
        {
            get { return (bool)GetValue(IsShowchangeStatusQuestionProperty); }
            set { SetValue(IsShowchangeStatusQuestionProperty, value); }
        }

        public string StatusString
        {
            get { return (string)GetValue(StatusStringProperty); }
            set { SetValue(StatusStringProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _gridMain = new Grid
            {
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(5)
            };
            _gridMain.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition
                {
                    Height = GridLength.Auto
                },
                new RowDefinition
                {
                    Height = GridLength.Auto
                }
            };
            var info = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition
                    {
                        Width = GridLength.Auto
                    },
                    new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    },
                    new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    }
                },
                RowDefinitions = new RowDefinitionCollection()
                {
                    new RowDefinition()
                    {
                        Height = GridLength.Auto
                    },
                    new RowDefinition()
                    {
                        Height = GridLength.Auto
                    }
                },
                VerticalOptions = LayoutOptions.Start
            };
            //ImageRounderCorner imgAvatar = new ImageRounderCorner()
            //{
            //    HeightRequest = Common.OnPlatform(40,50,60),
            //    WidthRequest = Common.OnPlatform(40, 50, 60),
            //};
            var imgAvatar = new CachedImage
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = Common.OnPlatform(40, 50, 60),
                WidthRequest = Common.OnPlatform(40, 50, 60),
                DownsampleToViewSize = true,
                RetryCount = 3,
                TransparencyEnabled = false,
                Transformations = MoreSupportViewModel2.Instance.ListTranforms
            };


            if (!string.IsNullOrEmpty(Avatar))
            {
                imgAvatar.Source = Avatar;
            }
            else
            {
                if (IsQuestion)
                {
                    imgAvatar.Source = (Gender == "Female"
                        ? AppConstant.AvatarPatientFeMale
                        : AppConstant.AvatarPatientMale);
                }
                else
                {
                    imgAvatar.Source = (Gender == "Female"
                        ? AppConstant.AvatarDoctorFeMale
                        : AppConstant.AvatarDoctorMale);
                }
            }
            Grid.SetColumn(imgAvatar, 0);
            Grid.SetRow(imgAvatar, 0);
            Grid.SetRowSpan(imgAvatar, 2);
            var stackInfo = new StackLayout
            {
                Spacing = 2,
                VerticalOptions = LayoutOptions.Center
            };
            var lblName = new Label
            {
                Text = Name?.Trim(),
                TextColor = HcStyles.BlackTextColor,
                FontSize = HcStyles.FontSizeContent,
                FontAttributes = FontAttributes.Bold
            };

            var lblOld = new Label
            {
                Text = BasicInfo,
                FontSize = HcStyles.FontSizeSubContent,
                TextColor = HcStyles.BlackTextColor
            };

            //lblOld.Text += ", " + Gender;

            var lblDatetime = new Label
            {
                Text = DateTime.ToString("MM/dd/yyyy hh:mm tt"),
                FontSize = HcStyles.FontSizeSubContent,
                TextColor = HcStyles.BlackTextColor
            };
            if (CreatedByUserId == "patient")
            {
                IsQuestion = true;
            }
            if (IsQuestion)
            {
                stackInfo.Children.Add(lblName);
                stackInfo.Children.Add(lblOld);
            }
            else
            {
                lblOld.Text = " ";
                //stackInfo.Children.Add(lblOld);
                stackInfo.Children.Add(lblName);
            }
            stackInfo.Children.Add(lblDatetime);
            Grid.SetColumn(stackInfo, 1);
            Grid.SetRow(stackInfo, 0);
            Grid.SetRowSpan(stackInfo, 2);

            var height = Common.OnPlatform(30, 30, 40);
            var width = Common.OnPlatform(30, 30, 40);

            var grBtn = new Grid
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                HeightRequest = height,
                WidthRequest = width,
                Padding = new Thickness(0, 0, 20, 0),
                GestureRecognizers =
                {
                    new TapGestureRecognizer
                    {
                        Command = AskDoctorViewModel.Instance.AskCommand
                    }
                }
            };
            grBtn.Children.Add(new Image
            {
                Source = ImageSource.FromFile("ic_button_add.png"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = height,
                WidthRequest = width,
                VerticalOptions = LayoutOptions.FillAndExpand
            });

            //ContentView contentLabel = new ContentView()
            //{
            //    VerticalOptions = LayoutOptions.Center,
            //    HorizontalOptions = LayoutOptions.Center,
            //    Padding = new Thickness(10,0,0,0),
            //    Content = new Label()
            //    {
            //        Text = AppResources.ask,
            //        TextColor = Color.White,
            //        FontAttributes = FontAttributes.Bold,
            //        FontSize = HcStyles.ButtonFontSize,
            //    }
            //};
            //grBtn.Children.Add(contentLabel);


            Grid.SetColumn(grBtn, 2);
            Grid.SetRow(grBtn, 0);

            info.Children.Add(imgAvatar);
            info.Children.Add(stackInfo);
            if (IsQuestion)
                info.Children.Add(grBtn);

            //           < ContentView
            //                 HorizontalOptions = "EndAndExpand"
            //                 IsVisible = "{Binding SupportQuestionSelectedItem.IsCreateByUser}"
            //                 Grid.Row = "1" >
            //      < controls:ButtonRadiusControl Text = "{Binding SupportQuestionSelectedItem.StatusRevertString}" HorizontalOptions = "Center"
            //                                    BgHeight = "{StaticResource ButtonHeight}"
            //                                    TextPadding = "0" BgWidth = "{StaticResource ButtonWidth}"
            //                                    Command = "{Binding ChangeQuestionStatusCommand}" />
            //    </ ContentView > 

            var _width = Device.Idiom == TargetIdiom.Tablet ? 140 : 110;
            var _height = Device.Idiom == TargetIdiom.Tablet ? 30 : 30;

            _btnChangeStatus = new ButtonRadiusControl
            {
                BgHeight = Common.OnPlatform<double>(_height, 30, 50),
                TextPadding = 0,
                BgWidth = Common.OnPlatform<double>(_width, 140, 180),
                Command = ChangeStatusCommand,
                BindingContext = this
            };
            if (Common.OS == TargetPlatform.WinPhone)
                _btnChangeStatus.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof (Button)) - 4;

            _btnChangeStatus.SetBinding(ButtonRadiusControl.TextProperty, new Binding("StatusString"));

            _ctvChangeStatus = new ContentView
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Content = _btnChangeStatus,
                BindingContext = this,
                VerticalOptions = LayoutOptions.End,
                Padding = new Thickness(3)
            };
            _ctvChangeStatus.SetBinding(ContentView.IsVisibleProperty, new Binding("IsShowchangeStatusQuestion"));
            Grid.SetColumn(_ctvChangeStatus, 2);
            Grid.SetRow(_ctvChangeStatus, 1);

            if (IsQuestion)
                info.Children.Add(_ctvChangeStatus);

            Grid.SetRow(stackInfo, 0);

            var content = new ContentView
            {
                BackgroundColor = HcStyles.QAColor,
                Padding = new Thickness(10)
            };
            var lblContent = new Label
            {
                Text = RemoveSpace(Text?.Trim()),
                TextColor = HcStyles.BlackTextColor,
                FontSize = HcStyles.FontSizeContent
            };
            if (Common.OS == TargetPlatform.WinPhone)
            {
                if (lblContent.Text.Length > 1500)
                {
                    var temp = SplitText(lblContent.Text);
                    content.Content = (new Label
                    {
                        Text = temp[0] + "...",
                        TextColor = HcStyles.BlackTextColor,
                        FontSize = HcStyles.FontSizeContent,
                        LineBreakMode = LineBreakMode.WordWrap,
                        GestureRecognizers =
                        {   
                            new TapGestureRecognizer
                            {
                                Command = ReadMoreCommand,
                                CommandParameter = Text
                            }
                        }
                    });
                }
                else
                {
                    content.Content = (lblContent);
                }
            }
            else
            {
                content.Content = (lblContent);
            }

            Grid.SetRow(content, 1);
            Grid.SetColumnSpan(info, 2);
            Grid.SetColumnSpan(content, 2);

            _gridMain.Children.Add(info);
            _gridMain.Children.Add(content);

            if (CreatedByUserId == "patient")
            {
                lblOld.Text = MoreSupportViewModel2.Instance.SupportQuestionSelectedItem.BasicInfo;
                lblDatetime.Text =
                    MoreSupportViewModel2.Instance.SupportQuestionSelectedItem.WhenCreated.ToLocalTimeZone().ToString(
                        "MM/dd/yyyy hh:mm tt");
                lblName.Text = MoreSupportViewModel2.Instance.SupportQuestionSelectedItem.FullName;
                lblContent.Text = MoreSupportViewModel2.Instance.SupportQuestionSelectedItem.Symptom;
                if (!string.IsNullOrEmpty(MoreSupportViewModel2.Instance.SupportQuestionSelectedItem.PatientPhoto))
                {
                    imgAvatar.Source =
                        ImageSource.FromUri(
                            new Uri(MoreSupportViewModel2.Instance.SupportQuestionSelectedItem.PatientPhoto));
                }
                else
                {
                    if (IsQuestion)
                    {
                        imgAvatar.Source = (Gender == "Female"
                            ? AppConstant.AvatarPatientFeMale
                            : AppConstant.AvatarPatientMale);
                    }
                    else
                    {
                        imgAvatar.Source = (Gender == "Female"
                            ? AppConstant.AvatarDoctorFeMale
                            : AppConstant.AvatarDoctorMale);
                    }
                }

                var stack = new StackLayout
                {
                    Padding = new Thickness(5),
                    Spacing = 10
                };
                stack.Children.Add(_gridMain);
                var lblAnswerContent = new ContentView
                {
                    BackgroundColor = Color.FromHex("#5bb947"),
                    HeightRequest = Common.OnPlatform(20, 20, 40),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = new Thickness(10, 5, 0, 5),
                    Content = new Label
                    {
                        Text = MoreSupportViewModel2.Instance.SupportQuestionSelectedItem.ReplyCountString,
                        FontAttributes = FontAttributes.Bold,
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = HcStyles.FontSizeContent,
                        TextColor = Color.White
                    }
                };
                stack.Children.Add(lblAnswerContent);
                Content = stack;
            }
            else
            {
                _gridMain.BackgroundColor = HcStyles.QAColor;
                var stack = new StackLayout
                {
                    BackgroundColor = Color.White,
                    Padding = new Thickness(5)
                };
                stack.Children.Add(_gridMain);
                Content = stack;
            }
        }

        private string RemoveSpace(string txt)
        {
            while (txt.IndexOf("\n\n") != -1)
            {
                txt = txt.Replace("\n\n", "\n");
            }
            return txt;
        }

        private string RemoveNewLine(string txt)
        {
            while (txt.IndexOf("\n") != -1)
            {
                txt = txt.Replace("\n", " ");
            }
            return txt;
        }

        private List<string> SplitText(string txt)
        {
            var listString = new List<string>();
            var count = 1000;
            while (txt.Length > 1000)
            {
                var end = false;
                var temp = txt.Substring(0, count);
                while ((temp[temp.Length - 2] != '\\') && (temp[temp.Length - 2] != 'n'))
                {
                    count++;
                    try
                    {
                        temp += txt.Substring(count, 1);
                    }
                    catch
                    {
                        count--;
                        end = true;
                        break;
                    }
                }
                listString.Add(temp);
                txt = txt.Remove(0, count);
                if (end)
                    break;
                //if (txt.Length >= 1000)
                //    count = 1000;
                //else
                //    count = txt.Length;
            }

            listString.Add(txt);
            return listString;
        }
    }
}