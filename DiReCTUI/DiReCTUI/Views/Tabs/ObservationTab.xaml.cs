using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DiReCTUI.Views;


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
            this.NavigationService.Navigate(new ObservationSample());
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
