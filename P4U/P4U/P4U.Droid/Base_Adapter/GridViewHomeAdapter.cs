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
            ImageView imageView;

            if (convertView == null)
            {
                // if it's not recycled, initialize some attributes
                imageView = new ImageView(context);
                imageView.LayoutParameters = new AbsListView.LayoutParams(100,100);
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