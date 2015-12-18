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

namespace P4U.Droid.Base_Adapter
{
    public class ListViewAdapter : BaseAdapter<PlaceSearch>
    {
        Activity context;
        List<PlaceSearch> items;
            
       public ListViewAdapter(Activity context,List<PlaceSearch> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override PlaceSearch this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            var item = items[position];

            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = string.Format("{0}", item.Name);
            return view;
        }

    }
}