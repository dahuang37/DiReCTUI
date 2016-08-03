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

        public List<SOP> LocationSOPCollection = new List<SOP>();
        public Location Location;
        public List<string> SOPTask;
        
        public SOP()
        {

        }
        public SOP(Location location, List<string> sopTask)
        {
            this.Location = location;
            this.SOPTask = sopTask;
            
        }
       
        public void Add(Location location, List<string> sopTask)
        {
            LocationSOPCollection.Add(new SOP(location, sopTask));
        }
        public void Add(SOP sop)
        {
            LocationSOPCollection.Add(sop);
        }
        public List<SOP> GetLocationSOP()
        {
            return this.LocationSOPCollection;
        }
        public void RemoveLocationSOP(SOP locationSOP)
        {
            LocationSOPCollection.Remove(locationSOP);
        }

        public void RemoveAllLocationSOP()
        {
            LocationSOPCollection.Clear();
        }
       
    }
}
