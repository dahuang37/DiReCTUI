using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using GMap.NET.MapProviders;
using Microsoft.Maps.MapControl.WPF;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace DiReCTUI.Map
{

    #region GMap Tile Layer
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
    #endregion


    public partial class BingMap 
    {

        #region Fields
        /// <summary>
        /// use to save the last point user touch screen to add marker
        /// </summary>
        private Location lastTouchLocation;

        /// <summary>
        /// current marker indicates user location
        /// </summary>
        private DraggablePin currentMarker;

        #endregion

        #region Constructor

        /// <summary>
        /// This constructor initializes the Map and currentMarker
        /// </summary>

        public BingMap()
        {
            InitializeComponent();
            Map.CredentialsProvider = new Credential().getCredential();
            Map.Focus();

            #region demo part
            ///this part was in the demo, but I found that removing this part
            ///does not affect the functionality of the map
            //try
            //{
            //Random rnd = new Random();
            //int port = rnd.Next(8800, 8900);
            ////    GMapProvider.WebProxy = new WebProxy("127.0.0.1", 1080);
            ////    GMapProvider.IsSocksProxy = true;

            //GMaps.Instance.EnableTileHost(port);
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
        }


        #endregion

        #region Draw Circle / Radius
        
        /// <summary>
        /// main function to be called
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
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

        // private helpers
        private double ToRadian(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        private double ToDegrees(double radians)
        {
            return radians * (180 / Math.PI);
        }
        
        private const double EarthRadiusInKilometers = 6367.0;
      
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
        
        #region Buttons private helpers
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
        private async void Add_Marker_Click(object sender, EventArgs e)
        {
            //if (lastTouchLocation != null)
            //{
            //    Pushpin pin = new Pushpin();
            //    pin.Location = lastTouchLocation;
            //    Map.Children.Add(pin);
            //}
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            await metroWindow.ShowMessageAsync("Title", "Body");

        }
        #endregion

        #region Public functions
        
        /// <summary>
        /// return currentMarker for other class to manipulate
        /// </summary>
        /// <returns></returns>
        public DraggablePin getCurrentMarker()
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
        public void setCurrentMarkerPosition(Location loc)
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
        public void addSOPPushPin(Location loc, string label, double radius)
        {
            addPushPins(loc , label);
            drawCircle(loc, radius);
        }

        /// <summary>
        /// add immutable pushpins to the map
        /// </summary>
        /// <param name="loc"></param>
        /// <param name="label"></param>
        public void addPushPins(Location loc, string label)
        {
           
            Pushpin pin = new Pushpin();
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
        public void addDraggablePins(Location loc, string label)
        {
            DraggablePin pin = new DraggablePin(Map);
            {
                pin.Location = new Location(loc);
                pin.ToolTip = new Label()
                {
                    Content = label
                };
            }
            Map.Children.Add(pin);
        }
        
        #endregion
        
    }

}
