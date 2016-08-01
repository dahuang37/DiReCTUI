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

        #region Properties
        
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

        #endregion

        #region public functions
        void StartTracking();
        
        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e);
        
        void StopTracking();
        #endregion
    }
}
