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

namespace P4U.Droid
{
    public class GridViewHome
    {
        public int Image { get; set; }
        public string Name { get; set; }

        public static List<GridViewHome> getGridViewHome()
        {
            List<GridViewHome> lstGrid = new List<GridViewHome>()
            {
                new GridViewHome(){ Image=Resource.Drawable.ic_location,Name="location"},
                new GridViewHome(){ Image=Resource.Drawable.ic_food,Name="restaurant"},
                new GridViewHome(){ Image=Resource.Drawable.ic_parking,Name="parking"},
                new GridViewHome(){ Image=Resource.Drawable.ic_airport,Name="location"},
            };
            return lstGrid;
        }
    }
}