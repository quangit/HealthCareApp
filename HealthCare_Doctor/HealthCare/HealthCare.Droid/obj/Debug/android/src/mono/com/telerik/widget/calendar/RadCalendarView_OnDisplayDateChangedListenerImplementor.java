package mono.com.telerik.widget.calendar;


public class RadCalendarView_OnDisplayDateChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.calendar.RadCalendarView.OnDisplayDateChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDisplayDateChanged:(JJ)V:GetOnDisplayDateChanged_JJHandler:Com.Telerik.Widget.Calendar.RadCalendarView/IOnDisplayDateChangedListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Calendar.RadCalendarView+IOnDisplayDateChangedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2015.1.306.0, Culture=neutral, PublicKeyToken=null", RadCalendarView_OnDisplayDateChangedListenerImplementor.class, __md_methods);
	}


	public RadCalendarView_OnDisplayDateChangedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == RadCalendarView_OnDisplayDateChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Calendar.RadCalendarView+IOnDisplayDateChangedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2015.1.306.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onDisplayDateChanged (long p0, long p1)
	{
		n_onDisplayDateChanged (p0, p1);
	}

	private native void n_onDisplayDateChanged (long p0, long p1);

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
