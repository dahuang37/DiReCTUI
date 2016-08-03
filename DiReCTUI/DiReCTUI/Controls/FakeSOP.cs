using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maps.MapControl.WPF;

namespace DiReCTUI.Controls
{
    public class FakeSOP
    {
        private SOP fakeSOP;
        
        public FakeSOP()
        {
            fakeSOP = new SOP();
            fakeSOP.AddLocationSOP(new Location(25.040, 121.6101), "Task1", "A");
            fakeSOP.AddLocationSOP(new Location(25.043, 121.611), "Task2", "B");
            fakeSOP.AddLocationSOP(new Location(25.0400233, 121.614), "Task3", "C");

        }
        public SOP GetFakeSOP()
        {
            return this.fakeSOP;
        }

        List<string> SOPTypes = new List<string>
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
