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
using System.Collections.ObjectModel;

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
        readonly int DbId = GMapProviders.BingMap.DbId;

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

        #region Fields
        //use to save the last point user touch screen to add marker
        private Location lastTouchLocation;
        //use to get user's GPS location
        private GPSLocation GPS;
        //current marker indicates user location
        private DraggablePin currentMarker;
        #endregion

        #region Constructor

        /// <summary>
        /// This constructor will initialize the Map and GPS location.
        /// The Map features uses combination of Gmap and Bing WPF control  
        /// </summary>

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
            currentMarker = new DraggablePin(Map);
            {
                currentMarker.Location = new Location(25.04133, 121.6133);

                currentMarker.ToolTip = new Label()
                {
                    Content = "Current Position"
                };
            }
            currentMarker.Background = new SolidColorBrush(Color.FromArgb(100, 100, 100, 100));
            Map.Children.Add(currentMarker);







        }
        #endregion

        #region circle
        private double ToRadian(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        private double ToDegrees(double radians)
        {
            return radians * (180 / Math.PI);
        }
        
        private const double EarthRadiusInKilometers = 6367.0;

        public void drawCircle(Location center, double radius)
        {
            // Add Red Polyline to the Map
            var poly = new MapPolyline();
            var locations = CreateCircle(center, radius);
            poly.Locations = locations;
            poly.Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            poly.StrokeThickness = 2;
            Map.Children.Add(poly);

        }

        private LocationCollection CreateCircle(Location center, double radius)
        {
            var earthRadius = EarthRadiusInKilometers;
            var lat = ToRadian(center.Latitude); //radians
            var lng = ToRadian(center.Longitude); //radians
            var d = radius / earthRadius; // d = angular distance covered on earth's surface
            var locations = new LocationCollection();

            for (var x = 0; x <= 360; x++)
            {
                var brng = ToRadian(x);
                var latRadians = Math.Asin(Math.Sin(lat) * Math.Cos(d) + Math.Cos(lat) * Math.Sin(d) * Math.Cos(brng));
                var lngRadians = lng + Math.Atan2(Math.Sin(brng) * Math.Sin(d) * Math.Cos(lat), Math.Cos(d) - Math.Sin(lat) * Math.Sin(latRadians));

                locations.Add(new Location(ToDegrees(latRadians), ToDegrees(lngRadians)));
            }

            return locations;
        }

        #endregion
        
        #region private helpers
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
        #endregion

        #region public functions
        public DraggablePin getCurrentMarker()
        {
            if(currentMarker == null)
            {
                currentMarker = new DraggablePin(Map);
            }
            return currentMarker;
        }
        public void setCurrentMarkerPosition(Location loc)
        {
            if(currentMarker != null)
            {
                currentMarker.Location = loc;
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
        public Location ViewPortToLocation(Point point) {
            return this.Map.ViewportPointToLocation(point);
        }
            
        public void addSOPPushPin(double Lat, double Lon, string label, double radius)
        {
            addPushPins(Lat, Lon, label);
            Location loc = new Location(Lat, Lon);
            drawCircle(loc, radius);

        }
        public void addPushPins(double Latitude, double Longitude, string label)
        {
           
            Pushpin pin = new Pushpin();
            {
                pin.Location = new Location(Latitude, Longitude);
                pin.ToolTip = new Label()
                {
                    Content = label
                };
            }
            Map.Children.Add(pin);
        }

        public void addDraggablePins(double Latitude, double Longitude, string label)
        {
            DraggablePin pin = new DraggablePin(Map);
            {
                pin.Location = new Location(Latitude, Longitude);
                pin.ToolTip = new Label()
                {
                    Content = label
                };
            }
            Map.Children.Add(pin);
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
        
       
        #endregion

      
       
    }

}
