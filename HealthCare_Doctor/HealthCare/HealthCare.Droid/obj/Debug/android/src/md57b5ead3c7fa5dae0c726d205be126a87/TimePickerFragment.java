package md57b5ead3c7fa5dae0c726d205be126a87;


public class TimePickerFragment
	extends android.app.DialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateDialog:(Landroid/os/Bundle;)Landroid/app/Dialog;:GetOnCreateDialog_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("HealthCare.Droid.Views.TimePickerFragment, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TimePickerFragment.class, __md_methods);
	}


	public TimePickerFragment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == TimePickerFragment.class)
			mono.android.TypeManager.Activate ("HealthCare.Droid.Views.TimePickerFragment, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public TimePickerFragment (android.content.Context p0, int p1, int p2, android.app.TimePickerDialog.OnTimeSetListener p3) throws java.lang.Throwable
	{
		super ();
		if (getClass () == TimePickerFragment.class)
			mono.android.TypeManager.Activate ("HealthCare.Droid.Views.TimePickerFragment, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:Android.App.TimePickerDialog+IOnTimeSetListener, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1, p2, p3 });
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
