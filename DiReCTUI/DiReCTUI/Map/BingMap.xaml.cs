using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using GMap.NET.MapProviders;
using Microsoft.Maps.MapControl.WPF;
using GMap.NET;
using System.Diagnostics;

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
    
    public partial class BingMap 
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

        public BingMap()
        {
            InitializeComponent();
            Map.CredentialsProvider = new Credential().getCredential();
            Map.Focus();

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

            //initialize the satellite map
            test.MapProvider = GMap.NET.MapProviders.BingSatelliteMapProvider.Instance;
            test.Position = new PointLatLng(23.6978, 120.9605);
            test.Zoom = 13;
            test.CanDragMap = false;
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
            Map.Children.Add(poly);
        }

        // private helpers for draw circle
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
        /// Use to store the last touchpoint by user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BingMap_TouchDown(object sender, TouchEventArgs e)
        {
            var touchPoint = e.GetTouchPoint(this);
            lastTouchLocation = Map.ViewportPointToLocation(touchPoint.Position);
        }

        /// <summary>
        /// Button that zooms in the map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomIn_Click(object sender, EventArgs e)
        {
            test.Zoom = test.Zoom + 1;
            Map.ZoomLevel++;
            
        }

        /// <summary>
        /// Button that zoomout out the map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomOut_Click(object sender, EventArgs e)
        {
            Map.ZoomLevel--;
            test.Zoom--;
        }

        /// <summary>
        /// testing button
        /// currently it switches from the Mecrator map (googleMap) to Satellite map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Marker_Click(object sender, EventArgs e)
        {
            
            if (test.Visibility == Visibility.Hidden)
            {
                Map.Visibility = Visibility.Hidden;
                test.Zoom = Map.ZoomLevel;
                test.Position = new PointLatLng(Map.Center.Latitude, Map.Center.Longitude);
                test.Visibility = Visibility.Visible;
            }else
            {
                test.Visibility = Visibility.Hidden;
                Map.Visibility = Visibility.Visible;
            }


        }
       
        
        /// <summary>
        /// return currentMarker for other class to manipulate
        /// </summary>
        /// <returns></returns>
        public DraggablePin GetCurrentMarker()
        {
            if(currentMarker == null)
            {
                currentMarker = new DraggablePin(Map);
            }
            return currentMarker;
        }

        /// <summary>
        /// set currentMarker's position
        /// </summary>
        /// <param name="loc"></param>
        public void SetCurrentMarkerPosition(Location loc)
        {
            if(currentMarker != null)
            {
                currentMarker.Location = loc;
            }
        }

        /// <summary>
        /// return the map's ViewPortPointToLocation Function
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Location ViewPortToLocation(Point point) {
            return this.Map.ViewportPointToLocation(point);
        }
        
        /// <summary>
        /// add SOP pushpin, Pushpin with circle around the center
        /// </summary>
        /// <param name="Lat"></param>
        /// <param name="Lon"></param>
        /// <param name="label"></param>
        /// <param name="radius"></param>
        public void AddSOPPushPin(Location location, string label, double radius)
        {
            DrawCircle(location, radius);
            AddPushPins(location, label);
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
            Map.Children.Add(pin);
        }

        /// <summary>
        /// add draggable pushpin to the map
        /// </summary>
        /// <param name="Latitude"></param>
        /// <param name="Longitude"></param>
        /// <param name="label"></param>
        public void AddDraggablePins(Location loc, string label)
        {
            var pin = new DraggablePin(Map);
            {
                pin.Location = new Location(loc);
                pin.ToolTip = new Label()
                {
                    Content = label
                };
            }
            Map.Children.Add(pin);
        }
        
       
        
    }

}
