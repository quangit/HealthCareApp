package md589708ed6ec44ff2ee5a16f77867c7589;


public class LoadMoreMvxAdapter
	extends md53471cbf751f08dad2f5f63288aefa6f2.MvxAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("HealthCare.Droid.Controls.LoadMoreMvxAdapter, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LoadMoreMvxAdapter.class, __md_methods);
	}


	public LoadMoreMvxAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LoadMoreMvxAdapter.class)
			mono.android.TypeManager.Activate ("HealthCare.Droid.Controls.LoadMoreMvxAdapter, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public LoadMoreMvxAdapter (android.content.Context p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == LoadMoreMvxAdapter.class)
			mono.android.TypeManager.Activate ("HealthCare.Droid.Controls.LoadMoreMvxAdapter, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
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
