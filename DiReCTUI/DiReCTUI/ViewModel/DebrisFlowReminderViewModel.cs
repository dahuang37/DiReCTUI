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
    /// </summary>
    public class DebrisFlowReminderViewModel : DialogBase
    {
        
        private ObservableCollection<SOPDisplay> recordList = new ObservableCollection<SOPDisplay>();
        public ObservableCollection<SOPDisplay> RecordList
        {
            get { return recordList; }
        }
        
        public DebrisFlowReminderViewModel(Action<DialogBase> closeHandler, List<string> sopTask) : base(closeHandler)
        {

            foreach(string str in sopTask)
            {
                recordList.Add(new SOPDisplay() { Title = str });
            }
        }


    }
}
