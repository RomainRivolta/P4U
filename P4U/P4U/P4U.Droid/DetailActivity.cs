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
using Android.Support.V7.App;
using Android.Support.V4.View;

namespace P4U.Droid
{
    [Activity(Label = "DetailActivity")]
    public class DetailActivity : AppCompatActivity
    {

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbar, menu);
            var item = menu.FindItem(Resource.Id.action_search);
            var searchView = MenuItemCompat.GetActionView(item);
            SearchView _searchView = searchView.JavaCast<SearchView>();
            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DetailsSearch);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Create your application here

            Core core = new Core();
            var resulPlaceId = Intent.GetStringExtra("ResulPlaceId"); 
            string query = core.PlaceDetailsResponses(resulPlaceId);
            PlaceDetails lstPlaceDetails = core.GetPlaceDetails(query).Result;

            FindViewById<TextView>(Resource.Id.textViewDetailsName).Text = lstPlaceDetails.Name;
            FindViewById<TextView>(Resource.Id.textViewDetailsAddress).Text = lstPlaceDetails.FormattedAddress;
            FindViewById<TextView>(Resource.Id.textViewDetailsWeb).Text = lstPlaceDetails.Website;
            FindViewById<TextView>(Resource.Id.textViewDetailsPhone).Text = lstPlaceDetails.InternationalPhoneNumber;
            FindViewById<RatingBar>(Resource.Id.ratingBar).Rating = float.Parse(lstPlaceDetails.Rating);



        }
    }
}