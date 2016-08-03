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
    public partial class ObservationTab
    {
        public ObservationTab()
        {
            InitializeComponent();
        }

        /// <summary>
        /// initialize map and viewmodel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Debris_Flow_Click(object sender, RoutedEventArgs e)
        {      
            var debrisFlowPage = new DebrisFlowPage();
            var map = debrisFlowPage.MapPage.map;
            var debrisFlowRecord = new DebrisFlowRecord();
            var debrisFlowCollection = new DebrisFlowCollection();
            var debrisFlowViewModel = new DebrisFlowViewModel(map, debrisFlowRecord, debrisFlowCollection);
            
            debrisFlowPage.DataContext = debrisFlowViewModel;
            this.NavigationService.Navigate(debrisFlowPage);
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
