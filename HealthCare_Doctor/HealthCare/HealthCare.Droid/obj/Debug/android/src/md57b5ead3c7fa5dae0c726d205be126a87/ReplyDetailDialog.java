package md57b5ead3c7fa5dae0c726d205be126a87;


public class ReplyDetailDialog
	extends md5a8f025a8cad3d863cfc102eacf93dc45.MvxDialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateDialog:(Landroid/os/Bundle;)Landroid/app/Dialog;:GetOnCreateDialog_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("HealthCare.Droid.Views.ReplyDetailDialog, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ReplyDetailDialog.class, __md_methods);
	}


	public ReplyDetailDialog () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ReplyDetailDialog.class)
			mono.android.TypeManager.Activate ("HealthCare.Droid.Views.ReplyDetailDialog, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public android.app.Dialog onCreateDialog (android.os.Bundle p0)
	{
		return n_onCreateDialog (p0);
	}

	private native android.app.Dialog n_onCreateDialog (android.os.Bundle p0);

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
