package md57b5ead3c7fa5dae0c726d205be126a87;


public class MyDatePickerDialog_MyDateDialog
	extends android.app.DatePickerDialog
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDateChanged:(Landroid/widget/DatePicker;III)V:GetOnDateChanged_Landroid_widget_DatePicker_IIIHandler\n" +
			"";
		mono.android.Runtime.register ("HealthCare.Droid.Views.MyDatePickerDialog+MyDateDialog, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyDatePickerDialog_MyDateDialog.class, __md_methods);
	}


	public MyDatePickerDialog_MyDateDialog (android.content.Context p0, android.app.DatePickerDialog.OnDateSetListener p1, int p2, int p3, int p4) throws java.lang.Throwable
	{
		super (p0, p1, p2, p3, p4);
		if (getClass () == MyDatePickerDialog_MyDateDialog.class)
			mono.android.TypeManager.Activate ("HealthCare.Droid.Views.MyDatePickerDialog+MyDateDialog, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.App.DatePickerDialog+IOnDateSetListener, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2, p3, p4 });
	}


	public MyDatePickerDialog_MyDateDialog (android.content.Context p0, int p1, android.app.DatePickerDialog.OnDateSetListener p2, int p3, int p4, int p5) throws java.lang.Throwable
	{
		super (p0, p1, p2, p3, p4, p5);
		if (getClass () == MyDatePickerDialog_MyDateDialog.class)
			mono.android.TypeManager.Activate ("HealthCare.Droid.Views.MyDatePickerDialog+MyDateDialog, HealthCare.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:Android.App.DatePickerDialog+IOnDateSetListener, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2, p3, p4, p5 });
	}


	public void onDateChanged (android.widget.DatePicker p0, int p1, int p2, int p3)
	{
		n_onDateChanged (p0, p1, p2, p3);
	}

	private native void n_onDateChanged (android.widget.DatePicker p0, int p1, int p2, int p3);

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
