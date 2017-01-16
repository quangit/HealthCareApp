// Decompiled with JetBrains decompiler
// Type: GalaSoft.MvvmLight.Views.INavigationService
// Assembly: GalaSoft.MvvmLight, Version=5.2.0.37222, Culture=neutral, PublicKeyToken=e7570ab207bcb616
// MVID: CD658FBF-4288-4906-BA0F-BFEBD0A62515
// Assembly location: E:\Du lieu\SDC workspace\Xamarin\HealthCare CLAS\Mobile\HealthCare\packages\MvvmLightLibs.5.2.0.0\lib\monoandroid1\GalaSoft.MvvmLight.dll

using System;
using Xamarin.Forms;

namespace HealthCare.Services.Interfaces
{
    /// <summary>
    ///     An interface defining how navigation between pages should
    ///     be performed in various frameworks such as Windows,
    ///     Windows Phone, Android, iOS etc.
    /// </summary>
    public interface INavigationService
    {
        bool IsInitialized { get; }

        /// <summary>
        ///     The key corresponding to the currently displayed page.
        /// </summary>
        string CurrentPageKey { get; }

        /// <summary>
        ///     The key corresponding to the currently displayed page.
        /// </summary>
        Type CurrentPageType { get; }

        void Initialize(NavigationPage navigation);

        /// <summary>
        ///     If possible, instructs the navigation service
        ///     to discard the current page and display the previous page
        ///     on the navigation stack.
        /// </summary>
        void GoBack();

        /// <summary>
        ///     Instructs the navigation service to display a new page
        ///     corresponding to the given key. Depending on the platforms,
        ///     the navigation service might have to be configured with a
        ///     key/page list.
        /// </summary>
        /// <param name="pageType">
        ///     The key corresponding to the page
        ///     that should be displayed.
        /// </param>
        void NavigateTo(Type pageType);

        /// <summary>
        ///     Instructs the navigation service to display a new page
        ///     corresponding to the given key, and passes a parameter
        ///     to the new page.
        ///     Depending on the platforms, the navigation service might
        ///     have to be Configure with a key/page list.
        /// </summary>
        /// <param name="pageType">
        ///     The key corresponding to the page
        ///     that should be displayed.
        /// </param>
        /// <param name="parameter">
        ///     The parameter that should be passed
        ///     to the new page.
        /// </param>
        void NavigateTo(Type pageType, params object[] parameter);

        /// <summary>
        ///     Instructs the navigation service to display a new page
        ///     corresponding to the given key. Depending on the platforms,
        ///     the navigation service might have to be configured with a
        ///     key/page list.
        /// </summary>
        /// <param name="pageKey">
        ///     The key corresponding to the page
        ///     that should be displayed.
        /// </param>
        void NavigateTo(string pageKey);

        /// <summary>
        ///     Instructs the navigation service to display a new page
        ///     corresponding to the given key, and passes a parameter
        ///     to the new page.
        ///     Depending on the platforms, the navigation service might
        ///     have to be Configure with a key/page list.
        /// </summary>
        /// <param name="pageKey">
        ///     The key corresponding to the page
        ///     that should be displayed.
        /// </param>
        /// <param name="parameter">
        ///     The parameter that should be passed
        ///     to the new page.
        /// </param>
        void NavigateTo(string pageKey, params object[] parameter);

        void ReplaceRootPage(Type toPageType);
    }
}