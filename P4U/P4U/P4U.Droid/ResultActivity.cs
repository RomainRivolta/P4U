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

namespace P4U.Droid
{
    [Activity(Label = "P4U")]
    public class ResultActivity : AppCompatActivity
    {
        private string SELECT_TYPE;
        private const int MAX_WIDTH=160;
        private const int MAX_HEIGHT= 160;
        private string LONGITUDE;
        private string LATITUDE;
        private int RADIUS = 10000;
        private List<PlaceSearch> RESULT_PLACE_SEARCH;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ResultSearch);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "k";

            SELECT_TYPE = Intent.GetStringExtra("SelectType");
            LONGITUDE = Intent.GetStringExtra("longitude");
            LATITUDE  = Intent.GetStringExtra("latitude");

            Core core = new Core();

            string query = core.TextSearchRequestsByLocation(LONGITUDE, LATITUDE, RADIUS, SELECT_TYPE);
            Console.WriteLine(query);
            RESULT_PLACE_SEARCH = core.GetPlaceSearch(query, MAX_WIDTH, MAX_HEIGHT).Result;
            FillListView(RESULT_PLACE_SEARCH);

            Button buttonSearch = FindViewById<Button>(Resource.Id.search);
            buttonSearch.Click += ButtonSearch_Click;

            ListView lstView = FindViewById<ListView>(Resource.Id.listViewResult);
            lstView.ItemClick += ItemListView_Click;
        }

        private void ItemListView_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            Core core = new Core();
            if (e.Position != RESULT_PLACE_SEARCH.Count)
            {
                string query = core.PlaceDetailsResponses(RESULT_PLACE_SEARCH[e.Position].PlaceId);
                var lstPlaceDetails = core.GetPlaceDetails(query).Result;

                Intent resultActivity = new Intent(this, typeof(DetailActivity));
                var res = Newtonsoft.Json.JsonConvert.SerializeObject(lstPlaceDetails);
                resultActivity.PutExtra("ResultMatrix", res);
                StartActivity(resultActivity);
            }
            else
            {
                string query = core.TextSearchRequestsByLocation(LONGITUDE, LATITUDE, RADIUS, SELECT_TYPE, pagetoken: RESULT_PLACE_SEARCH.FirstOrDefault().PageToken);
                RESULT_PLACE_SEARCH = core.GetPlaceSearch(query, MAX_WIDTH, MAX_HEIGHT).Result;
                RefreshListView(RESULT_PLACE_SEARCH);
            }
        }

        void ButtonSearch_Click(object sender, EventArgs eventArgs)
        {
            string search = FindViewById<TextView>(Resource.Id.editTextSearch).Text;

            if (!string.IsNullOrEmpty(search))
            {
                Core core = new Core();
                string query = core.TextSearchRequestsBySearch(SELECT_TYPE, search);
                RESULT_PLACE_SEARCH = core.GetPlaceSearch(query,MAX_WIDTH,MAX_HEIGHT).Result;
                FillListView(RESULT_PLACE_SEARCH);
            }
        }

        private void FillListView(List<PlaceSearch> result)
        {
            if (result != null)
            {
                ListView lstview = FindViewById<ListView>(Resource.Id.listViewResult);
                lstview.Adapter = new Base_Adapter.ListViewAdapter(this, result);
            }
        }

        private void RefreshListView(List<PlaceSearch> result)
        {
            if (result != null)
            {
                ListView lstview = FindViewById<ListView>(Resource.Id.listViewResult);
                ((Base_Adapter.ListViewAdapter)lstview.Adapter).AddIListItems(result);
            }
        }
    }
}