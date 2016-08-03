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

        private ObservableCollection<LocationSOP> LocationSOPCollection;

        public struct LocationSOP
        {
            public Location Location;
            public string SOPTask;
            public string Id;
            
            //constructor for LocationSOP
            public LocationSOP(Location location, string SOPTask, string Id) 
            {
                this.Location = location;
                this.Id = Id;
                this.SOPTask = SOPTask;
            }
        }

        public SOP()
        {
            LocationSOPCollection = new ObservableCollection<LocationSOP>();

        }
       
        public void AddLocationSOP(Location location, string SOPTask, string Id)
        {
            LocationSOP locationSOP =  new LocationSOP(location, SOPTask, Id);
            LocationSOPCollection.Add(locationSOP);
            return;
        }
        public ObservableCollection<LocationSOP> GetLocationSOP()
        {
            return this.LocationSOPCollection;
        }
        public void RemoveLocationSOP(LocationSOP locationSOP)
        {
            LocationSOPCollection.Remove(locationSOP);
        }

        public void RemoveAllLocationSOP()
        {
            LocationSOPCollection.Clear();
        }
       
    }
}
