using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiReCTUI.ViewModel
{
    /// <summary>
    /// This should be inherited by all other dialog view model to include to close functionality
    /// </summary>
    public class DialogBase : ViewModelBase
    {
        private RelayCommand closeCommand;
        private Action<DialogBase> closeHandler;

        // binds with close button
        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new RelayCommand(p => this.Close());
                }
                return closeCommand;
            }
        }
        protected void Close()
        {
            this.closeHandler(this);
        }

        // takes the closehanlder and pass it to the command
        public DialogBase(Action<DialogBase> closeHandler)
        {
            this.closeHandler = closeHandler;
        }

    }
}
