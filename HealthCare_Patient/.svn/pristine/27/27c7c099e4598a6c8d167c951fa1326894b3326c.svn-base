using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HealthCare.Helpers;
using HealthCare.Pages;
using HealthCare.Services.Interfaces;
using Xamarin.Forms;

namespace HealthCare.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
        private NavigationPage _navigation;

        public string CurrentPageKey
        {
            get
            {
                lock (_pagesByKey)
                {
                    if (_navigation.CurrentPage == null)
                    {
                        return null;
                    }

                    var pageType = _navigation.CurrentPage.GetType();

                    return _pagesByKey.ContainsValue(pageType)
                        ? _pagesByKey.First(p => p.Value == pageType).Key
                        : null;
                }
            }
        }

        public Type CurrentPageType
        {
            get
            {
                lock (_pagesByKey)
                {
                    if (_navigation.CurrentPage == null)
                    {
                        return null;
                    }

                    var pageType = _navigation.CurrentPage.GetType();

                    return _pagesByKey.ContainsValue(pageType)
                        ? _pagesByKey.First(p => p.Value == pageType).Value
                        : null;
                }
            }
        }

        public void GoBack()
        {
            _navigation.PopAsync();
        }

        public void NavigateTo(Type pageType)
        {
            NavigateTo(pageType.FullName, null);
        }

        public void NavigateTo(Type pageType, params object[] parameter)
        {
            NavigateTo(pageType.FullName, parameter);
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, params object[] parametersObjects)
        {
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    var type = _pagesByKey[pageKey];
                    ConstructorInfo constructor;
                    object[] parameters;

                    if (parametersObjects == null)
                    {
                        constructor = type.GetTypeInfo()
                            .DeclaredConstructors
                            .FirstOrDefault(c => !c.GetParameters().Any());

                        parameters = new object[]
                        {
                        };
                    }
                    else
                    {
                        constructor = type.GetTypeInfo()
                            .DeclaredConstructors
                            .FirstOrDefault(
                                c =>
                                {
                                    var p = c.GetParameters();
                                    return p.Count() == 1
                                           && p[0].ParameterType == parametersObjects.GetType();
                                });

                        parameters = parametersObjects;
                    }

                    if (constructor == null)
                    {
                        throw new InvalidOperationException(
                            "No suitable constructor found for page " + pageKey);
                    }

                    var page = constructor.Invoke(parameters) as Page;

                    _navigation.PushAsync(page, true).GetAwaiter().OnCompleted(new Action((async () =>
                    {
                        if (
                        !(page is HomeTabbedPage || page is SearchPage || page is AskDoctorPage || page is CheckupPage ||
                          page is PaymentPage || page is PhotoEditPage))
                        {
                            await Task.Delay(100);
                            page?.ToolbarItems?.Clear();
                        }
                    })));


                }
                else
                {
                    throw new ArgumentException(
                        string.Format(
                            "No such page: {0}. Did you forget to call NavigationService.Configure?",
                            pageKey),
                        "pageKey");
                }
            }
        }

        public void ReplaceRootPage(Type toPageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(toPageType.FullName))
                {
                    var type = _pagesByKey[toPageType.FullName];
                    ConstructorInfo constructor;
                    object[] parameters;

                    constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .FirstOrDefault(c => !c.GetParameters().Any());

                    parameters = new object[]
                    {
                    };

                    if (constructor == null)
                    {
                        throw new InvalidOperationException(
                            "No suitable constructor found for page " + toPageType.FullName);
                    }

                    var page = constructor.Invoke(parameters) as Page;

                    _navigation = new NavigationPage(page);

                    _navigation.Style = HcStyles.NavigationPageStyle;
                    App.Instance.MainPage = _navigation;
                }
                else
                {
                    throw new ArgumentException(
                        string.Format(
                            "No such page: {0}. Did you forget to call NavigationService.Configure?",
                            toPageType.FullName),
                        "pageKey");
                }
            }
        }

        public bool IsInitialized => _navigation != null;

        public void Initialize(NavigationPage navigation)
        {
            _navigation = navigation;
        }

        public void Configure(Type pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageType.FullName))
                {
                    _pagesByKey[pageType.FullName] = pageType;
                }
                else
                {
                    _pagesByKey.Add(pageType.FullName, pageType);
                }
            }
        }
    }
}