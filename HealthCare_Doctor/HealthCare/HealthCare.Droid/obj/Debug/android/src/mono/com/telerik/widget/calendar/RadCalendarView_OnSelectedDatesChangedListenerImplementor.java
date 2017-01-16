package mono.com.telerik.widget.calendar;


public class RadCalendarView_OnSelectedDatesChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.calendar.RadCalendarView.OnSelectedDatesChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onSelectedDatesChanged:(Lcom/telerik/widget/calendar/RadCalendarView$SelectionContext;)V:GetOnSelectedDatesChanged_Lcom_telerik_widget_calendar_RadCalendarView_SelectionContext_Handler:Com.Telerik.Widget.Calendar.RadCalendarView/IOnSelectedDatesChangedListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Calendar.RadCalendarView+IOnSelectedDatesChangedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2015.1.306.0, Culture=neutral, PublicKeyToken=null", RadCalendarView_OnSelectedDatesChangedListenerImplementor.class, __md_methods);
	}


	public RadCalendarView_OnSelectedDatesChangedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == RadCalendarView_OnSelectedDatesChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Calendar.RadCalendarView+IOnSelectedDatesChangedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2015.1.306.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onSelectedDatesChanged (com.telerik.widget.calendar.RadCalendarView.SelectionContext p0)
	{
		n_onSelectedDatesChanged (p0);
	}

	private native void n_onSelectedDatesChanged (com.telerik.widget.calendar.RadCalendarView.SelectionContext p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
