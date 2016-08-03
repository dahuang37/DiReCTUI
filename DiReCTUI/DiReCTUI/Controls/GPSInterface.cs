using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Device.Location;
using System.ComponentModel;
using Microsoft.Maps.MapControl.WPF;

namespace DiReCTUI.Controls
{
    public interface GPSInterface
    {
        
        Location Location
        {
            get;
            set;
        }

        string Status
        {
            get;
            set;
        }
        
        void StartTracking();
        
        void Watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e);
        
        void StopTracking();
       
    }
}
