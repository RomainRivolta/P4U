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
using Java.Lang;

namespace P4U.Droid.Base_Adapter
{
    public class GridViewHomeAdapter: BaseAdapter<GridViewHome>
    {
        Context context;
        List<GridViewHome>items;

        public GridViewHomeAdapter(Context c,List<GridViewHome> items):base()
        {
            this.context = c;
            this.items = items;
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override GridViewHome this[int position]
        {
            get { return items[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            if (convertView == null)
            {
                convertView = inflater.Inflate(Resource.Layout.CustomGridViewHome,null);
            }
            convertView.FindViewById<ImageView>(Resource.Id.imageViewHomeIcon).SetImageResource(items[position].Image);
            convertView.FindViewById<TextView>(Resource.Id.grid_text).Text = items[position].Name;
            return convertView;
        }
    }
}