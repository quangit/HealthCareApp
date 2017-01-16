package mono.com.telerik.android.common;


public class CollectionChangeListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.android.common.CollectionChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_collectionChanged:(Lcom/telerik/android/common/CollectionChangedEvent;)V:GetCollectionChanged_Lcom_telerik_android_common_CollectionChangedEvent_Handler:Com.Telerik.Android.Common.ICollectionChangeListenerInvoker, Telerik.Xamarin.Android.Common\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Android.Common.ICollectionChangeListenerImplementor, Telerik.Xamarin.Android.Common, Version=2015.1.306.0, Culture=neutral, PublicKeyToken=null", CollectionChangeListenerImplementor.class, __md_methods);
	}


	public CollectionChangeListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CollectionChangeListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Android.Common.ICollectionChangeListenerImplementor, Telerik.Xamarin.Android.Common, Version=2015.1.306.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void collectionChanged (com.telerik.android.common.CollectionChangedEvent p0)
	{
		n_collectionChanged (p0);
	}

	private native void n_collectionChanged (com.telerik.android.common.CollectionChangedEvent p0);

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
