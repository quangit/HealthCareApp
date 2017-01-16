// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using System.CodeDom.Compiler;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Sample
{
	[Register ("SampleViewController")]
	partial class SampleViewController
	{
		[Outlet]
		UIButton BtnDate { get; set; }

		[Outlet]
		UIButton BtnDateTime { get; set; }

		[Outlet]
		UIButton BtnLogin { get; set; }

		[Outlet]
		UIButton BtnPlainEvent { get; set; }

		[Outlet]
		UIButton BtnSearch { get; set; }

		[Outlet]
		UIButton BtnTime { get; set; }

		[Outlet]
		UITextField TxtDatePicker { get; set; }

		[Outlet]
		UITextField TxtDateTimePicker { get; set; }

		[Outlet]
		UITextField TxtListItems { get; set; }

		[Outlet]
		UITextField TxtTimePicker { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (TxtListItems != null) {
				TxtListItems.Dispose ();
				TxtListItems = null;
			}

			if (TxtDatePicker != null) {
				TxtDatePicker.Dispose ();
				TxtDatePicker = null;
			}

			if (TxtTimePicker != null) {
				TxtTimePicker.Dispose ();
				TxtTimePicker = null;
			}

			if (TxtDateTimePicker != null) {
				TxtDateTimePicker.Dispose ();
				TxtDateTimePicker = null;
			}

			if (BtnPlainEvent != null) {
				BtnPlainEvent.Dispose ();
				BtnPlainEvent = null;
			}

			if (BtnLogin != null) {
				BtnLogin.Dispose ();
				BtnLogin = null;
			}

			if (BtnSearch != null) {
				BtnSearch.Dispose ();
				BtnSearch = null;
			}

			if (BtnDate != null) {
				BtnDate.Dispose ();
				BtnDate = null;
			}

			if (BtnTime != null) {
				BtnTime.Dispose ();
				BtnTime = null;
			}

			if (BtnDateTime != null) {
				BtnDateTime.Dispose ();
				BtnDateTime = null;
			}
		}
	}
}
