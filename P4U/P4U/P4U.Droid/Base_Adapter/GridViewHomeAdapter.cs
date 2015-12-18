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
    public class GridViewHomeAdapter: BaseAdapter
    {
        Context context;

        public GridViewHomeAdapter(Context c)
        {
            this.context = c;
        }

        public override int Count
        {
            get
            {
                return thumbIds.Length;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ImageView imageView;

            if (convertView == null)
            {
                // if it's not recycled, initialize some attributes
                imageView = new ImageView(context);
                //imageView.LayoutParameters = new AbsListView.LayoutParams(4, 4);
                imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
            }
            else
            {
                imageView = (ImageView)convertView;
            }
            imageView.SetImageResource(thumbIds[position]);
            return imageView;
        }

        private readonly int[] thumbIds = {
            Resource.Drawable.ic_location, Resource.Drawable.ic_food,
            Resource.Drawable.ic_parking, Resource.Drawable.ic_airport
        };

    }
}