package md59b9c6274a50112b5e1acc6ab989960bd;


public class MyEvent
	extends com.telerik.widget.calendar.events.Event
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("HealthCare.Droid.Views.Fragments.MyEvent, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyEvent.class, __md_methods);
	}


	public MyEvent (java.lang.String p0, long p1, long p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == MyEvent.class)
			mono.android.TypeManager.Activate ("HealthCare.Droid.Views.Fragments.MyEvent, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int64, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int64, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}

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
