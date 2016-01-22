using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Preferences;
using Java.Lang;
using Android.Content.Res;

namespace P4U.Droid
{
    public class NumberPickerPreference: DialogPreference
    {
        NumberPicker picker;
        int initialValue = -1;
        public NumberPickerPreference(Context context, IAttributeSet attributes) : base(context, attributes)
        {
            
        }

        protected override void OnBindDialogView(View view)
        {
            base.OnBindDialogView(view);
            this.picker = (NumberPicker)view.FindViewById(Resource.Id.pref_num_picker);

            picker.MinValue = 1;
            picker.MaxValue = 10;
            if(this.initialValue != -1)
            {
                picker.Value = this.initialValue;
            }

        }
        public override void OnClick(IDialogInterface dialog, int which)
        {
            base.OnClick(dialog, which);
            if(which == (int) DialogButtonType.Positive)
            {
                this.initialValue = picker.Value;
                PersistInt(this.initialValue);
                CallChangeListener(this.initialValue);
            }
        }
        protected override void OnSetInitialValue(bool restorePersistedValue, Java.Lang.Object defaultValue)
        {
            int def = (defaultValue  is Number ) ? (int)defaultValue
				: (defaultValue != null) ? Integer.ParseInt(defaultValue.ToString()) : 1;
            if (restorePersistedValue)
            {
                this.initialValue = GetPersistedInt(def);
            }
            else this.initialValue = (int)defaultValue;
            base.OnSetInitialValue(restorePersistedValue, defaultValue);
        }

        protected override Java.Lang.Object OnGetDefaultValue(TypedArray a, int index)
        {
            return a.GetInt(index, 1);
        }
    }
}