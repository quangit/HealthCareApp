// Decompiled with JetBrains decompiler
// Type: GalaSoft.MvvmLight.Views.IDialogService
// Assembly: GalaSoft.MvvmLight, Version=5.2.0.37222, Culture=neutral, PublicKeyToken=e7570ab207bcb616
// MVID: CD658FBF-4288-4906-BA0F-BFEBD0A62515
// Assembly location: E:\Du lieu\SDC workspace\Xamarin\HealthCare CLAS\Mobile\HealthCare\packages\MvvmLightLibs.5.2.0.0\lib\monoandroid1\GalaSoft.MvvmLight.dll

using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HealthCare.Services.Interfaces
{
    /// <summary>
    ///     An interface defining how dialogs should
    ///     be displayed in various frameworks such as Windows,
    ///     Windows Phone, Android, iOS etc.
    /// </summary>
    public interface IHcDialogService
    {
        bool IsInitialized { get; }
        void Initialize(Page dialogPage);

        /// <summary>
        ///     Displays information about an error.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="buttonText">
        ///     The text shown in the only button
        ///     in the dialog box. If left null, the text "OK" will be used.
        /// </param>
        /// <param name="afterHideCallback">
        ///     A callback that should be executed after
        ///     the dialog box is closed by the user.
        /// </param>
        /// <returns>
        ///     A Task allowing this async method to be awaited.
        /// </returns>
        Task ShowError(string message, string buttonText, Action afterHideCallback);

        /// <summary>
        ///     Displays information about an error.
        /// </summary>
        /// <param name="error">The exception of which the message must be shown to the user.</param>
        /// <param name="buttonText">
        ///     The text shown in the only button
        ///     in the dialog box. If left null, the text "OK" will be used.
        /// </param>
        /// <param name="afterHideCallback">
        ///     A callback that should be executed after
        ///     the dialog box is closed by the user.
        /// </param>
        /// <returns>
        ///     A Task allowing this async method to be awaited.
        /// </returns>
        Task ShowError(Exception error, string buttonText, Action afterHideCallback);

        /// <summary>
        ///     Displays information about an error.
        /// </summary>
        /// <param name="error">The exception of which the message must be shown to the user.</param>
        /// <returns>
        ///     A Task allowing this async method to be awaited.
        /// </returns>
        Task ShowError(Exception error);

        /// <summary>
        ///     Displays information to the user. The dialog box will have only
        ///     one button with the text "OK".
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <returns>
        ///     A Task allowing this async method to be awaited.
        /// </returns>
        Task ShowMessage(string message);

        /// <summary>
        ///     Displays information to the user. The dialog box will have only
        ///     one button.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="buttonText">
        ///     The text shown in the only button
        ///     in the dialog box. If left null, the text "OK" will be used.
        /// </param>
        /// <param name="afterHideCallback">
        ///     A callback that should be executed after
        ///     the dialog box is closed by the user.
        /// </param>
        /// <returns>
        ///     A Task allowing this async method to be awaited.
        /// </returns>
        Task ShowMessage(string message, string buttonText, Action afterHideCallback);

        /// <summary>
        ///     Displays information to the user. The dialog box will have only
        ///     one button.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="buttonConfirmText">
        ///     The text shown in the "confirm" button
        ///     in the dialog box. If left null, the text "OK" will be used.
        /// </param>
        /// <param name="buttonCancelText">
        ///     The text shown in the "cancel" button
        ///     in the dialog box. If left null, the text "Cancel" will be used.
        /// </param>
        /// <param name="afterHideCallback">
        ///     A callback that should be executed after
        ///     the dialog box is closed by the user. The callback method will get a boolean
        ///     parameter indicating if the "confirm" button (true) or the "cancel" button
        ///     (false) was pressed by the user.
        /// </param>
        /// <returns>
        ///     A Task allowing this async method to be awaited. The task will return
        ///     true or false depending on the dialog result.
        /// </returns>
        Task<bool> ShowMessage(string message, string buttonConfirmText, string buttonCancelText,
            Action<bool> afterHideCallback);

        /// <summary>
        ///     Displays information to the user in a simple dialog box. The dialog box will have only
        ///     one button with the text "OK". This method should be used for debugging purposes.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <returns>
        ///     A Task allowing this async method to be awaited.
        /// </returns>
        Task ShowMessageBox(string message);
    }
}