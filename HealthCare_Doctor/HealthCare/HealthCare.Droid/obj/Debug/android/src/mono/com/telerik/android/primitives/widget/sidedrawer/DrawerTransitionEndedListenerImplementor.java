package mono.com.telerik.android.primitives.widget.sidedrawer;


public class DrawerTransitionEndedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.android.primitives.widget.sidedrawer.DrawerTransitionEndedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTransitionEnded:(Lcom/telerik/android/primitives/widget/sidedrawer/DrawerTransition;)V:GetOnTransitionEnded_Lcom_telerik_android_primitives_widget_sidedrawer_DrawerTransition_Handler:Com.Telerik.Android.Primitives.Widget.Sidedrawer.IDrawerTransitionEndedListenerInvoker, Telerik.Xamarin.Android.Primitives\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Android.Primitives.Widget.Sidedrawer.IDrawerTransitionEndedListenerImplementor, Telerik.Xamarin.Android.Primitives, Version=2015.1.306.0, Culture=neutral, PublicKeyToken=null", DrawerTransitionEndedListenerImplementor.class, __md_methods);
	}


	public DrawerTransitionEndedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == DrawerTransitionEndedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Android.Primitives.Widget.Sidedrawer.IDrawerTransitionEndedListenerImplementor, Telerik.Xamarin.Android.Primitives, Version=2015.1.306.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onTransitionEnded (com.telerik.android.primitives.widget.sidedrawer.DrawerTransition p0)
	{
		n_onTransitionEnded (p0);
	}

	private native void n_onTransitionEnded (com.telerik.android.primitives.widget.sidedrawer.DrawerTransition p0);

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
