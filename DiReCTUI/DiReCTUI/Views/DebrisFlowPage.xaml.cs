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

using GMap.NET;
using Microsoft.Maps.MapControl.WPF;
using DiReCTUI.ViewModel;
using DiReCTUI.Map;
namespace DiReCTUI.Views
{
    /// <summary>
    /// Interaction logic for DebrisFlowPage.xaml
    /// </summary>
    public partial class DebrisFlowPage : Page
    {

        public DebrisFlowPage()
        {
            InitializeComponent();
            
        }

       

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

       
    }
}
