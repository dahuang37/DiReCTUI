using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using DiReCTUI.ViewModel;
using DiReCTUI.Model;
using MahApps.Metro.Controls.Dialogs;


namespace DiReCTUI.Views.Tabs
{
    /// <summary>
    /// Interaction logic for ObservationTab.xaml
    /// </summary>
    public partial class ObservationTab : Page
    {
        public ObservationTab()
        {
            InitializeComponent();
            
        }

        
        private void Debris_Flow_Click(object sender, RoutedEventArgs e)
        {
       
            var db = new DebrisFlowPage();
            var map = db.map;
            var dbRecord = new DebrisFlowRecord();
            var bgInfo = new BackgroundInfo().DebrisBackgroundInfo;
           
            

            var dbvm = new DebrisFlowViewModel(map, dbRecord, bgInfo);
            dbvm.RivuletName = "te3st"; 
            db.DataContext = dbvm;
            this.NavigationService.Navigate(db);
        }

        private void Fire_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Flood_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Protected_Objects_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
