using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maps.MapControl.WPF;

namespace DiReCTUI.Controls
{
    public class DebrisFlowSOP
    {
        private SOP sOP;
        
        public DebrisFlowSOP()
        {
            sOP = new SOP();
            sOP.Add(new Location(25.040, 121.6101), new List<string>() { "Rock", "Plantation" });
            sOP.Add(new Location(25.043, 121.611), new List<string>() { "Catchment", "Slope" });
            sOP.Add(new Location(25.0400233, 121.614), new List<string>() { "Protected Objects", "Basic Info" });

        }
        public SOP GetSOP()
        {
            return this.sOP;
        }

        public List<string> SOPTypes = new List<string>
        {
            "Rock",
            "Plantation",
            "Protected Object",
            "Slope",
            "Catchment",
            "Basic Info"
        };



    }
}
