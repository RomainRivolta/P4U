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
            get
            {
                var pageToken = items.First().PageToken;
                
                //if (!string.IsNullOrWhiteSpace(pageToken))
                //{
                    return items.Count + 1;
               // }
                //else
                //{
                  //  return items.Count;
                //}
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available

                if (view == null) // otherwise create a new one
                    view = context.LayoutInflater.Inflate(Resource.Layout.CustomListViewResult, null);

            TextView textViewNameResult = view.FindViewById<TextView>(Resource.Id.textViewNameResult);
            TextView textViewAddrResult = view.FindViewById<TextView>(Resource.Id.textViewAddrResult);
            TextView textViewLocationResult = view.FindViewById<TextView>(Resource.Id.textViewLocationResult);
            //TextView textViewPaysEtVilleEtCPResult = view.FindViewById<TextView>(Resource.Id.textViewPaysEtVilleEtCPResult);
            TextView textViewMoreData = view.FindViewById<TextView>(Resource.Id.textViewMoreData);
            TextView textViewDistance = view.FindViewById<TextView>(Resource.Id.textViewDistance);
            ImageView imageViewPictureResult = view.FindViewById<ImageView>(Resource.Id.imageViewPictureResult);

            
            if (position != Count - 1)
            {
                var item = items[position];

                string[] splitAddress = item.Address.Split(',');
                string location = splitAddress.Last();
                string addr = string.Empty;

                for (int i = 0; i < splitAddress.Length-1; i++)
                {
                    addr += splitAddress[i].Trim();
                }

                textViewNameResult.Text = item.Name;
                textViewNameResult.Visibility = ViewStates.Visible;

                textViewLocationResult.Text = location;
                textViewLocationResult.Visibility = ViewStates.Visible;

                //textViewPaysEtVilleEtCPResult.Text = addr;
                //textViewPaysEtVilleEtCPResult.Visibility = ViewStates.Visible;

                textViewAddrResult.Text = addr;
                textViewAddrResult.Visibility = ViewStates.Visible;

                textViewDistance.Text = item.Distance;
                textViewDistance.Visibility = ViewStates.Visible;

                textViewMoreData.Visibility = ViewStates.Gone;

                Square.Picasso.Picasso.With(context).Load(item.Picture).Into(imageViewPictureResult);
                imageViewPictureResult.Visibility = ViewStates.Visible;
            }
            else
            {
                textViewMoreData.Text = "Afficher plus de resultats...";
                textViewMoreData.Visibility = ViewStates.Visible;

                textViewNameResult.Visibility = ViewStates.Gone;
                //textViewPaysEtVilleEtCPResult.Visibility = ViewStates.Gone;
                //textViewRueResult.Visibility = ViewStates.Gone;
                textViewAddrResult.Visibility = ViewStates.Gone;
                textViewLocationResult.Visibility = ViewStates.Gone;
                imageViewPictureResult.Visibility = ViewStates.Gone;
                textViewDistance.Visibility = ViewStates.Gone;

            }
            return view;
        }

        public void AddIListItems(List<PlaceSearch> lstPlace)
        {
            this.items.AddRange(lstPlace);
            this.NotifyDataSetChanged();
        }


    }
}