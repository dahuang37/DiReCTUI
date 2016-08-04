using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiReCTUI.Controls
{
    public class SOPDisplay
    {
        
        // this is used for listbox binding SOP items
        // see example in DebrisFlowMapViewModel and DebrisFlowReminderViewModel
        public string Title { get; set; }
        public ICommand Command { get; set; }
        public SOPDisplay()
        {
        }
        
    }
}
