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
        public string Type { get; set; }

        public static List<GridViewHome> getGridViewHome()
        {
            List<GridViewHome> lstGrid = new List<GridViewHome>()
            {
                new GridViewHome(){ Image=Resource.Drawable.ic_location,Type="parking|restaurant|airport|taxi_stand|gas_station|bus_station|train_station|subway_station|bar|cafe|shopping_mall",Name="Proximity"},
                new GridViewHome(){ Image=Resource.Drawable.ic_parking,Type="parking",Name="Parking"},
                new GridViewHome(){ Image=Resource.Drawable.ic_food,Type="restaurant",Name="Restaurant"},

                new GridViewHome(){ Image=Resource.Drawable.ic_bar,Type="bar",Name="Bar"},
                new GridViewHome(){ Image=Resource.Drawable.ic_cafe,Type="cafe",Name="Cafe"},
                new GridViewHome(){ Image=Resource.Drawable.ic_shopping_mall,Type="shopping_mall",Name="Shopping mall"},

                new GridViewHome(){ Image=Resource.Drawable.ic_cinema,Type="movie_theater",Name="Cinema"},
                new GridViewHome(){ Image=Resource.Drawable.ic_taxi_stand,Type="taxi_stand",Name="Taxi"},
                new GridViewHome(){ Image=Resource.Drawable.ic_gas_station,Type="gas_station",Name="Gas station"},

                new GridViewHome(){ Image=Resource.Drawable.ic_bus_station,Type="bus_station",Name="Bus station"},
                new GridViewHome(){ Image=Resource.Drawable.ic_train,Type="train_station",Name="Train station"},
                new GridViewHome(){ Image=Resource.Drawable.ic_subway,Type="subway_station",Name="Subway station"},
            };
            return lstGrid;
        }
    }
}