using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiReCTUI.Controls
{
    public partial class SOP
    {
        public struct LocationSOP
        {
            public Location location;
            public string SOPTask;
            public string ID;
            
            //constructor for LocationSOP
            public LocationSOP(Location loc, string SOPTask, string ID) 
            {
                this.location = loc;
                this.ID = ID;
                this.SOPTask = SOPTask;
            }

            

        }
        
        private ObservableCollection<LocationSOP> LocationSOPCollection;

        #region Constructor
        public SOP()
        {
            LocationSOPCollection = new ObservableCollection<LocationSOP>();
        }
        #endregion

        #region public function
        public void addLocationSOP(Location loc, string SOPTask, string ID)
        {
            LocationSOP locSOP =  new LocationSOP(loc, SOPTask, ID);
            LocationSOPCollection.Add(locSOP);
            return;
        }
        public ObservableCollection<LocationSOP> getLocationSOP()
        {
            return this.LocationSOPCollection;
        }
        public void removeLocationSOP(LocationSOP locSOP)
        {
            LocationSOPCollection.Remove(locSOP);
        }

        public void removeAllLocationSOP()
        {
            LocationSOPCollection.Clear();
        }
        #endregion
    }
}
