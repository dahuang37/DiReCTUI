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

using GMap.NET.MapProviders;
using Microsoft.Maps.MapControl.WPF;
using GMap.NET;

namespace DiReCTUI.Map
{

    /// <summary>
    /// Interaction logic for BingMap.xaml
    /// Add Gmap on top of BingMap, allow user to switch between different source of Map
    /// Currently we are only using GoogleMap
    /// 
    /// To test the layer, comment out the <local: CustomTileLayer/> in BingMap.xaml
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

    /// <summary>
    /// Interaction logic for MainMap.xaml
    /// </summary>
    public partial class MainMap : UserControl
    {

        /// <summary>
        /// use to save the last point user touch screen to add marker
        /// </summary>
        private Location lastTouchLocation;

        /// <summary>
        /// current marker indicates user location
        /// </summary>
        private DraggablePin currentMarker;

        private const double earthRadiusInKilometers = 6367.0;


        /// <summary>
        /// This constructor initializes the Map and currentMarker
        /// </summary>
        /// 
        public MainMap()
        {
            InitializeComponent();

            map.CredentialsProvider = new Credential().getCredential();
            map.Focus();

            ///this part was in the demo, but I found that removing this part
            ///does not affect the functionality of the map
            #region demo part
            //try
            //{
            //    Random rnd = new Random();
            //    int port = rnd.Next(8800, 8900);
            //    GMapProvider.WebProxy = new WebProxy("127.0.0.1", 1080);
            //    GMapProvider.IsSocksProxy = true;

            //    GMaps.Instance.EnableTileHost(port);
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("ex: " + ex);
            //}
            #endregion

            // Initialize currentMarker to be at IIS
            currentMarker = new DraggablePin(map);
            {
                currentMarker.Location = new Location(25.04133, 121.6133);
                currentMarker.ToolTip = new Label()
                {
                    Content = "Current Position"
                };
            }
            currentMarker.Background = new SolidColorBrush(Color.FromArgb(100, 100, 100, 100));
            map.Children.Add(currentMarker);

            //initialize the satellite map
            stalliteMap.MapProvider = GMap.NET.MapProviders.BingSatelliteMapProvider.Instance;
            stalliteMap.Position = new PointLatLng(23.6978, 120.9605);
            stalliteMap.Zoom = 13;
            stalliteMap.CanDragMap = false;
        }

        /// <summary>
        /// return currentMarker for other class to manipulate
        /// </summary>
        /// <returns></returns>
        public DraggablePin GetCurrentMarker()
        {
            if (currentMarker == null)
            {
                currentMarker = new DraggablePin(map);
            }
            return currentMarker;
        }

        /// <summary>
        /// set currentMarker's position
        /// </summary>
        /// <param name="loc"></param>
        public void SetCurrentMarkerPosition(Location location)
        {
            if (currentMarker != null)
            {
                currentMarker.Location = location;
            }
        }

        /// <summary>
        /// return the map's ViewPortPointToLocation Function
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Location ViewPortToLocation(Point point)
        {
            return this.map.ViewportPointToLocation(point);
        }

        
        /// <summary>
        /// add immutable pushpins to the map
        /// </summary>
        /// <param name="loc"></param>
        /// <param name="label"></param>
        public void AddPushPins(Location loc, string label)
        {

            var pin = new Pushpin();
            {
                pin.Location = new Location(loc);
                pin.ToolTip = new Label()
                {
                    Content = label
                };
            }
            map.Children.Add(pin);
        }

        /// <summary>
        /// add draggable pushpin to the map
        /// </summary>
        /// <param name="Latitude"></param>
        /// <param name="Longitude"></param>
        /// <param name="label"></param>
        public void AddDraggablePins(Location loc, string label)
        {
            var pin = new DraggablePin(map);
            {
                pin.Location = new Location(loc);
                pin.ToolTip = new Label()
                {
                    Content = label
                };
            }
            map.Children.Add(pin);
        }

        /// <summary>
        /// draw the circle around the center, given the Location (Latitude and Longitude)
        /// this function would convert the Location type to radian and draw circle of radius in meters
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        public void DrawCircle(Location center, double radius)
        {
            // Add Red Polyline to the Map
            var poly = new MapPolyline();
            var locations = CreateCircleLocations(center, radius);
            poly.Locations = locations;
            poly.Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            poly.Fill = new SolidColorBrush(Color.FromArgb(20, 0, 0, 0));
            poly.StrokeThickness = 2;
            map.Children.Add(poly);
        }

        // private helpers for draw circle
        /// <summary>
        /// formula from online to convert from Location type to meters
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private LocationCollection CreateCircleLocations(Location center, double radius)
        {
            var earthRadius = earthRadiusInKilometers;
            var latitude = ToRadian(center.Latitude); //radians
            var longitude = ToRadian(center.Longitude); //radians
            var d = radius / earthRadius; // d = angular distance covered on earth's surface
            var locations = new LocationCollection();

            for (var x = 0; x <= 360; x++)
            {
                var range = ToRadian(x);
                var latitudeRadians = Math.Asin(Math.Sin(latitude) * Math.Cos(d) + Math.Cos(latitude) * Math.Sin(d) * Math.Cos(range));
                var longitudeRadians = longitude + Math.Atan2(Math.Sin(range) * Math.Sin(d) * Math.Cos(latitude), Math.Cos(d) - Math.Sin(latitude) *
                    Math.Sin(latitudeRadians));

                locations.Add(new Location(ToDegrees(latitudeRadians), ToDegrees(longitudeRadians)));
            }

            return locations;
        }
        /// <summary>
        /// convert degree to radian
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        private double ToRadian(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        /// <summary>
        /// radian to degree
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        private double ToDegrees(double radians)
        {
            return radians * (180 / Math.PI);
        }


        /// <summary>
        /// Use to store the last touchpoint by user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BingMap_TouchDown(object sender, TouchEventArgs e)
        {
            var touchPoint = e.GetTouchPoint(this);
            lastTouchLocation = map.ViewportPointToLocation(touchPoint.Position);
        }

        /// <summary>
        /// Button that zooms in the map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomIn_Click(object sender, EventArgs e)
        {
            stalliteMap.Zoom = stalliteMap.Zoom + 1;
            map.ZoomLevel++;

        }

        /// <summary>
        /// Button that zoomout out the map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomOut_Click(object sender, EventArgs e)
        {
            map.ZoomLevel--;
            stalliteMap.Zoom--;
        }

        /// <summary>
        /// testing button
        /// currently it switches from the Mecrator map (googleMap) to Satellite map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Marker_Click(object sender, EventArgs e)
        {

            if (stalliteMap.Visibility == Visibility.Hidden)
            {
                map.Visibility = Visibility.Hidden;
                stalliteMap.Zoom = map.ZoomLevel;
                stalliteMap.Position = new PointLatLng(map.Center.Latitude, map.Center.Longitude);
                stalliteMap.Visibility = Visibility.Visible;
            }
            else
            {
                stalliteMap.Visibility = Visibility.Hidden;
                map.Visibility = Visibility.Visible;
            }


        }
        
        
    }
}
