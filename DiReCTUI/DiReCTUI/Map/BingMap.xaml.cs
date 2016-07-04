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

namespace DiReCTUI.Map
{
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

        public BingMap()
        {
            InitializeComponent();
            Map.Focus();

            //DataContext
            this.DataContext = new GPSLocation();

            GMaps.Instance.EnableTileHost(8845);
            // The pushpin to add to the map.
            Pushpin pin = new Pushpin();
            {
                pin.Location = Map.Center;

                pin.ToolTip = new Label()
                {
                    Content = "GMap.NET fusion power! ;}"
                };
            }
            Map.Children.Add(pin);

        }
        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GMaps.Instance.DisableTileHost();
            GMaps.Instance.CancelTileCaching();
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
    }

}
