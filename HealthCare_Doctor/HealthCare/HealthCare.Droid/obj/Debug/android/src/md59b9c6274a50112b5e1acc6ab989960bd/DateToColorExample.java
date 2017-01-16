package md59b9c6274a50112b5e1acc6ab989960bd;


public class DateToColorExample
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.android.common.Function
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_apply:(Ljava/lang/Object;)Ljava/lang/Object;:GetApply_Ljava_lang_Object_Handler:Com.Telerik.Android.Common.IFunctionInvoker, Telerik.Xamarin.Android.Common\n" +
			"";
		mono.android.Runtime.register ("HealthCare.Droid.Views.Fragments.DateToColorExample, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DateToColorExample.class, __md_methods);
	}


	public DateToColorExample () throws java.lang.Throwable
	{
		super ();
		if (getClass () == DateToColorExample.class)
			mono.android.TypeManager.Activate ("HealthCare.Droid.Views.Fragments.DateToColorExample, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public java.lang.Object apply (java.lang.Object p0)
	{
		return n_apply (p0);
	}

	private native java.lang.Object n_apply (java.lang.Object p0);

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
