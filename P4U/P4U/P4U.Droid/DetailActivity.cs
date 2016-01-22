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
        private const int MAX_WIDTH = 400;
        private const int MAX_HEIGHT = 200;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbar, menu);
            var item = menu.FindItem(Resource.Id.action_search);
            var searchView = MenuItemCompat.GetActionView(item);
            SearchView _searchView = searchView.JavaCast<SearchView>();
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_settings:
                    Intent intentSettings = new Intent(this, typeof(SettingsActivity));
                    StartActivity(intentSettings);
                    break;

            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DetailsSearch);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayShowHomeEnabled(false);
            SupportActionBar.SetLogo(Resource.Drawable.Icon);
            SupportActionBar.SetDisplayUseLogoEnabled(true);
            // Create your application here

            Core core = new Core();
            var resulPlaceId = Intent.GetStringExtra("ResulPlaceId"); 
            var resulPhoto = Intent.GetStringExtra("ResulPicture");
            string query = core.PlaceDetailsResponses(resulPlaceId);
            PlaceDetails placedetails = core.GetPlaceDetails(query).Result;
            placedetails.Picture = resulPhoto;
            SupportActionBar.Title = placedetails.Name;

            FindViewById<TextView>(Resource.Id.textViewDetailsName).Text = placedetails.Name;
            FindViewById<TextView>(Resource.Id.textViewDetailsAddress).Text = placedetails.FormattedAddress;
            FindViewById<TextView>(Resource.Id.textViewDetailsWeb).Text = placedetails.Website;
            FindViewById<TextView>(Resource.Id.textViewDetailsPhone).Text = placedetails.InternationalPhoneNumber;

            RatingBar ratingBar = FindViewById<RatingBar>(Resource.Id.ratingBar);
            ratingBar.Rating = float.Parse(placedetails.Rating);
            Android.Graphics.Drawables.LayerDrawable layerDrawable = (Android.Graphics.Drawables.LayerDrawable)ratingBar.ProgressDrawable;
            layerDrawable.GetDrawable(2).SetColorFilter(Android.Graphics.Color.ParseColor("#f24235"), Android.Graphics.PorterDuff.Mode.SrcAtop);

            ImageView imageViewPictureResult = FindViewById<ImageView>(Resource.Id.imageViewDetails);
            Square.Picasso.Picasso.With(this).Load(placedetails.Picture).Into(imageViewPictureResult);
        }
    }
}