using DiReCTUI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiReCTUI.ViewModel
{

    /// <summary>
    /// this class binds with the DebrisFlowReminderDialog
    /// The debrisFlowReminderDialog shows up when the user approaches one defined location
    /// </summary>
    public class DebrisFlowReminderViewModel : DialogBase
    {
        
        private ObservableCollection<SOPDisplay> recordList = new ObservableCollection<SOPDisplay>();
        // binds with the listbox, must use observable collection to bind with listbox
        public ObservableCollection<SOPDisplay> RecordList
        {
            get { return recordList; }
        }
        
        // add the items to be recorded to the list
        public DebrisFlowReminderViewModel(Action<DialogBase> closeHandler, List<string> sopTask) : base(closeHandler)
        {
            foreach(string str in sopTask)
            {
                recordList.Add(new SOPDisplay() { Title = str });
            }
        }


    }
}
