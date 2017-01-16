package md59b9c6274a50112b5e1acc6ab989960bd;


public class HelloWebViewClient
	extends android.webkit.WebViewClient
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_shouldOverrideUrlLoading:(Landroid/webkit/WebView;Ljava/lang/String;)Z:GetShouldOverrideUrlLoading_Landroid_webkit_WebView_Ljava_lang_String_Handler\n" +
			"";
		mono.android.Runtime.register ("HealthCare.Droid.Views.Fragments.HelloWebViewClient, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", HelloWebViewClient.class, __md_methods);
	}


	public HelloWebViewClient () throws java.lang.Throwable
	{
		super ();
		if (getClass () == HelloWebViewClient.class)
			mono.android.TypeManager.Activate ("HealthCare.Droid.Views.Fragments.HelloWebViewClient, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public HelloWebViewClient (android.app.Activity p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == HelloWebViewClient.class)
			mono.android.TypeManager.Activate ("HealthCare.Droid.Views.Fragments.HelloWebViewClient, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.App.Activity, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public boolean shouldOverrideUrlLoading (android.webkit.WebView p0, java.lang.String p1)
	{
		return n_shouldOverrideUrlLoading (p0, p1);
	}

	private native boolean n_shouldOverrideUrlLoading (android.webkit.WebView p0, java.lang.String p1);

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
