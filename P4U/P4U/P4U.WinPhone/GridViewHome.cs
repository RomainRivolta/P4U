using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace P4U.WinPhone
{
    public class GridViewHome
    {
        public string ImageType { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public static List<GridViewHome> getGridViewHome()
        {
            List<GridViewHome> lstGrid = new List<GridViewHome>()
            {
                new GridViewHome(){ ImageType="Images/ic_location.png",Type="parking|restaurant|airport|taxi_stand|gas_station|bus_station|train_station|subway_station|bar|cafe|shopping_mall",Name="Proximity"},
                new GridViewHome(){ ImageType="Images/ic_parking.png",Type="parking",Name="Parking"},
                new GridViewHome(){ ImageType="Images/ic_food.png", Type="restaurant",Name="Restaurant"},

                new GridViewHome(){ ImageType="Images/ic_bar.png",Type="bar",Name="Bar"},
                new GridViewHome(){ ImageType="Images/ic_cafe.png",Type="cafe",Name="Cafe"},
                new GridViewHome(){ ImageType="Images/ic_shopping_mall.png",Type="shopping_mall",Name="Shopping mall"},

                new GridViewHome(){ ImageType="Images/ic_cinema.png",Type="movie_theater",Name="Cinema"},
                new GridViewHome(){ ImageType="Images/ic_taxi_stand.png",Type="taxi_stand",Name="Taxi"},
                new GridViewHome(){ ImageType="Images/ic_gas_station.png",Type="gas_station",Name="Gas station"},

                new GridViewHome(){ ImageType="Images/ic_bus_station.png",Type="bus_station",Name="Bus station"},
                new GridViewHome(){ ImageType="Images/ic_train.png",Type="train_station",Name="Train station"},
                new GridViewHome(){ ImageType="Images/ic_subway.png",Type="subway_station",Name="Subway station"},
            };
            return lstGrid;
        }
    }
}