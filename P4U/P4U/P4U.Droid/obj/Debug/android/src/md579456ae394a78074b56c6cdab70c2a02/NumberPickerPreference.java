package md579456ae394a78074b56c6cdab70c2a02;


public class NumberPickerPreference
	extends android.preference.DialogPreference
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onBindDialogView:(Landroid/view/View;)V:GetOnBindDialogView_Landroid_view_View_Handler\n" +
			"n_onClick:(Landroid/content/DialogInterface;I)V:GetOnClick_Landroid_content_DialogInterface_IHandler\n" +
			"n_onSetInitialValue:(ZLjava/lang/Object;)V:GetOnSetInitialValue_ZLjava_lang_Object_Handler\n" +
			"n_onGetDefaultValue:(Landroid/content/res/TypedArray;I)Ljava/lang/Object;:GetOnGetDefaultValue_Landroid_content_res_TypedArray_IHandler\n" +
			"";
		mono.android.Runtime.register ("P4U.Droid.NumberPickerPreference, P4U.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", NumberPickerPreference.class, __md_methods);
	}


	public NumberPickerPreference (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == NumberPickerPreference.class)
			mono.android.TypeManager.Activate ("P4U.Droid.NumberPickerPreference, P4U.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public NumberPickerPreference (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == NumberPickerPreference.class)
			mono.android.TypeManager.Activate ("P4U.Droid.NumberPickerPreference, P4U.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public NumberPickerPreference (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == NumberPickerPreference.class)
			mono.android.TypeManager.Activate ("P4U.Droid.NumberPickerPreference, P4U.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public NumberPickerPreference (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3) throws java.lang.Throwable
	{
		super (p0, p1, p2, p3);
		if (getClass () == NumberPickerPreference.class)
			mono.android.TypeManager.Activate ("P4U.Droid.NumberPickerPreference, P4U.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public void onBindDialogView (android.view.View p0)
	{
		n_onBindDialogView (p0);
	}

	private native void n_onBindDialogView (android.view.View p0);


	public void onClick (android.content.DialogInterface p0, int p1)
	{
		n_onClick (p0, p1);
	}

	private native void n_onClick (android.content.DialogInterface p0, int p1);


	public void onSetInitialValue (boolean p0, java.lang.Object p1)
	{
		n_onSetInitialValue (p0, p1);
	}

	private native void n_onSetInitialValue (boolean p0, java.lang.Object p1);


	public java.lang.Object onGetDefaultValue (android.content.res.TypedArray p0, int p1)
	{
		return n_onGetDefaultValue (p0, p1);
	}

	private native java.lang.Object n_onGetDefaultValue (android.content.res.TypedArray p0, int p1);

	java.util.ArrayList refList;
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
