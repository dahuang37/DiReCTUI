using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiReCTUI.Map
{
    public class LocationChangedEventArgs : EventArgs
    {
        public readonly Location location;

        public LocationChangedEventArgs(Location location)
        {
            this.location = location;
        }
    }

   
}
