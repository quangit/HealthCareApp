﻿#pragma checksum "D:\SVN\HealthcareProject\HealthCare_Doctor\HealthCare\Phase 2\HealthCare.Win\Views\HomeTab\WeekTopicView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9F68D98D140BE7CAA23FA20178AC49EE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HealthCare.Win.Views.HomeTab
{
    partial class WeekTopicView : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_UIElement_Visibility(global::Windows.UI.Xaml.UIElement obj, global::Windows.UI.Xaml.Visibility value)
            {
                obj.Visibility = value;
            }
        };

        private class WeekTopicView_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IWeekTopicView_Bindings
        {
            private global::HealthCare.Win.Views.HomeTab.WeekTopicView dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private global::Windows.UI.Xaml.ResourceDictionary localResources;
            private global::System.WeakReference<global::Windows.UI.Xaml.FrameworkElement> converterLookupRoot;

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.TextBlock obj6;

            private WeekTopicView_obj1_BindingsTracking bindingsTracking;

            public WeekTopicView_obj1_Bindings()
            {
                this.bindingsTracking = new WeekTopicView_obj1_BindingsTracking(this);
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 6:
                        this.obj6 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    default:
                        break;
                }
            }

            // IWeekTopicView_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            // WeekTopicView_obj1_Bindings

            public void SetDataRoot(global::HealthCare.Win.Views.HomeTab.WeekTopicView newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.dataRoot = newDataRoot;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }
            public void SetConverterLookupRoot(global::Windows.UI.Xaml.FrameworkElement rootElement)
            {
                this.converterLookupRoot = new global::System.WeakReference<global::Windows.UI.Xaml.FrameworkElement>(rootElement);
            }

            public global::Windows.UI.Xaml.Data.IValueConverter LookupConverter(string key)
            {
                if (this.localResources == null)
                {
                    global::Windows.UI.Xaml.FrameworkElement rootElement;
                    this.converterLookupRoot.TryGetTarget(out rootElement);
                    this.localResources = rootElement.Resources;
                    this.converterLookupRoot = null;
                }
                return (global::Windows.UI.Xaml.Data.IValueConverter) (this.localResources.ContainsKey(key) ? this.localResources[key] : global::Windows.UI.Xaml.Application.Current.Resources[key]);
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::HealthCare.Win.Views.HomeTab.WeekTopicView obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_ViewModel(obj.ViewModel, phase);
                    }
                }
            }
            private void Update_ViewModel(global::HealthCare.Core.ViewModels.HomeViewModel obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_ViewModel(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_ViewModel_WeekTopics(obj.WeekTopics, phase);
                    }
                }
            }
            private void Update_ViewModel_WeekTopics(global::System.Collections.ObjectModel.ObservableCollection<global::HealthCare.Core.Models.Topic> obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_UIElement_Visibility(this.obj6, (global::Windows.UI.Xaml.Visibility)this.LookupConverter("SourceToVisibilityConverter").Convert(obj, typeof(global::Windows.UI.Xaml.Visibility), null, null));
                }
            }

            private class WeekTopicView_obj1_BindingsTracking
            {
                global::System.WeakReference<WeekTopicView_obj1_Bindings> WeakRefToBindingObj; 

                public WeekTopicView_obj1_BindingsTracking(WeekTopicView_obj1_Bindings obj)
                {
                    WeakRefToBindingObj = new global::System.WeakReference<WeekTopicView_obj1_Bindings>(obj);
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_ViewModel(null);
                }

                public void PropertyChanged_ViewModel(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    WeekTopicView_obj1_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        string propName = e.PropertyName;
                        global::HealthCare.Core.ViewModels.HomeViewModel obj = sender as global::HealthCare.Core.ViewModels.HomeViewModel;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                            if (obj != null)
                            {
                                    bindings.Update_ViewModel_WeekTopics(obj.WeekTopics, DATA_CHANGED);
                            }
                        }
                        else
                        {
                            switch (propName)
                            {
                                case "WeekTopics":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_WeekTopics(obj.WeekTopics, DATA_CHANGED);
                                    }
                                    break;
                                }
                                default:
                                    break;
                            }
                        }
                    }
                }
                private global::HealthCare.Core.ViewModels.HomeViewModel cache_ViewModel = null;
                public void UpdateChildListeners_ViewModel(global::HealthCare.Core.ViewModels.HomeViewModel obj)
                {
                    if (obj != cache_ViewModel)
                    {
                        if (cache_ViewModel != null)
                        {
                            ((global::System.ComponentModel.INotifyPropertyChanged)cache_ViewModel).PropertyChanged -= PropertyChanged_ViewModel;
                            cache_ViewModel = null;
                        }
                        if (obj != null)
                        {
                            cache_ViewModel = obj;
                            ((global::System.ComponentModel.INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_ViewModel;
                        }
                    }
                }
                public void PropertyChanged_ViewModel_WeekTopics(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    WeekTopicView_obj1_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        string propName = e.PropertyName;
                        global::System.Collections.ObjectModel.ObservableCollection<global::HealthCare.Core.Models.Topic> obj = sender as global::System.Collections.ObjectModel.ObservableCollection<global::HealthCare.Core.Models.Topic>;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                        }
                        else
                        {
                            switch (propName)
                            {
                                default:
                                    break;
                            }
                        }
                    }
                }
                public void CollectionChanged_ViewModel_WeekTopics(object sender, global::System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
                {
                    WeekTopicView_obj1_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        global::System.Collections.ObjectModel.ObservableCollection<global::HealthCare.Core.Models.Topic> obj = sender as global::System.Collections.ObjectModel.ObservableCollection<global::HealthCare.Core.Models.Topic>;
                    }
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2:
                {
                    global::Windows.UI.Xaml.Controls.Grid element2 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    #line 395 "..\..\..\..\Views\HomeTab\WeekTopicView.xaml"
                    ((global::Windows.UI.Xaml.Controls.Grid)element2).Loaded += this.AppBarButtonLoaded;
                    #line default
                }
                break;
            case 3:
                {
                    this.LayoutRoot = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 4:
                {
                    global::Windows.UI.Xaml.Controls.Pivot element4 = (global::Windows.UI.Xaml.Controls.Pivot)(target);
                    #line 690 "..\..\..\..\Views\HomeTab\WeekTopicView.xaml"
                    ((global::Windows.UI.Xaml.Controls.Pivot)element4).SelectionChanged += this.Pivot_OnSelectionChanged;
                    #line default
                }
                break;
            case 5:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element5 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 834 "..\..\..\..\Views\HomeTab\WeekTopicView.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element5).Click += this.RefreshClick;
                    #line default
                }
                break;
            case 7:
                {
                    global::Windows.UI.Xaml.Controls.Border element7 = (global::Windows.UI.Xaml.Controls.Border)(target);
                    #line 799 "..\..\..\..\Views\HomeTab\WeekTopicView.xaml"
                    ((global::Windows.UI.Xaml.Controls.Border)element7).Tapped += this.PdfOpen;
                    #line default
                }
                break;
            case 8:
                {
                    global::Windows.UI.Xaml.Controls.Border element8 = (global::Windows.UI.Xaml.Controls.Border)(target);
                    #line 814 "..\..\..\..\Views\HomeTab\WeekTopicView.xaml"
                    ((global::Windows.UI.Xaml.Controls.Border)element8).Tapped += this.ItemTapped;
                    #line default
                }
                break;
            case 9:
                {
                    this.txtAll = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    WeekTopicView_obj1_Bindings bindings = new WeekTopicView_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    bindings.SetConverterLookupRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            }
            return returnValue;
        }
    }
}

