package healthcare.droid.views;


public class CheckupView
	extends md52531c11624a119d534fceefe7f0bbf11.MvxActionBarActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateOptionsMenu:(Landroid/view/Menu;)Z:GetOnCreateOptionsMenu_Landroid_view_Menu_Handler\n" +
			"";
		mono.android.Runtime.register ("HealthCare.Droid.Views.CheckupView, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CheckupView.class, __md_methods);
	}


	public CheckupView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CheckupView.class)
			mono.android.TypeManager.Activate ("HealthCare.Droid.Views.CheckupView, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public boolean onCreateOptionsMenu (android.view.Menu p0)
	{
		return n_onCreateOptionsMenu (p0);
	}

	private native boolean n_onCreateOptionsMenu (android.view.Menu p0);

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
