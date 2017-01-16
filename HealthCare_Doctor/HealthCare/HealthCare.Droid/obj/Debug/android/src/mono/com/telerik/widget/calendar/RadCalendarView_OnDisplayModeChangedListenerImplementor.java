package mono.com.telerik.widget.calendar;


public class RadCalendarView_OnDisplayModeChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.calendar.RadCalendarView.OnDisplayModeChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDisplayModeChanged:(Lcom/telerik/widget/calendar/CalendarDisplayMode;Lcom/telerik/widget/calendar/CalendarDisplayMode;)V:GetOnDisplayModeChanged_Lcom_telerik_widget_calendar_CalendarDisplayMode_Lcom_telerik_widget_calendar_CalendarDisplayMode_Handler:Com.Telerik.Widget.Calendar.RadCalendarView/IOnDisplayModeChangedListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Calendar.RadCalendarView+IOnDisplayModeChangedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2015.1.306.0, Culture=neutral, PublicKeyToken=null", RadCalendarView_OnDisplayModeChangedListenerImplementor.class, __md_methods);
	}


	public RadCalendarView_OnDisplayModeChangedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == RadCalendarView_OnDisplayModeChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Calendar.RadCalendarView+IOnDisplayModeChangedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2015.1.306.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onDisplayModeChanged (com.telerik.widget.calendar.CalendarDisplayMode p0, com.telerik.widget.calendar.CalendarDisplayMode p1)
	{
		n_onDisplayModeChanged (p0, p1);
	}

	private native void n_onDisplayModeChanged (com.telerik.widget.calendar.CalendarDisplayMode p0, com.telerik.widget.calendar.CalendarDisplayMode p1);

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
