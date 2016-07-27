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

        //#region Click Functions For DataTemplate
        //private void General_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    CurrentPresenter.ContentTemplate = (DataTemplate)this.FindResource("DebrisFlowUpStream");   
        //}
        //private void UpStream_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    CurrentPresenter.ContentTemplate = (DataTemplate)this.FindResource("DebrisFlowCatchment");
        //}
        //private void UpStream_Back_Click(object sender, RoutedEventArgs e)
        //{
        //    CurrentPresenter.ContentTemplate = (DataTemplate)this.FindResource("DebrisFlowGeneral");
        //}
        //private void Catchment_Next_Click(object sender, RoutedEventArgs e)
        //{
        //    CurrentPresenter.ContentTemplate = (DataTemplate)this.FindResource("DebrisFlowFlowTrack");
        //}
        //private void Catchment_Back_Click(object sender, RoutedEventArgs e)
        //{
        //    CurrentPresenter.ContentTemplate = (DataTemplate)this.FindResource("DebrisFlowUpStream");
        //}
        //private void FlowTrack_Next_Click(object sender, RoutedEventArgs e)
        //{
            
        //}
        //private void FlowTrack_Back_Click(object sender, RoutedEventArgs e)
        //{
        //    CurrentPresenter.ContentTemplate = (DataTemplate)this.FindResource("DebrisFlowCatchment");
        //}
        //#endregion 

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }
        
    }
}
