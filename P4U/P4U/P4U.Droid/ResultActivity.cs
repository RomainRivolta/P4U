using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P4U.Droid
{
    [Activity(Label = "P4U")]
    public class ResultActivity : AppCompatActivity
    {
        private string SELECT_TYPE;
        private string QUERY;
        private const int MAX_WIDTH=160;
        private const int MAX_HEIGHT= 160;
        private int RADIUS = 10000;
        private List<PlaceSearch> RESULT_PLACE_SEARCH;
        private Core core = new Core();

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

            SetContentView(Resource.Layout.ResultSearch);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            string query = string.Empty;

            core.latitude = Intent.GetStringExtra("latitude");
            core.longitude = Intent.GetStringExtra("longitude");

            if (string.IsNullOrEmpty(Intent.GetStringExtra("SelectType")))
            {
                QUERY = Intent.GetStringExtra("Search");
                query = core.TextSearchRequestsBySearch(QUERY);
            }

            else
            {
                SELECT_TYPE = Intent.GetStringExtra("SelectType");
                QUERY = SELECT_TYPE;
                SupportActionBar.Title = Intent.GetStringExtra("SelectName");
                query = core.TextSearchRequestsByLocation(RADIUS, QUERY, SELECT_TYPE);
            }

            RESULT_PLACE_SEARCH = core.GetPlaceSearch(query, MAX_WIDTH, MAX_HEIGHT).Result;
            FillListView(RESULT_PLACE_SEARCH);

            Button buttonSearch = FindViewById<Button>(Resource.Id.search);
            buttonSearch.Click += ButtonSearch_Click;

            ListView lstView = FindViewById<ListView>(Resource.Id.listViewResult);
            lstView.ItemClick += ItemListView_Click;
        }

        private void ItemListView_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e.Position != RESULT_PLACE_SEARCH.Count)
            {
                //string query = core.PlaceDetailsResponses(RESULT_PLACE_SEARCH[e.Position].PlaceId);
                //var lstPlaceDetails = core.GetPlaceDetails(query).Result;

                Intent resultActivity = new Intent(this, typeof(DetailActivity));
                //var res = Newtonsoft.Json.JsonConvert.SerializeObject(lstPlaceDetails);
                //resultActivity.PutExtra("ResultMatrix", res);
                resultActivity.PutExtra("ResulPlaceId", RESULT_PLACE_SEARCH[e.Position].PlaceId);
                StartActivity(resultActivity);
            }
            else
            {
                string query = core.TextSearchRequestsByLocation(RADIUS, QUERY, SELECT_TYPE, pagetoken: RESULT_PLACE_SEARCH.FirstOrDefault().PageToken);
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
                string query = core.TextSearchRequestsBySearch(search);
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