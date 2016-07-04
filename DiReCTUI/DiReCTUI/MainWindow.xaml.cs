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

using MahApps.Metro.Controls;
using System.ComponentModel;
using GMap.NET;
namespace DiReCTUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += new CancelEventHandler(MainWindow_Closing);
            
        }
        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GMaps.Instance.DisableTileHost();
            GMaps.Instance.CancelTileCaching();
        }
    }
}
