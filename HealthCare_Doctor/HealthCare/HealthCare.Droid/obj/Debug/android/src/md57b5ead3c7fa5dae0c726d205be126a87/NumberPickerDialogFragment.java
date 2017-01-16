package md57b5ead3c7fa5dae0c726d205be126a87;


public class NumberPickerDialogFragment
	extends android.app.DialogFragment
	implements
		mono.android.IGCUserPeer,
		android.widget.NumberPicker.OnValueChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateDialog:(Landroid/os/Bundle;)Landroid/app/Dialog;:GetOnCreateDialog_Landroid_os_Bundle_Handler\n" +
			"n_onValueChange:(Landroid/widget/NumberPicker;II)V:GetOnValueChange_Landroid_widget_NumberPicker_IIHandler:Android.Widget.NumberPicker/IOnValueChangeListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("HealthCare.Droid.Views.NumberPickerDialogFragment, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", NumberPickerDialogFragment.class, __md_methods);
	}


	public NumberPickerDialogFragment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == NumberPickerDialogFragment.class)
			mono.android.TypeManager.Activate ("HealthCare.Droid.Views.NumberPickerDialogFragment, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public android.app.Dialog onCreateDialog (android.os.Bundle p0)
	{
		return n_onCreateDialog (p0);
	}

	private native android.app.Dialog n_onCreateDialog (android.os.Bundle p0);


	public void onValueChange (android.widget.NumberPicker p0, int p1, int p2)
	{
		n_onValueChange (p0, p1, p2);
	}

	private native void n_onValueChange (android.widget.NumberPicker p0, int p1, int p2);

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
