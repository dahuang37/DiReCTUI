using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.ComponentModel;
using Microsoft.Maps.MapControl.WPF;
using DiReCTUI.Controls;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;


namespace DiReCTUI.Map
{
    //TODO:
    // add search function allow user to search for location
    /// <summary>
    /// Interaction logic for BingMap.xaml
    /// </summary>
    public class CustomTileSource : TileSource
    {
        readonly string UrlFormat = "http://localhost:8844/{0}/{1}/{2}/{3}";
        readonly int DbId = GMapProviders.GoogleMap.DbId;

        // keep in mind that bing only supports mercator based maps

        public override Uri GetUri(int x, int y, int zoomLevel)
        {
            return new Uri(string.Format(UrlFormat, DbId, zoomLevel, x, y));
        }
    }

    public class CustomTileLayer : MapTileLayer
    {
        public CustomTileLayer()
        {
            TileSource = new CustomTileSource();
        }
    }
    public partial class BingMap 
    {
        //Bing Map properties
        
        private Location lastTouchLocation;

        private GPSLocation GPS;
        public BingMap()
        {
            InitializeComponent();
            Map.Focus();

            //DataContext
            GPS = new GPSLocation();
            this.DataContext = GPS;
            
            try
            {
                // GMapProvider.WebProxy = WebRequest.DefaultWebProxy;
                // or
                Random rnd = new Random();
                int port = rnd.Next(8800, 8900);
                GMapProvider.WebProxy = new WebProxy("127.0.0.1", 1080);
                GMapProvider.IsSocksProxy = true;
                
                GMaps.Instance.EnableTileHost(port);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ex: " + ex);
                
            }


            // The pushpin to add to the map.
            DraggablePin pin = new DraggablePin(Map);
            {
                pin.Location = Map.Center;

                pin.ToolTip = new Label()
                {
                    Content = "GMap.NET fusion power! ;}"
                };
            }
            
            Map.Children.Add(pin);

        }

        private void BingMap_TouchDown(object sender, TouchEventArgs e)
        {


            TouchPoint p = e.GetTouchPoint(this);

            lastTouchLocation = Map.ViewportPointToLocation(p.Position);


        }
        private void ZoomIn_Click(object sender, EventArgs e)
        {
            Map.ZoomLevel++;
        }
        private void ZoomOut_Click(object sender, EventArgs e)
        {
            Map.ZoomLevel--;
        }

        private void Add_Marker_Click(object sender, EventArgs e)
        {
            if (lastTouchLocation != null)
            {
                Pushpin pin = new Pushpin();
                pin.Location = lastTouchLocation;
                Map.Children.Add(pin);
            }

        }

        public void getCurrentPosition()
        {
            if (GPS.Status == "Tracking")
            {
                Map.Center = new Location(GPS.Latitude, GPS.Longitude);
                if (Map.ZoomLevel < 12)
                {
                    Map.ZoomLevel = 12;
                }
                Pushpin pin = new Pushpin();
                {
                    pin.Location = Map.Center;

                    pin.ToolTip = new Label()
                    {
                        Content = "Current Position"
                    };
                }
                Map.Children.Add(pin);
            }
        }

        public void zoomIntoPosition(Location loc)
        {
            Map.Center = new Location(loc);
            Map.ZoomLevel = 15;
            Pushpin pin = new Pushpin();
            {
                pin.Location = Map.Center;

                pin.ToolTip = new Label()
                {
                    Content = "Current Position"
                };
            }
            Map.Children.Add(pin);
        }
        public void addRoute(Location loc)
        {
            //LocationCollection locs = new LocationCollection();
            //locs.Add(Map.Center);
            //locs.Add(loc);
            //MapPolyline routeLine = new MapPolyline()
            //{
            //    Locations = locs,
            //    Stroke = new SolidColorBrush(Colors.Blue),
            //    StrokeThickness = 5
            //};

            //Map.Children.Add(routeLine);
            MapPolyline polyline = new MapPolyline();
            polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
            polyline.StrokeThickness = 5;
            polyline.Opacity = 0.7;
            polyline.Locations = new LocationCollection() {
        new Location(47.6424, -122.3219),
        new Location(47.8424,-122.1747),
        new Location(47.67856,-122.130994)};

            Map.Children.Add(polyline);
            Map.Center = new Location(47.6424, -122.3219);
        }
        public Location getPosition()
        {
            return Map.Center;
        }
    }

}
