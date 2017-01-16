package mono.com.telerik.android.primitives.widget.sidedrawer;


public class DrawerChangeListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.android.primitives.widget.sidedrawer.DrawerChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDrawerClosed:(Lcom/telerik/android/primitives/widget/sidedrawer/RadSideDrawer;)V:GetOnDrawerClosed_Lcom_telerik_android_primitives_widget_sidedrawer_RadSideDrawer_Handler:Com.Telerik.Android.Primitives.Widget.Sidedrawer.IDrawerChangeListenerInvoker, Telerik.Xamarin.Android.Primitives\n" +
			"n_onDrawerClosing:(Lcom/telerik/android/primitives/widget/sidedrawer/RadSideDrawer;)Z:GetOnDrawerClosing_Lcom_telerik_android_primitives_widget_sidedrawer_RadSideDrawer_Handler:Com.Telerik.Android.Primitives.Widget.Sidedrawer.IDrawerChangeListenerInvoker, Telerik.Xamarin.Android.Primitives\n" +
			"n_onDrawerOpened:(Lcom/telerik/android/primitives/widget/sidedrawer/RadSideDrawer;)V:GetOnDrawerOpened_Lcom_telerik_android_primitives_widget_sidedrawer_RadSideDrawer_Handler:Com.Telerik.Android.Primitives.Widget.Sidedrawer.IDrawerChangeListenerInvoker, Telerik.Xamarin.Android.Primitives\n" +
			"n_onDrawerOpening:(Lcom/telerik/android/primitives/widget/sidedrawer/RadSideDrawer;)Z:GetOnDrawerOpening_Lcom_telerik_android_primitives_widget_sidedrawer_RadSideDrawer_Handler:Com.Telerik.Android.Primitives.Widget.Sidedrawer.IDrawerChangeListenerInvoker, Telerik.Xamarin.Android.Primitives\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Android.Primitives.Widget.Sidedrawer.IDrawerChangeListenerImplementor, Telerik.Xamarin.Android.Primitives, Version=2015.1.306.0, Culture=neutral, PublicKeyToken=null", DrawerChangeListenerImplementor.class, __md_methods);
	}


	public DrawerChangeListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == DrawerChangeListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Android.Primitives.Widget.Sidedrawer.IDrawerChangeListenerImplementor, Telerik.Xamarin.Android.Primitives, Version=2015.1.306.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onDrawerClosed (com.telerik.android.primitives.widget.sidedrawer.RadSideDrawer p0)
	{
		n_onDrawerClosed (p0);
	}

	private native void n_onDrawerClosed (com.telerik.android.primitives.widget.sidedrawer.RadSideDrawer p0);


	public boolean onDrawerClosing (com.telerik.android.primitives.widget.sidedrawer.RadSideDrawer p0)
	{
		return n_onDrawerClosing (p0);
	}

	private native boolean n_onDrawerClosing (com.telerik.android.primitives.widget.sidedrawer.RadSideDrawer p0);


	public void onDrawerOpened (com.telerik.android.primitives.widget.sidedrawer.RadSideDrawer p0)
	{
		n_onDrawerOpened (p0);
	}

	private native void n_onDrawerOpened (com.telerik.android.primitives.widget.sidedrawer.RadSideDrawer p0);


	public boolean onDrawerOpening (com.telerik.android.primitives.widget.sidedrawer.RadSideDrawer p0)
	{
		return n_onDrawerOpening (p0);
	}

	private native boolean n_onDrawerOpening (com.telerik.android.primitives.widget.sidedrawer.RadSideDrawer p0);

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
